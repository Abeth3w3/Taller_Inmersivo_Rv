using System;
using System.Collections;
using UnityEngine;
using TMPro;

// Crea un Canvas con un Panel (fondo) y dentro un TextMeshPro - Text (UI).
// Arrastra ambos a los campos de abajo. Pon este script en ese mismo Panel
// o en un GameObject "DialogueUI" aparte.
public class DialogueUI : MonoBehaviour
{
    public static DialogueUI Instance { get; private set; }

    public GameObject dialoguePanel;
    public TMP_Text dialogueText;
    public float displayDuration = 2.5f;

    private Coroutine currentRoutine;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);
    }

    // onComplete es opcional: sirve para encadenar un segundo diálogo
    // (como el caso de la rocola: "frase 1" -> falla -> "frase 2")
    public void ShowDialogue(string message, Action onComplete = null)
    {
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(ShowRoutine(message, onComplete));
    }

    private IEnumerator ShowRoutine(string message, Action onComplete)
    {
        if (dialoguePanel != null)
            dialoguePanel.SetActive(true);

        if (dialogueText != null)
            dialogueText.text = message;

        yield return new WaitForSeconds(displayDuration);

        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);

        onComplete?.Invoke();
    }
}