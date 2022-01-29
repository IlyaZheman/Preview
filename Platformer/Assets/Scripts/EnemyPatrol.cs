using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private GameObject leftBorder;
    [SerializeField] private GameObject rightBorder;
    [SerializeField] private new Rigidbody2D rigidbody;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GroundDetection groundDetection;
    [SerializeField] private CollisionDamage collisionDamage;
    
    [SerializeField] private bool isRightDirection;
    
    [SerializeField] private float speed;

    private void Update()
    {
        if (groundDetection.IsGrounded)
        {
            if (transform.position.x > rightBorder.transform.position.x || collisionDamage.Direction < 0)
            {
                isRightDirection = false;
            }
            else if (transform.position.x < leftBorder.transform.position.x || collisionDamage.Direction > 0)
            {
                isRightDirection = true;
            }
            rigidbody.velocity = isRightDirection ? Vector2.right : Vector2.left;
            rigidbody.velocity *= speed;
        }
        if (rigidbody.velocity.x > 0)
            spriteRenderer.flipX = true;
        if (rigidbody.velocity.x < 0)
            spriteRenderer.flipX = false;
        
        
        animator.SetFloat("Speed", speed);
    }
}
