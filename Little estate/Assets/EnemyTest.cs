using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour, IDamagable
{
    private int _health = 10;
    
    public void TakeDamage(int damageCount)
    {
        _health -= damageCount;

        if (_health <= 0)
            Destroy(gameObject);

    }
    
}
