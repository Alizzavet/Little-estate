using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (BoxCollider))]
public abstract class InteractableObject : MonoBehaviour
{
    [SerializeField] protected DialogueConfig _dia;
}
