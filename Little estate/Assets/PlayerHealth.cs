using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamagable
{

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
        GetComponent<PlayerFightController>().CanAttacking();
        _animator.SetTrigger("damage");

        // Отскок
        Vector3 knockbackDirection = -hitDirection.normalized;
        GetComponent<CharacterController>().Move((knockbackDirection + Vector3.up) * _knockbackForce * -1);
    }

    public void TakeDamage(int damageCount)
    {
        Debug.Log("Damage");
    }
}
