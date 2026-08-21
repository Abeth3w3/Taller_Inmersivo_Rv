using UnityEngine;

// Ponlo en el GameObject de la rocola (con su Collider2D en Is Trigger,
// en el mismo Layer que usa tu PlayerInteract para detectar Interactable).
public class RocolaStation : Interactable
{
    [Header("Audio")]
    public AudioClip attemptFailSound;   // sonido al intentar encenderla y fallar
    public AudioClip hitSound;           // sonido de golpe
    public AudioClip musicClip;          // música que suena cuando por fin enciende

    [Header("Diálogos")]
    [TextArea] public string introLine = "Es mejor empezar el día con un poco de música.";
    [TextArea] public string failLine = "Otra vez este vejestorio...";
    [TextArea] public string fixedLine = "¡Por fin! Ya suena la música.";

    [Header("Config")]
    public int hitsRequired = 4;

    private bool hasShownIntro = false;
    private bool isActive = false;
    private int hitCount = 0;

    public override void Interact(PlayerInventory inventory)
    {
        if (isActive)
        {
            DialogueUI.Instance.ShowDialogue("La música ya está sonando.");
            return;
        }

        // Primera vez: diálogo de intro -> intenta encender -> falla -> queja
        if (!hasShownIntro)
        {
            hasShownIntro = true;

            DialogueUI.Instance.ShowDialogue(introLine, () =>
            {
                AudioManager.Instance.PlaySFX(attemptFailSound);
                DialogueUI.Instance.ShowDialogue(failLine);
            });
            return;
        }

        // A partir de la segunda interacción, cada E cuenta como un golpe
        hitCount++;
        AudioManager.Instance.PlaySFX(hitSound);

        if (hitCount >= hitsRequired)
        {
            ActivateRocola();
        }
    }

    private void ActivateRocola()
    {
        isActive = true;
        AudioManager.Instance.PlayAmbient(musicClip);
        DialogueUI.Instance.ShowDialogue(fixedLine);
    }
}