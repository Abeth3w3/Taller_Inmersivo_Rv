using UnityEngine;

public class CoffeeMachineMilk : Interactable
{
    private bool hasMilk = false;

    public override void Interact(PlayerInventory inventory)
    {
        // Depositar leche
        if (inventory.HasItem(ItemType.MilkContainer))
        {
            hasMilk = true;
            inventory.SetItem(ItemType.EmptyContainer);

            Debug.Log("Vertiste la leche en la cafetera.");
            return;
        }

        // Preparar café con leche
        if (hasMilk && inventory.HasItem(ItemType.EmptyCup))
        {
            inventory.SetItem(ItemType.MilkCoffee);
            hasMilk = false;

            Debug.Log("Preparaste un café con leche.");
            return;
        }

        if (!hasMilk)
        {
            Debug.Log("La cafetera necesita leche primero.");
        }
        else
        {
            Debug.Log("Necesitas una taza vacía.");
        }
    }
}
