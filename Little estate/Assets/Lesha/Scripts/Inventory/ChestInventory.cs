using UnityEngine;

public class ChestInventory : InventoryManager, IInventory
{
    public override void SelectSlot(InventorySlot slot)
    {
       
        if (PlayerInventory.Instance == null)
            return;
        
        if (PlayerInventory.Instance.GetSlot(slot))
            slot.DropItem();
    }
}
