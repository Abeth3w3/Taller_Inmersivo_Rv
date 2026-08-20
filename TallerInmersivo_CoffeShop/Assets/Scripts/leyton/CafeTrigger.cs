using UnityEngine;
using TMPro;
using System.Collections;

public class CafeTrigger : MonoBehaviour
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
            StartCoroutine(MostrarCafe());
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