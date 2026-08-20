using UnityEngine;

public class FridgeStation : Interactable
{
    public override void Interact(PlayerInventory inventory)
    {
        if (inventory.HasItem(ItemType.EmptyContainer))
        {
            inventory.SetItem(ItemType.MilkContainer);
            Debug.Log("Llenaste el recipiente con leche.");
        }
        else
        {
            Debug.Log("Necesitas un recipiente vacío.");
        }
    }
}
