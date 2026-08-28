using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [Header("Sonido (opcional)")]
    [Tooltip("AudioSource que reproducirá los sonidos de esta estación. Si se deja vacío, simplemente no suena nada (no da error).")]
    public AudioSource audioSource;

    [Tooltip("Sonido por defecto de esta estación. Las estaciones con varias acciones (ej. colocar / preparar / recoger) pueden tener sus propios AudioClip y pasarlos a PlaySound(clip).")]
    public AudioClip interactSound;

    public abstract void Interact(PlayerInventory inventory);

    protected void PlaySound(AudioClip clip = null)
    {
        if (audioSource == null) return;

        AudioClip clipAReproducir = clip != null ? clip : interactSound;
        if (clipAReproducir != null)
        {
            audioSource.PlayOneShot(clipAReproducir);
        }
    }
}