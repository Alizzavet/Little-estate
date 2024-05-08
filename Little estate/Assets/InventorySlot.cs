using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image _sprite;
    [SerializeField] private TMP_Text _countText;

    [SerializeField] public Image _slotSprite;

    public int _currentCount { get; private set; }

    public DropedItemConfig _myItem { get; private set; }

    private void Start()
    {
        _sprite.sprite = null;
        _countText.text = 0.ToString();
    }

    public void SetItem(DropedItemConfig item)
    {
        _myItem = item;
        _currentCount = 1;
        
        UpdateInfo();
    }

    public void AddToStack()
    {
        _currentCount++;
        UpdateInfo();
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
        
    }

}
