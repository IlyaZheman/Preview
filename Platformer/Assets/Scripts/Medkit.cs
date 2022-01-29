using Unity.VisualScripting;
using UnityEngine;

public class Medkit : MonoBehaviour
{
    [SerializeField] private int bonusHealth;
    [SerializeField] private Animator animator;

    private void Start()
    {
        GameManager.Instance.MedkitContainer.Add(gameObject, this);
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Health health = col.gameObject.GetComponent<Health>();
            health.SetHeath(bonusHealth);
            StartDestroy();
        }
    }
    
    public void StartDestroy()
    {
        animator.SetTrigger("StartDestroy");
    }

    public void EndDestroy()
    {
        Destroy(gameObject);
    }
}
