using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private new Rigidbody2D rigidbody;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GroundDetection groundDetection;
    [SerializeField] private Health health;

    public Health Health
    {
        get => health;
    }

    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    [SerializeField] private float minimalHeight;

    [SerializeField] private bool isCheatMode;
    [SerializeField] private float flyForce;

    [SerializeField] private Arrow arrow;
    [SerializeField] private Transform arrowSpawnPoint;
    [SerializeField] private float shootForce;
    [SerializeField] private float reloadTime;

    [SerializeField] private int arrowsCount = 5;
    
    private Vector3 _direction;
    private bool _isJumping;
    private bool _isCooldownAtShooting;
    private List<Arrow> _arrowsPool;

    public static Player Instance { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _arrowsPool = new List<Arrow>();
        for (int i = 0; i < arrowsCount; i++)
        {
            var arrowTemp = Instantiate(arrow, arrowSpawnPoint);
            _arrowsPool.Add(arrowTemp);
            arrowTemp.gameObject.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        if (animator != null)
        {
            animator.SetBool("IsGrounded", groundDetection.IsGrounded);
        }

        if (animator != null && !_isJumping && !groundDetection.IsGrounded)
        {
            animator.SetTrigger("StartFall");
        }

        _isJumping = _isJumping && !groundDetection.IsGrounded;

        _direction = Vector3.zero;
        if (Input.GetKey(KeyCode.A))
        {
            _direction = Vector3.left;
        }

        if (Input.GetKey(KeyCode.D))
        {
            _direction = Vector3.right;
        }

        _direction *= speed;
        _direction.y = rigidbody.velocity.y;
        rigidbody.velocity = _direction;

        if (_direction.x > 0)
        {
            spriteRenderer.flipX = false;
        }

        if (_direction.x < 0)
        {
            spriteRenderer.flipX = true;
        }

        if (animator != null)
        {
            animator.SetFloat("Speed", Mathf.Abs(_direction.x));
        }

        if (Input.GetKey(KeyCode.W) && isCheatMode)
        {
            rigidbody.AddForce(Vector2.up * (flyForce * Time.deltaTime));
        }

        CheckFall();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && groundDetection.IsGrounded)
        {
            rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetTrigger("StartJump");
            _isJumping = true;
        }
        
        CheckShoot();
    }

    private void OnTriggerEnter2D(Collider2D col) //todo перенести скрипт в PlayerInventory
    {
        if (GameManager.Instance.CoinContainer.ContainsKey(col.gameObject))
        {
            PlayerInventory.Instance.CoinsCount++;
            PlayerInventory.Instance.CoinsText = PlayerInventory.Instance.CoinsCount.ToString();
            var coin = GameManager.Instance.CoinContainer[col.gameObject];
            coin.StartDestroy();
        }
    }

    void CheckShoot()
    {
        if (Input.GetMouseButtonDown(0) && !_isCooldownAtShooting && groundDetection.IsGrounded)
        {
            animator.SetTrigger("Shoot");
            
            _isCooldownAtShooting = true;
            StartCoroutine(Cooldown());
        }
    }
    
    void Shoot()
    {
        Arrow prefab = GetArrowFromPool();
        
        prefab.SetImpulse
            (Vector2.right, spriteRenderer.flipX ? -shootForce : shootForce, this);
    }

    private Arrow GetArrowFromPool()
    {
        if (_arrowsPool.Count > 0)
        {
            var arrowTemp = _arrowsPool[0];
            _arrowsPool.Remove(arrowTemp);
            arrowTemp.gameObject.SetActive(true);
            arrowTemp.transform.parent = null;
            arrowTemp.transform.position = arrowSpawnPoint.transform.position;
            return arrowTemp;
        }
        return Instantiate(arrow, arrowSpawnPoint.position, quaternion.identity);
    }

    public void ReturnArrowToPool(Arrow arrowTemp)
    {
        if (!_arrowsPool.Contains(arrowTemp))
        {
            _arrowsPool.Add(arrowTemp);
        }

        arrowTemp.transform.parent = arrowSpawnPoint;
        arrowTemp.transform.position = arrowSpawnPoint.transform.position;
        arrowTemp.gameObject.SetActive(false);
    }
    
    void CheckFall()
    {
        if (transform.position.y < minimalHeight && isCheatMode)
        {
            rigidbody.velocity = new Vector2(0, 0);
            transform.position = new Vector3(0, 0, 0);
        }
        else if (transform.position.y < minimalHeight)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(reloadTime);
        _isCooldownAtShooting = false;
        yield break;
    }
}