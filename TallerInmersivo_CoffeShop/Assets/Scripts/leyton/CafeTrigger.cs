using UnityEngine;
using TMPro;
using System.Collections;

public class CafeTrigger : Interactable
{
    [Header("Pedido")]
    public ItemType pedido;

    [Header("Referencias")]
    public Transform pedirCafe;
    public Transform salida;
    public GameObject cafePanel;
    public TextMeshProUGUI cafeText;
    public float velocidad = 2f;

    [Header("Sonidos (opcionales)")]
    public AudioClip sonidoPedido;
    public AudioClip sonidoEntregaCorrecta;
    public AudioClip sonidoEntregaIncorrecta;

    private Vector2 objetivoActual;
    private bool esFrente = false;
    private bool yendoASalida = false;
    private bool listoParaAtender = false;
    private bool atendido = false;

    private int layerNormal;
    private int layerInteraccion;

    void Awake()
    {
        layerNormal = gameObject.layer;
        layerInteraccion = LayerMask.NameToLayer("Interaction");
        pedido = (Random.value < 0.5f) ? ItemType.DarkCoffee : ItemType.MilkCoffee;
    }

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

        if (!frente)
        {
            gameObject.layer = layerNormal;
            listoParaAtender = false;
        }
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
        if (other.transform == pedirCafe && esFrente && !atendido && !yendoASalida)
        {
            listoParaAtender = true;
            gameObject.layer = layerInteraccion;
            MostrarPedido();
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

    private void MostrarPedido()
    {
        if (cafeText != null) cafeText.text = TextoPedido(pedido);
        if (cafePanel != null) cafePanel.SetActive(true);

        PlaySound(sonidoPedido);
        CoffeeProgressEvents.DispararNuevoPedido(pedido);
    }

    private string TextoPedido(ItemType tipo)
    {
        switch (tipo)
        {
            case ItemType.DarkCoffee: return "Buenas, quiero un café negro, muchas gracias.";
            case ItemType.MilkCoffee: return "Buenas, quiero un café con leche, muchas gracias.";
            default: return "";
        }
    }

    public override void Interact(PlayerInventory inventory)
    {
        if (!listoParaAtender || !esFrente || atendido || yendoASalida) return;

        if (inventory.heldItem == pedido)
        {
            atendido = true;
            listoParaAtender = false;
            inventory.ClearItem();
            gameObject.layer = layerNormal;

            if (cafePanel != null) cafePanel.SetActive(false);

            PlaySound(sonidoEntregaCorrecta);
            CoffeeProgressEvents.DispararPedidoEntregado();
            Debug.Log("¡Gracias! Este es justo el café que pedí.");

            yendoASalida = true;

            if (Spawner.instancia != null)
            {
                Spawner.instancia.LiberarPuestoCafe(this);
            }
        }
        else
        {
            PlaySound(sonidoEntregaIncorrecta);
            Debug.Log("Ese no es el café que pedí.");
        }
    }
}