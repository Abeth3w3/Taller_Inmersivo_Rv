using System;
using UnityEngine;

public enum ItemType
{
    None,
    EmptyCup,
    DarkCoffee,
    EmptyContainer,
    MilkContainer,
    HotMilk,
    MilkCoffee
}

public class PlayerInventory : MonoBehaviour
{
    public ItemType heldItem = ItemType.None;

    public event Action<ItemType> OnItemChanged;

    public bool HasItem(ItemType item)
    {
        return heldItem == item;
    }

    public void SetItem(ItemType item)
    {
        heldItem = item;
        Debug.Log("Ahora tienes: " + heldItem);
        OnItemChanged?.Invoke(heldItem);
    }

    public void ClearItem()
    {
        heldItem = ItemType.None;
        OnItemChanged?.Invoke(heldItem);
    }
}