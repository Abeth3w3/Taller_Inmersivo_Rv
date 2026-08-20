using UnityEngine;

public class CoffeeMachineDark : Interactable
{
    public override void Interact(PlayerInventory inventory)
    {
        if (inventory.HasItem(ItemType.EmptyCup))
        {
            inventory.SetItem(ItemType.DarkCoffee);
            Debug.Log("Preparaste un café oscuro.");
        }
        else
        {
            Debug.Log("Necesitas una taza vacía.");
        }
    }
}
