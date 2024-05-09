using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour, IInteractable, IInputable
{
    [SerializeField] private GameObject _chestUI;

    private PlayerInventory _playerInventory;
    public ChestInventory _chestInventory;

    private void Start()
    {
        _chestInventory = GetComponent<ChestInventory>();
    }

    public void Interact()
    {
        Debug.Log("Открытие инвентаря");
        
        _chestUI.SetActive(true);
        InputSystem.Instance.SetTimedInput(this);
        PlayerInventory.Instance.SetChest(_chestInventory);
    }

    public void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _chestUI.SetActive(false);
            InputSystem.Instance.ReturnInput();
            PlayerInventory.Instance.UnsetChest();
        }
    }
    
}
