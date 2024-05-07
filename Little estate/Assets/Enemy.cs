using System;
using System.Collections;
using System.Collections.Generic;
using Pool;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] private EnemyConfig _enemyConfig;
    [SerializeField] protected List<DropedItemConfig> _dropedItems;
    
    private int _health;

    private void OnEnable()
    {
        _health = _enemyConfig.Health;
    }

    public abstract void OnDeath();
    
    public virtual void TakeDamage(int damageCount)
    {
        _health -= damageCount;

        if (_health <= 0)
        {
            OnDeath();
            
            
            //TODO заносить в пул и удалять из него
            Destroy(gameObject);
        }

    }

    protected void SpawnItems()
    {
        foreach (var itemConfig in _dropedItems)
        {
            for (var i = 0; i < itemConfig.Count; i++)
            {
                var item = PoolObject.Get<DropedItem>();
                item.SetData(itemConfig, transform);
            }
        }
    }
}
