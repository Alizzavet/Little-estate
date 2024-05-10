using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Configs/Enemy")]
public class EnemyConfig : ScriptableObject
{
    [SerializeField] private int _health;

    public int Health => _health;
}
