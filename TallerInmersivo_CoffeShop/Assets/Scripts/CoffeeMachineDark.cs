using UnityEngine;
using System.Collections;

public class CoffeeMachineDark : Interactable
{
    private enum Estado { Vacia, Preparando, Lista }
    private Estado estado = Estado.Vacia;

    [Header("Tiempo de preparación")]
    public float tiempoPreparacion = 4f;

    [Header("Sonidos (opcionales, uno por cada paso)")]
    public AudioClip sonidoColocarTaza;
    public AudioClip sonidoListo;
    public AudioClip sonidoRecoger;

    public override void Interact(PlayerInventory inventory)
    {
        switch (estado)
        {
            case Estado.Vacia:
                if (inventory.HasItem(ItemType.EmptyCup))
                {
                    inventory.ClearItem();
                    estado = Estado.Preparando;
                    PlaySound(sonidoColocarTaza);
                    CoffeeProgressEvents.DispararCafeNegroEnPreparacion();
                    Debug.Log("Colocaste la taza en la cafetera. Empezando a preparar...");
                    StartCoroutine(Preparar());
                }
                else
                {
                    Debug.Log("Necesitas una taza vacía para usar la cafetera.");
                }
                break;

            case Estado.Preparando:
                Debug.Log("El café todavía se está preparando...");
                break;

            case Estado.Lista:
                if (inventory.heldItem == ItemType.None)
                {
                    inventory.SetItem(ItemType.DarkCoffee);
                    estado = Estado.Vacia;
                    PlaySound(sonidoRecoger);
                    CoffeeProgressEvents.DispararCafeNegroRecogido();
                    Debug.Log("Recogiste el café negro.");
                }
                else
                {
                    Debug.Log("Tienes las manos ocupadas.");
                }
                break;
        }
    }

    private IEnumerator Preparar()
    {
        yield return new WaitForSeconds(tiempoPreparacion);
        estado = Estado.Lista;
        PlaySound(sonidoListo);
        Debug.Log("¡El café negro está listo para recoger!");
    }
}