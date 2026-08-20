using UnityEngine;
// Ponlo en el mismo GameObject que tu PlayerMovement.
// No modifica ese script, solo lee la posición del Rigidbody2D.
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerFootsteps : MonoBehaviour
{
    public AudioClip[] footstepClips;
    [Range(0f, 1f)] public float volume = 0.6f;
    public float stepInterval = 0.35f; // tiempo entre pasos

    private Rigidbody2D rb;
    private float stepTimer;
    private Vector2 lastPosition;

    // Distancia mínima recorrida por frame para considerar que se está moviendo.
    // Ajusta este valor si los pasos suenan de más o de menos.
    private const float moveThreshold = 0.001f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        lastPosition = rb.position;
    }

    void FixedUpdate()
    {
        // Medimos por posición en vez de por velocity: funciona igual
        // tanto si el Rigidbody2D es Dynamic como Kinematic (MovePosition
        // no actualiza linearVelocity/velocity en modo Kinematic).
        float distanceMoved = Vector2.Distance(rb.position, lastPosition);
        bool isMoving = distanceMoved > moveThreshold;
        lastPosition = rb.position;

        if (isMoving)
        {
            stepTimer -= Time.fixedDeltaTime;
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

        if (AudioManager.Instance == null)
        {
            Debug.LogWarning("AudioManager.Instance es null. ¿Existe el GameObject 'AudioManager' en la escena?");
            return;
        }

        AudioClip clip = footstepClips[Random.Range(0, footstepClips.Length)];
        AudioManager.Instance.PlaySFX(clip, volume);
    }
}