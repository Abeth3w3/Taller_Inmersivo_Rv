using UnityEngine;

public class ContainerStation : Interactable
{
    public override void Interact(PlayerInventory inventory)
    {
        if (inventory.heldItem == ItemType.None)
        {
            inventory.SetItem(ItemType.EmptyContainer);
            PlaySound();
            Debug.Log("Tomaste un recipiente vacío");
        }
        else
        {
            Debug.Log("Ya tienes algo en la mano.");
        }
    }
}