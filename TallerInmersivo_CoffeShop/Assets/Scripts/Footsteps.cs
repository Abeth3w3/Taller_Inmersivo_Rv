using UnityEngine;

// Ponlo en el mismo GameObject que tu PlayerMovement.
// No modifica ese script, solo lee la velocidad del Rigidbody2D.
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerFootsteps : MonoBehaviour
{
    public AudioClip[] footstepClips;
    [Range(0f, 1f)] public float volume = 0.6f;
    public float stepInterval = 0.35f; // tiempo entre pasos

    private Rigidbody2D rb;
    private float stepTimer;
    private const float moveThreshold = 0.05f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // NOTA: si tu proyecto usa una versión de Unity donde Rigidbody2D.velocity
        // aún NO fue renombrado a linearVelocity, cambia esta línea por rb.velocity.
        bool isMoving = rb.linearVelocity.sqrMagnitude > moveThreshold * moveThreshold;

        if (isMoving)
        {
            stepTimer -= Time.deltaTime;
            if (stepTimer <= 0f)
            {
                PlayFootstep();
                stepTimer = stepInterval;
            }
        }
        else
        {
            stepTimer = 0f; // así el primer paso al volver a moverse suena de inmediato
        }
    }

    private void PlayFootstep()
    {
        if (footstepClips == null || footstepClips.Length == 0) return;
        if (AudioManager.Instance == null) return;

        AudioClip clip = footstepClips[Random.Range(0, footstepClips.Length)];
        AudioManager.Instance.PlaySFX(clip, volume);
    }
}