using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public Transform interactionPoint;
    public float interactionRadius = 0.6f;
    public LayerMask interactLayer;

    // Ahora se asigna desde el Inspector
    public PlayerInventory inventory;

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
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (inventory == null)
            {
                Debug.LogError("Inventory es NULL");
                return;
            }

            Collider2D[] hits = Physics2D.OverlapCircleAll(
                interactionPoint.position,
                interactionRadius,
                interactLayer);

            if (hits.Length == 0)
            {
                Debug.Log("No detecté nada");
                return;
            }

            foreach (Collider2D hit in hits)
            {
                Debug.Log("Detecté: " + hit.name);

                Interactable interactable = hit.GetComponent<Interactable>();

                if (interactable != null)
                {
                    interactable.Interact(inventory);
                    return;
                }
            }

            Debug.Log("Detecté colliders, pero ninguno tiene Interactable");
        }
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
