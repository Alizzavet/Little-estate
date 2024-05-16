using Pool;
using UnityEngine;

public class PlayerInventory : InventoryManager, IInputable
{
    public static PlayerInventory Instance;

    private ChestInventory _currentChest;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void SetChest(ChestInventory chest)
    {
        _currentChest = chest;
    }
    public void UnsetChest()
    {
        _currentChest = null;
    }

    private void OnEnable()
    {
        InputSystem.Instance.SetInput(this);
        
        _inventorySlots[_selectedSlotIndex]._slotSprite.color = _selectedColor;
    }
    public bool AddItemFromWorld(DropedItemConfig item)
    {
        // Проверяем, есть ли уже такой предмет в инвентаре
        foreach (var slot in _inventorySlots)
        {
            if (slot._myItem == item && slot._currentCount < MaxCount)
            {
                slot.AddToStack(item.Count);
                return true;
            }
        }

        // Если нет, ищем пустой слот и добавляем туда предмет
        foreach (var slot in _inventorySlots)
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
    }

    public void HandleInput()
    {
        for (var i = 1; i <= _inventorySlots.Count; i++)
        {
            if (Input.GetKeyDown(i.ToString()))
            {
                _inventorySlots[_selectedSlotIndex]._slotSprite.color = _defaultColor;
                _selectedSlotIndex = i - 1;
                _inventorySlots[_selectedSlotIndex]._slotSprite.color = _selectedColor;
            }
        }

        // выбрасывание предмета
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (_inventorySlots[_selectedSlotIndex]._myItem == null) 
                return;
            
            for (var i = 0; i < _inventorySlots[_selectedSlotIndex]._currentCount; i++)
            {
                var dropItem = PoolObject.Get<DropedItem>();
                dropItem.SetDropData(_inventorySlots[_selectedSlotIndex]._myItem, transform);
            }
            
            
            Debug.Log($"Передаю {transform.localPosition}");
            
            // в конце. обнуление инвентаря
            _inventorySlots[_selectedSlotIndex].DropItem();

        }
        
    }
    public new bool GetSlot(InventorySlot item)
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
                    return true;
            }
        }

        // Если все слоты заполнены, ищем слоты с тем же предметом и добавляем туда оставшиеся предметы
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

        return false; 
    }
    
    public override void SelectSlot(InventorySlot slot)
    {
        if (_currentChest == null)
            return;
        
        if (_currentChest.GetSlot(slot))
            slot.DropItem();
    }
}