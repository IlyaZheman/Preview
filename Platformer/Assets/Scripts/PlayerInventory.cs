using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private Text coinsText;
    public string CoinsText
    {
        get => coinsText.text;
        set => coinsText.text = value;
    }
    
    [SerializeField] private int coinsCount;
    public int CoinsCount
    {
        get => coinsCount;
        set
        {
            if (value > 0)
            {
                coinsCount = value;
            }
        }
    }
    public static PlayerInventory Instance { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        coinsText.text = "0";
    }
}
