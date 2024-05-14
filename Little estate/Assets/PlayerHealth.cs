using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamagable
{

    private int _health = 100;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damageCount)
    {
        _health -= damageCount;
        
        _animator.SetTrigger("damage");
    }
}
