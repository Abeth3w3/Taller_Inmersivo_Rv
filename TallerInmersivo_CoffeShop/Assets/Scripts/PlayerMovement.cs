using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 3.5f;
    public float interactionDistance = 0.5f;
    public bool controlHabilitado = true;

    private Rigidbody2D rb;
    private Vector2 input;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Transform interactionPoint;

    private Vector2 facingDirection = Vector2.right;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        interactionPoint = transform.Find("InteractionPoint");
    }

    void Update()
    {
        if (!controlHabilitado)
        {
            input = Vector2.zero;

            if (animator != null)
            {
                animator.SetBool("IsMoving", false);
            }

            return;
        }

        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        input = input.normalized;

        if (input != Vector2.zero)
        {
            facingDirection = input;
        }

        if (animator != null)
        {
            animator.SetFloat("MoveX", Mathf.Abs(facingDirection.x));
            animator.SetFloat("MoveY", facingDirection.y);
            animator.SetBool("IsMoving", input != Vector2.zero);
        }

        if (spriteRenderer != null)
        {
            if (facingDirection.x < -0.1f)
                spriteRenderer.flipX = true;
            else if (facingDirection.x > 0.1f)
                spriteRenderer.flipX = false;
        }

        if (interactionPoint != null)
        {
            Vector2 dir = facingDirection;

            if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
                dir = new Vector2(Mathf.Sign(dir.x), 0f);
            else
                dir = new Vector2(0f, Mathf.Sign(dir.y));

            interactionPoint.position = (Vector2)transform.position + dir * interactionDistance;
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + input * speed * Time.fixedDeltaTime);
    }
}