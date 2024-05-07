using System.Collections;
using System.Collections.Generic;
using Pool;
using UnityEngine;

public class DamagableObject : Enemy
{

    public override void OnDeath()
    {
        SpawnItems();
    }
    

    
}
