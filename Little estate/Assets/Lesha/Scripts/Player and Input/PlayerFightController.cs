using System;
using System.Collections;
using System.Collections.Generic;
using Pool;
using UnityEngine;

public class PlayerFightController : MonoBehaviour, IInputable
{
    [SerializeField] private Transform _punchPosition;
    [SerializeField] private Animator _animator;

    private int _damage = 1;
    public bool _isAttacking;
    private bool _canAttack = true;

    private int _combo = 0;
    private void OnEnable()
    {
        InputSystem.Instance.SetInput(this);
    }

    private void Attack()
    {

        
        var hitEnemies = Physics.OverlapSphere(_punchPosition.position, 1f);

        foreach (var enemy in hitEnemies)
        {
            var damagableComponent = enemy.GetComponent<IDamagable>();
            if (damagableComponent != null)
            {
                var text = PoolObject.Get<DamageText>();
                text.transform.position = enemy.transform.position + new Vector3(0, 5f, 0);
                text.SetText(_damage + _combo);
        
                damagableComponent.TakeDamage(_damage + _combo);
            }

        }
        _combo = 1;
    }
    
    private void PlayAttackAnimation()
    {
        _animator.SetTrigger("attack" + _combo);
        _isAttacking = true;
        _canAttack = false;
    }

    private void EndAttack()
    {
        _isAttacking = false;
        _combo = 0;
        _canAttack = true;
    }

    private void CanAttack()
    {
        _canAttack = true;
    }
    
    public void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.F) && _canAttack)
            PlayAttackAnimation();
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_punchPosition.position, 1f);
    }
    
}