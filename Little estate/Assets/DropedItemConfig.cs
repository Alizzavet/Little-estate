using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Configs/Items")]
public class DropedItemConfig : ScriptableObject
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _count;
    [SerializeField] private string _id;


    public Sprite Sprite => _sprite;
    public int Count => _count;
    public string Id => _id;
}
