using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image health;
    [SerializeField] private float delta;
    private float _healthValue;
    private float _currentHealth;
    private Player _player;
    void Start()
    {
        _player = Player.Instance;
        _healthValue = _player.Health.CurrentHealth / (float) _player.Health.MaxHealth;
    }

    void Update()
    {
        _currentHealth = _player.Health.CurrentHealth / (float) _player.Health.MaxHealth;
        if (_currentHealth > _healthValue)
        {
            _healthValue += delta;
        }
        if (_currentHealth < _healthValue)
        {
            _healthValue -= delta;
        }
        if (_currentHealth < delta)
        {
            _healthValue = _currentHealth;
        }
        
        health.fillAmount = _healthValue;
    }
}
