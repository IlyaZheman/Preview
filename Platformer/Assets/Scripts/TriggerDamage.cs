using UnityEngine;

public class TriggerDamage : MonoBehaviour
{
    [SerializeField] private int damage;
    public int Damage
    {
        get => damage;
        set => damage = value;
    }
    
    [SerializeField] private bool isDestroyingAfterDamage;
    private IObjectDestroyer _destroyer;
    
    private GameObject _parent;
    public GameObject Parent
    { 
        get => _parent;
        set => _parent = value;
    }

    public void Init(IObjectDestroyer destroyer)
    {
        _destroyer = destroyer;
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == _parent || GameManager.Instance.CoinContainer.ContainsKey(col.gameObject) || 
            GameManager.Instance.MedkitContainer.ContainsKey(col.gameObject))
        {
            return;
        }
        
        if (GameManager.Instance.HealthContainer.ContainsKey(col.gameObject))
        {
            var health = GameManager.Instance.HealthContainer[col.gameObject];
            health.TakeHit(damage);
        }

        if (isDestroyingAfterDamage)
        {
            if (_destroyer == null)
            {
                Destroy(gameObject);
            }
            else
            {
                _destroyer.Destroy(gameObject);
            }
        }
    }
}

public interface IObjectDestroyer
{
    void Destroy(GameObject gameObject);
}
