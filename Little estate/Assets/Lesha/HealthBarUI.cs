using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Image _healthBar;
    [SerializeField] private float _duration = 0.5f; 
    
    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        var currentFillAmount = currentHealth / maxHealth;
        _healthBar.DOFillAmount(currentFillAmount, _duration);
    }
}
