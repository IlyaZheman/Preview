using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int health;
    public int CurrentHealth
    {
        get => health;
    }

    [SerializeField] private int maxHealth = 100;

    public int MaxHealth
    {
        get => maxHealth;
    }
    
    [SerializeField] private TakeDamage takeDamage;

    private void Start()
    {
        GameManager.Instance.HealthContainer.Add(gameObject, this);
    }

    public void TakeHit(int damage)
    {
        health -= damage;
        Debug.Log("Здоровье у " + gameObject.name + " = " + health);
        if (health <= 0)
        {
            Destroy(gameObject);
            Debug.Log(gameObject.name + " умер!");
        }
        else if (takeDamage != null)
        {
            takeDamage.GetDamage();
        }
    }

    public void SetHeath(int bonusHealth)
    {
        health += bonusHealth;
        if (health > MaxHealth)
        {
            health = MaxHealth;
        }
        Debug.Log("Здоровье у " + gameObject.name + " = " + health);
    }
}
