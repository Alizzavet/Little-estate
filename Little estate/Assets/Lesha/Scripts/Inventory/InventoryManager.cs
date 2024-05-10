using System.Collections.Generic;
using UnityEngine;

public abstract  class InventoryManager : MonoBehaviour, IInventory
{
    [SerializeField] protected List<InventorySlot> _inventorySlots;
    protected const int MaxCount = 30;
    
    protected int _selectedSlotIndex;
    protected readonly Color _defaultColor = Color.white; 
    protected readonly Color _selectedColor = Color.green;

    public bool GetSlot(InventorySlot item)
    {
        // Проверяем, есть ли уже такой предмет в инвентаре
        foreach (var slot in _inventorySlots)
        {
            if (slot._myItem == item._myItem && slot._currentCount < MaxCount)
            {
                var remaining = slot.AddToStack(item._currentCount);
                if (remaining == 0)
                    return true;

                item._currentCount = remaining; 
            }
        }
    
        // Если нет, ищем пустой слот и добавляем туда предмет
        foreach (var slot in _inventorySlots)
        {
            if (slot._myItem == null)
            {
                slot.SetItem(item._myItem);
                item._currentCount = slot.AddToStack(item._currentCount - 1); 
                if (item._currentCount == 0)
                {
                    return true; // все предметы поместились в стак
                }
            }
        }

        // Если все слоты заполнены, ищем слоты с тем же предметом и добавляем туда оставшиеся предметы
        foreach (var slot in _inventorySlots)
        {
            if (slot._myItem == item._myItem && slot._currentCount < MaxCount)
            {
                var remaining = slot.AddToStack(item._currentCount);
                if (remaining == 0)
                    return true; // все предметы поместились в стак

                item._currentCount = remaining; // обновляем количество предметов в переданном слоте
            }
        }

        return false; // не все предметы поместились в стак
    }

    public abstract void SelectSlot(InventorySlot slot);
}
