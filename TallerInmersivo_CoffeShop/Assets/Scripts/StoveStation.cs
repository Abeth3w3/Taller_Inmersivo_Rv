using UnityEngine;
using System.Collections;

public class StoveStation : Interactable
{
    private enum Estado { Vacia, Calentando, Lista }
    private Estado estado = Estado.Vacia;

    [Header("Tiempo de calentado")]
    public float tiempoCalentado = 4f;

    [Header("Sonidos (opcionales, uno por cada paso)")]
    public AudioClip sonidoInteractuar;
    public AudioClip sonidoColocarRecipiente;
    public AudioClip sonidoListo;
    public AudioClip sonidoRecoger;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;
    }

    public override void Interact(PlayerInventory inventory)
    {
        PlaySound(sonidoInteractuar);

        switch (estado)
        {
            case Estado.Vacia:
                if (inventory.HasItem(ItemType.MilkContainer))
                {
                    inventory.ClearItem();
                    estado = Estado.Calentando;
                    PlaySound(sonidoColocarRecipiente);
                    Debug.Log("Pusiste el recipiente con leche en la estufa.");
                    StartCoroutine(Calentar());
                }
                else
                {
                    Debug.Log("Necesitas un recipiente con leche para usar la estufa.");
                }
                break;

            case Estado.Calentando:
                Debug.Log("La leche todavía se está calentando...");
                break;

            case Estado.Lista:
                if (inventory.heldItem == ItemType.None)
                {
                    inventory.SetItem(ItemType.HotMilk);
                    estado = Estado.Vacia;
                    PlaySound(sonidoRecoger);
                    Debug.Log("Recogiste la leche caliente.");
                }
                else
                {
                    Debug.Log("Tienes las manos ocupadas.");
                }
                break;
        }
    }

    private IEnumerator Calentar()
    {
        yield return new WaitForSeconds(tiempoCalentado);
        estado = Estado.Lista;
        PlaySound(sonidoListo);
        Debug.Log("¡La leche está caliente y lista!");
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip == null) return;
        audioSource.PlayOneShot(clip);
    }
}