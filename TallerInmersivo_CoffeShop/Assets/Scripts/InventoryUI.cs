using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public PlayerInventory inventory;
    public Image icono;

    [Header("Sprites por item")]
    public Sprite spriteTazaVacia;
    public Sprite spriteCafeNegro;
    public Sprite spriteRecipienteVacio;
    public Sprite spriteLecheFria;
    public Sprite spriteLecheCaliente;
    public Sprite spriteCafeConLeche;

    void OnEnable()
    {
        if (inventory != null)
        {
            inventory.OnItemChanged += Actualizar;
            Actualizar(inventory.heldItem);
        }
    }

    void OnDisable()
    {
        if (inventory != null)
        {
            inventory.OnItemChanged -= Actualizar;
        }
    }

    private void Actualizar(ItemType item)
    {
        if (icono == null) return;

        switch (item)
        {
            case ItemType.EmptyCup:
                icono.sprite = spriteTazaVacia;
                break;
            case ItemType.DarkCoffee:
                icono.sprite = spriteCafeNegro;
                break;
            case ItemType.EmptyContainer:
                icono.sprite = spriteRecipienteVacio;
                break;
            case ItemType.MilkContainer:
                icono.sprite = spriteLecheFria;
                break;
            case ItemType.HotMilk:
                icono.sprite = spriteLecheCaliente;
                break;
            case ItemType.MilkCoffee:
                icono.sprite = spriteCafeConLeche;
                break;
            default:
                icono.sprite = null;
                break;
        }

        icono.enabled = icono.sprite != null;
    }
}