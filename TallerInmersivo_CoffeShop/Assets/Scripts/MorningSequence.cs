using System.Collections;
using UnityEngine;

public class MorningSequence : MonoBehaviour
{
    [Header("Referencias")]
    public PlayerMovement playerMovement;
    public PlayerInteract playerInteract;
    public Rigidbody2D playerRigidbody;
    public RocolaStation rocola;
    public Transform puntoRocola;
    public float velocidad = 3f;

    void Start()
    {
        if (playerMovement != null)
            playerMovement.controlHabilitado = false;

        if (playerInteract != null)
            playerInteract.controlHabilitado = false;

        if (rocola != null)
        {
            rocola.MostrarIntroYFallar(CaminarHaciaRocola);
        }
        else
        {
            TerminarSecuencia();
        }
    }

    private void CaminarHaciaRocola()
    {
        StartCoroutine(CaminarRoutine());
    }

    private IEnumerator CaminarRoutine()
    {
        if (playerRigidbody != null && puntoRocola != null)
        {
            while (((Vector2)playerRigidbody.position - (Vector2)puntoRocola.position).sqrMagnitude > 0.04f)
            {
                Vector2 nuevaPos = Vector2.MoveTowards(playerRigidbody.position, puntoRocola.position, velocidad * Time.deltaTime);
                playerRigidbody.MovePosition(nuevaPos);
                yield return null;
            }
        }

        TerminarSecuencia();
    }

    private void TerminarSecuencia()
    {
        if (playerMovement != null)
            playerMovement.controlHabilitado = true;

        if (playerInteract != null)
            playerInteract.controlHabilitado = true;
    }
}