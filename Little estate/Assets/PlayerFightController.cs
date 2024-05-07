using System;
using System.Collections;
using System.Collections.Generic;
using Pool;
using UnityEngine;

public class PlayerFightController : MonoBehaviour, IInputable
{
    [SerializeField] private Transform _punchPosition;

    private int _damage = 1;
    
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
                text.SetText(_damage);
        
                damagableComponent.TakeDamage(_damage);
            }

        }
    }
    
    
    public void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.F))
            Attack();
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_punchPosition.position, 1f);
    }
    
}
