using UnityEngine;

public class FridgeStation : Interactable
{
    [Header("Sonidos")]
    public AudioClip sonidoInteractuar;
    public AudioClip sonidoLlenar;

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

        if (inventory.HasItem(ItemType.EmptyContainer))
        {
            inventory.SetItem(ItemType.MilkContainer);
            PlaySound(sonidoLlenar);
            Debug.Log("Llenaste el recipiente con leche.");
        }
        else
        {
            Debug.Log("Necesitas un recipiente vacío.");
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip == null) return;
        audioSource.PlayOneShot(clip);
    }
}