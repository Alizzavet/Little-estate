using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public SerializableDictionary<DropedItemConfig, int> _dictionaryInventory = new();


    public void AddItem(DropedItemConfig item)
    {
        if (_dictionaryInventory.ContainsKey(item))
            _dictionaryInventory[item] += 1;
        else
            _dictionaryInventory.Add(item, 1);
    }
}
