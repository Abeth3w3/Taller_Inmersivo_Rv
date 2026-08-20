using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public Transform interactionPoint;
    public float interactionRadius = 0.6f;
    public LayerMask interactLayer;

    
    public PlayerInventory inventory;

    [Header("Indicador visual 'presiona E'")]
    [Tooltip("Un único GameObject (sprite o Text-TMP) que se muestra/oculta y se posiciona automáticamente sobre lo que esté cerca. No hace falta crear uno por cada estación.")]
    public GameObject interactionPrompt;
    public Vector3 promptOffset = new Vector3(0f, 0.6f, 0f);

    private Interactable currentTarget;

    void Awake()
    {
        if (interactionPoint == null)
            interactionPoint = transform.Find("InteractionPoint");

        // Fallback: si no se asignó manualmente en el Inspector,
        // intenta encontrarlo en el mismo GameObject.
        if (inventory == null)
            inventory = GetComponent<PlayerInventory>();

        if (inventory == null)
            Debug.LogError("No asignaste PlayerInventory en el Inspector");

        if (interactionPoint == null)
            Debug.LogError("No encontré el InteractionPoint (revisa que exista un hijo llamado exactamente 'InteractionPoint')");

        if (interactionPrompt != null)
            interactionPrompt.SetActive(false);
    }

    void Update()
    {
        UpdateInteractionPrompt();

        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
        }
    }

    // Revisa cada frame si hay un Interactable cerca, y si lo hay,
    // muestra el ícono "E" flotando encima de ese objeto.
    private void UpdateInteractionPrompt()
    {
        if (interactionPoint == null) return;

        Interactable nearest = FindClosestInteractable();
        currentTarget = nearest;

        if (interactionPrompt == null) return;

        if (nearest != null)
        {
            interactionPrompt.SetActive(true);
            interactionPrompt.transform.position = nearest.transform.position + promptOffset;
        }
        else
        {
            interactionPrompt.SetActive(false);
        }
    }

    private void TryInteract()
    {
        if (inventory == null)
        {
            Debug.LogError("Inventory es NULL");
            return;
        }

        if (currentTarget == null)
        {
            Debug.Log("No detecté nada");
            return;
        }

        currentTarget.Interact(inventory);
    }

    // Busca, entre todos los colliders detectados, el Interactable
    // cuyo borde real (ClosestPoint) esté más cerca del InteractionPoint.
    private Interactable FindClosestInteractable()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            interactionPoint.position,
            interactionRadius,
            interactLayer);

        Interactable closest = null;
        float closestDist = float.MaxValue;

        foreach (Collider2D hit in hits)
        {
            Interactable interactable = hit.GetComponent<Interactable>();
            if (interactable == null) continue;

            Vector2 nearestPoint = hit.ClosestPoint(interactionPoint.position);
            float dist = Vector2.Distance(interactionPoint.position, nearestPoint);

            if (dist < closestDist)
            {
                closestDist = dist;
                closest = interactable;
            }
        }

        return closest;
    }

    private void OnDrawGizmosSelected()
    {
        if (interactionPoint == null)
            interactionPoint = transform.Find("InteractionPoint");

        if (interactionPoint == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionPoint.position, interactionRadius);
    }
}