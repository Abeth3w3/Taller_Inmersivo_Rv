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

        if (interactionPoint == null)
            Debug.LogError("No encontré el InteractionPoint (revisa que exista un hijo llamado exactamente 'InteractionPoint')");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Presionaste E");

            if (inventory == null)
            {
                Debug.LogError("Inventory es NULL");
                return;
            }

            if (interactionPoint == null)
            {
                Debug.LogError("InteractionPoint es NULL");
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

            // Buscamos el Interactable más cercano DE VERDAD:
            // usamos ClosestPoint del propio collider (borde real),
            // no el pivote del objeto, para que un collider grande
            // (como el de una estantería) no "robe" la interacción
            // a algo que está físicamente más cerca (como la rocola).
            Interactable closest = null;
            float closestDist = float.MaxValue;

            foreach (Collider2D hit in hits)
            {
                Debug.Log("Detecté: " + hit.name);

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

            if (closest != null)
            {
                closest.Interact(inventory);
            }
            else
            {
                Debug.Log("Detecté colliders, pero ninguno tiene Interactable");
            }
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