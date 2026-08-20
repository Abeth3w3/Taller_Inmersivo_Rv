using UnityEngine;
using TMPro;
using System.Collections;

// Ahora hereda de Interactable en vez de MonoBehaviour: esto hace que
// el cliente se detecte automáticamente con tu sistema de "E" existente
// (PlayerInteract → FindClosestInteractable), siempre que su Collider2D
// esté en el mismo Layer que configuraste en "Interact Layer" del Player.
public class CafeTrigger : Interactable
{
    public Transform pedirCafe;
    public Transform salida;
    public GameObject cafePanel;
    public TextMeshProUGUI cafeText;
    public float velocidad = 2f;

    private string[] frases = {
        "Buenas quiero un café con leche, muchas gracias.",
        "Buenas quiero un café negro, muchas gracias."
    };

    private Vector2 objetivoActual;
    private bool esFrente = false;
    private bool mostrando = false;
    private bool yendoASalida = false;

    // Nuevo: true cuando el cliente físicamente llegó al punto de pedir café
    // y ya está a la espera de que el jugador presione E.
    private bool listoParaAtender = false;

    void Start()
    {
        GetComponent<SpriteRenderer>().color = new Color(Random.value, Random.value, Random.value);
    }

    public void Configurar(Transform pedirCafePunto, Transform salidaPunto, GameObject cafePanelObj, TextMeshProUGUI cafeTextObj)
    {
        pedirCafe = pedirCafePunto;
        salida = salidaPunto;
        cafePanel = cafePanelObj;
        cafeText = cafeTextObj;
    }

    public void AsignarObjetivo(Vector2 destino, bool frente)
    {
        objetivoActual = destino;
        esFrente = frente;
    }

    void Update()
    {
        if (yendoASalida)
        {
            if (salida != null)
                transform.position = Vector2.MoveTowards(transform.position, salida.position, velocidad * Time.deltaTime);
            return;
        }

        transform.position = Vector2.MoveTowards(transform.position, objetivoActual, velocidad * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform == pedirCafe && esFrente && !mostrando && !yendoASalida)
        {
            // Antes: StartCoroutine(MostrarCafe()) directo aquí.
            // Ahora: solo queda "a la espera". El jugador tiene que
            // acercarse y presionar E (ver método Interact más abajo).
            listoParaAtender = true;
        }
        else if (other.transform == salida && yendoASalida)
        {
            if (Spawner.instancia != null)
            {
                Spawner.instancia.NPCTermino(this);
            }
            Destroy(gameObject);
        }
    }

    // Llamado automáticamente por PlayerInteract cuando el jugador presiona E
    // estando cerca de este cliente (igual que con la cafetera, la nevera, etc).
    public override void Interact(PlayerInventory inventory)
    {
        if (listoParaAtender && esFrente && !mostrando && !yendoASalida)
        {
            listoParaAtender = false;
            StartCoroutine(MostrarCafe());
        }
    }

    private IEnumerator MostrarCafe()
    {
        mostrando = true;
        int index = Random.Range(0, frases.Length);
        cafeText.text = frases[index];
        cafePanel.SetActive(true);

        yield return new WaitForSeconds(10f);

        cafePanel.SetActive(false);
        mostrando = false;
        yendoASalida = true;

        if (Spawner.instancia != null)
        {
            Spawner.instancia.LiberarPuestoCafe(this);
        }
    }
}