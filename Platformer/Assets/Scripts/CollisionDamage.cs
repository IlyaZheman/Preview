using System;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    [SerializeField] private Animator animator;
    private Health _health;
    private float _direction;
    public float Direction => _direction;

    public void OnCollisionStay2D(Collision2D col)
    {
        if (GameManager.Instance.HealthContainer.ContainsKey(col.gameObject))
        {
            _health = GameManager.Instance.HealthContainer[col.gameObject];
            _direction = (col.transform.position - transform.position).x;
            animator.SetFloat("Direction", Mathf.Abs(_direction));
        }
        else
        {
            _health = null;
        }
    }

    public void SetDamage()
    {
        if (_health != null)
        {
            _health.TakeHit(damage);
        }
        _health = null;
        _direction = 0;
        animator.SetFloat("Direction", 0f);
    }
}
