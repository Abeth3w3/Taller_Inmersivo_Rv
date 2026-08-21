using UnityEngine;

public class CatInteract : Interactable
{
    public AudioClip sonidoCaricia;
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
        if (sonidoCaricia == null) return;
        audioSource.PlayOneShot(sonidoCaricia);
    }
}