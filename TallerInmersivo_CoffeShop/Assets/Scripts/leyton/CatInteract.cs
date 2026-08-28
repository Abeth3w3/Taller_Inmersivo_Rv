using UnityEngine;

public class CatInteract : Interactable
{
    public AudioClip sonidoCaricia;

    void Awake()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
        audioSource.playOnAwake = false;
    }

    public override void Interact(PlayerInventory inventory)
    {
        PlaySound(sonidoCaricia);
    }
}