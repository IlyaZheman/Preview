using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Dictionary<GameObject, Health> HealthContainer;
    public Dictionary<GameObject, Coin> CoinContainer;
    public Dictionary<GameObject, Medkit> MedkitContainer;
    public Dictionary<GameObject, BuffReciever> BuffRecieverContainer;

    private void Awake()
    {
        Instance = this;
        HealthContainer = new Dictionary<GameObject, Health>();
        CoinContainer = new Dictionary<GameObject, Coin>();
        MedkitContainer = new Dictionary<GameObject, Medkit>();
        BuffRecieverContainer = new Dictionary<GameObject, BuffReciever>();
    }

    public void OnClickPause()
    {
        Time.timeScale = Time.timeScale > 0 ? 0 : 1;
    }
}
