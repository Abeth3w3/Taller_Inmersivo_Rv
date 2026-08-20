using UnityEngine;

public class EmptyCupStation : Interactable
{
    public override void Interact(PlayerInventory inventory)
    {
        if (inventory == null)
        {
            Debug.LogError("El inventory llegó NULL a EmptyCupStation");
            return;
        }

        if (inventory.heldItem == ItemType.None)
        {
            inventory.SetItem(ItemType.EmptyCup);
            Debug.Log("Tomaste una taza vacía");
        }
        else
        {
            Debug.Log("Ya tienes algo en la mano");
        }
    }
}
