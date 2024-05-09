using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class ChestInventory : MonoBehaviour, IInventory
{
    // такой же, как у playerInventory
    
    
    [FormerlySerializedAs("_inventorySlots")] [SerializeField] private List<InventorySlot> _chestInventorySlots;
    
    private const int MaxCount = 30;
    
    private int _selectedSlotIndex;
    private readonly Color _defaultColor = Color.white; 
    private readonly Color _selectedColor = Color.green; 
    
    
    /*public bool AddItem(DropedItemConfig item)
    {
        // Проверяем, есть ли уже такой предмет в инвентаре
        foreach (var slot in _chestInventorySlots)
        {
            if (slot._myItem == item && slot._currentCount < MaxCount)
            {
                slot.AddToStack();
                return true;
            }
        }

        // Если нет, ищем пустой слот и добавляем туда предмет
        foreach (var slot in _chestInventorySlots)
        {
            if (slot._myItem == null)
            {
                slot.SetItem(item);
                return true;
            }
        }

        Debug.Log("Нельзя");
        // Если нет пустых слотов и предмета в инвентаре
        return false;
    }*/


    /*
     Старая штука
    public bool GetSlot(InventorySlot item)
    {
        // Проверяем, есть ли уже такой предмет в инвентаре
        foreach (var slot in _chestInventorySlots)
        {
            if (slot._myItem == item._myItem && slot._currentCount < MaxCount)
            {

                var newcount = slot.AddToStack(item._currentCount);
                return true;

            }
        }
        
        // Если нет, ищем пустой слот и добавляем туда предмет
        foreach (var slot in _chestInventorySlots)
        {
            if (slot._myItem == null)
            {
                slot.SetItem(item._myItem);
                return true;
            }
        }

        return false;
    }*/
    public bool GetSlot(InventorySlot item)
    {
        // Проверяем, есть ли уже такой предмет в инвентаре
        foreach (var slot in _chestInventorySlots)
        {
            if (slot._myItem == item._myItem && slot._currentCount < MaxCount)
            {
                int remaining = slot.AddToStack(item._currentCount);
                if (remaining == 0)
                {
                    return true; // все предметы поместились в стак
                }

                item._currentCount = remaining; // обновляем количество предметов в переданном слоте
            }
        }
    
        // Если нет, ищем пустой слот и добавляем туда предмет
        foreach (var slot in _chestInventorySlots)
        {
            if (slot._myItem == null)
            {
                slot.SetItem(item._myItem);
                item._currentCount = slot.AddToStack(item._currentCount - 1); // добавляем предметы в слот и обновляем количество оставшихся предметов
                if (item._currentCount == 0)
                {
                    return true; // все предметы поместились в стак
                }
            }
        }

        // Если все слоты заполнены, ищем слоты с тем же предметом и добавляем туда оставшиеся предметы
        foreach (var slot in _chestInventorySlots)
        {
            if (slot._myItem == item._myItem && slot._currentCount < MaxCount)
            {
                int remaining = slot.AddToStack(item._currentCount);
                if (remaining == 0)
                {
                    return true; // все предметы поместились в стак
                }
                else
                {
                    item._currentCount = remaining; // обновляем количество предметов в переданном слоте
                }
            }
        }

        return false; // не все предметы поместились в стак
    }

    

    public void SelectSlot(InventorySlot slot)
    {
        if (slot._myItem == null)
            Debug.Log($"Слот Инвентаря пуст");
        else
            Debug.Log($"Слот Инвентаря {slot._currentCount}");
        
        
    }
}
