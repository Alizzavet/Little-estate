using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Item", menuName = "Configs/Items")]
public class DropedItemConfig : ScriptableObject
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _countToSpawn;
    [SerializeField] private string _id;
    
    [SerializeField] private int _count;
    public int Count => _count;



    public Sprite Sprite => _sprite;
    public int CountToSpawn => _countToSpawn;
    public string Id => _id;
}
