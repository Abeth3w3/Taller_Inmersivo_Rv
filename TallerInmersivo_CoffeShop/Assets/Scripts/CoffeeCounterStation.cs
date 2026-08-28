using UnityEngine;

public class CoffeeCounterStation : Interactable
{
    private ItemType itemEnMesa = ItemType.None;

    [Header("Sonidos (opcionales, uno por cada paso)")]
    public AudioClip sonidoColocarCafe;
    public AudioClip sonidoVerterLeche;
    public AudioClip sonidoRecoger;

    public override void Interact(PlayerInventory inventory)
    {
        if (itemEnMesa == ItemType.None && inventory.HasItem(ItemType.DarkCoffee))
        {
            itemEnMesa = ItemType.DarkCoffee;
            inventory.ClearItem();
            PlaySound(sonidoColocarCafe);
            Debug.Log("Pusiste el café negro en la mesa.");
            return;
        }

        if (itemEnMesa == ItemType.DarkCoffee && inventory.HasItem(ItemType.HotMilk))
        {
            itemEnMesa = ItemType.MilkCoffee;
            inventory.ClearItem();
            PlaySound(sonidoVerterLeche);
            Debug.Log("Agregaste la leche caliente. ¡Café con leche listo!");
            return;
        }

        if (itemEnMesa == ItemType.MilkCoffee && inventory.heldItem == ItemType.None)
        {
            inventory.SetItem(ItemType.MilkCoffee);
            itemEnMesa = ItemType.None;
            PlaySound(sonidoRecoger);
            Debug.Log("Recogiste el café con leche.");
            return;
        }

        if (itemEnMesa == ItemType.None)
        {
            Debug.Log("Necesitas traer un café negro primero.");
        }
        else if (itemEnMesa == ItemType.DarkCoffee)
        {
            Debug.Log("Necesitas leche caliente para agregar al café.");
        }
        else
        {
            Debug.Log("Ya hay un café con leche listo, pero tienes las manos ocupadas.");
        }
    }
}