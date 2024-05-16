using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamagable
{
    private int _maxHealth = 100;
    private int _health = 100;
    private Animator _animator;
    private int _knockbackForce = 5;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damageCount, Vector3 hitDirection)
    {
        _health -= damageCount;
        GetComponent<HealthBarUI>().UpdateHealthBar(_maxHealth, _health);


        if (!IsDeath())
        {
            _animator.SetTrigger("damage");
            var knockbackDirection = -hitDirection.normalized;
            GetComponent<CharacterController>().Move(knockbackDirection * _knockbackForce * -1);
            
            GetComponent<PlayerFightController>().CanAttacking();
            InputSystem.Instance.StartCoroutine(InputSystem.Instance.SetPauseInput(0.5f));
        }
        else
        {
            Debug.Log("Умер");
            // Здесь логику для смэрти
        }

    }

    public void Heal(int healCount)
    {
        _health += healCount;
        if (_health > _maxHealth)
            _health = _maxHealth;
        
        GetComponent<HealthBarUI>().UpdateHealthBar(_maxHealth, _health);
    }

    private bool IsDeath()
    {
        if (_health <= 0)
            return true;

        return false;
    }
    
    public void TakeDamage(int damageCount)
    {
        Debug.Log("Damage");
    }
}
