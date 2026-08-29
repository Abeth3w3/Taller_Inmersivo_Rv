using System;
using UnityEngine;

public class RocolaStation : Interactable
{
    [Header("Audio")]
    public AudioClip attemptFailSound;
    public AudioClip hitSound;
    public AudioClip musicClip;

    [Header("Dialogos")]
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

        if (!hasShownIntro)
        {
            MostrarIntroYFallar(null);
            return;
        }

        hitCount++;
        AudioManager.Instance.PlaySFX(hitSound);

        if (hitCount >= hitsRequired)
        {
            ActivateRocola();
        }
    }

    public void MostrarIntroYFallar(Action alTerminar)
    {
        hasShownIntro = true;

        DialogueUI.Instance.ShowDialogue(introLine, () =>
        {
            AudioManager.Instance.PlaySFX(attemptFailSound);
            DialogueUI.Instance.ShowDialogue(failLine, alTerminar);
        });
    }

    private void ActivateRocola()
    {
        isActive = true;
        AudioManager.Instance.PlayAmbient(musicClip);
        DialogueUI.Instance.ShowDialogue(fixedLine);

        if (Spawner.instancia != null)
        {
            Spawner.instancia.IniciarSpawn();
        }
    }
}