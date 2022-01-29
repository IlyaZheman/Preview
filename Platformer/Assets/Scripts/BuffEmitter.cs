using UnityEngine;

public class BuffEmitter : MonoBehaviour
{
    [SerializeField] private Buff buff;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (GameManager.Instance.BuffRecieverContainer.ContainsKey(col.gameObject))
        {
            var reciever = GameManager.Instance.BuffRecieverContainer[col.gameObject];
            reciever.AddBuff(buff);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (GameManager.Instance.BuffRecieverContainer.ContainsKey(col.gameObject))
        {
            var reciever = GameManager.Instance.BuffRecieverContainer[col.gameObject];
            reciever.RemoveBuff(buff);
        }
    }
}

[System.Serializable]
public class Buff
{
    public BuffType type;
    public float additiveBonus;
    public float multipleBonus;
    
    
}

public enum BuffType : byte
{
    Damage, Force, Armor
}