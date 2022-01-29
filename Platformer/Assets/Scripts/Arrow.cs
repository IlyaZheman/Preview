using System.Collections;
using UnityEngine;

public class Arrow : MonoBehaviour, IObjectDestroyer
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private new Rigidbody2D rigidbody;
    [SerializeField] private TriggerDamage triggerDamage;
    [SerializeField] private float lifeTime;
    
    [SerializeField] private float force;
    public float Force
    {
        get { return force; }
        set { force = value; }
    }

    private Player _player;
    
    public void Destroy(GameObject gameObject)
    {
        _player.ReturnArrowToPool(this);
    }
    
    public void SetImpulse (Vector2 direction, float force, Player player)
    {
        _player = player;
        triggerDamage.Init(this);
        triggerDamage.Parent = player.gameObject;
        rigidbody.AddForce(direction * force, ForceMode2D.Impulse);
        transform.rotation = Quaternion.Euler(0, force < 0 ? 180 : 0, 0);
        StartCoroutine(StartLife());
    }

    private IEnumerator StartLife()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
        yield break;
    }
}
