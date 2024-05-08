using System.Collections.Generic;
using Pool;
using UnityEngine;

public class PlayerInventory : MonoBehaviour, IInputable
{
    [SerializeField] private List<InventorySlot> _inventorySlots;
    private const int MaxCount = 30;
    
    private int _selectedSlotIndex;
    private readonly Color _defaultColor = Color.white; 
    private readonly Color _selectedColor = Color.green; 

    private void OnEnable()
    {
        InputSystem.Instance.SetInput(this);
        
        _inventorySlots[_selectedSlotIndex]._slotSprite.color = _selectedColor;
    }
    public bool AddItem(DropedItemConfig item)
    {
        // Проверяем, есть ли уже такой предмет в инвентаре
        foreach (var slot in _inventorySlots)
        {
            if (slot._myItem == item && slot._currentCount < MaxCount)
            {
                slot.AddToStack();
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
            
            // в конце. обнуление инвентаря
            _inventorySlots[_selectedSlotIndex].DropItem();

        }
        
    }
}