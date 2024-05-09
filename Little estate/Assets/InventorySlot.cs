using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
// TODO сюда нужно добавить макс предметов в стаке (может попробовать черех DropItemConf)
public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image _sprite;
    [SerializeField] private TMP_Text _countText;

    [SerializeField] public Image _slotSprite;
    public IInventory inventory;
    public int _currentCount { get; internal set; }

    public DropedItemConfig _myItem { get; private set; }
    
    

    private void Start()
    {
        _sprite.sprite = null;
        _countText.text = 0.ToString();
        
        inventory = GetComponentInParent<IInventory>();
    }

    public void SetItem(DropedItemConfig item)
    {
        _myItem = item;
        _currentCount = 1;
        
        UpdateInfo();
    }

// TODO вот здесь менять
    /*public void AddToStack()
    {
        _currentCount++;
        UpdateInfo();
    }*/
    public int AddToStack(int itemCount)
    {
        const int maxItemCount = 30;
        int newCount = _currentCount + itemCount;

        if (newCount > maxItemCount)
        {
            _currentCount = maxItemCount;
            UpdateInfo();
            return newCount - maxItemCount; // возвращаем остаток
        }
        else
        {
            _currentCount = newCount;
            UpdateInfo();
            return 0; // все предметы поместились в стак, остаток равен 0
        }
    }


    private void UpdateInfo()
    {
        if (_myItem != null)
            _sprite.sprite = _myItem.Sprite;
        
        _countText.text = _currentCount.ToString();
    }

    public void DropItem()
    {
        _myItem = null;

        _sprite.sprite = null;
        _currentCount = 0;
        UpdateInfo();
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            // Получаем объект, по которому было произведено нажатие
            GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;

            // Проверяем, является ли объект слотом инвентаря или его дочерним объектом
            var clickedSlot = clickedObject.GetComponentInParent<InventorySlot>();
            if (clickedSlot != null)
            {
                // Вызываем метод SelectSlot для обновления выбранного слота
                inventory.SelectSlot(this);
            }
        }
    }
}
