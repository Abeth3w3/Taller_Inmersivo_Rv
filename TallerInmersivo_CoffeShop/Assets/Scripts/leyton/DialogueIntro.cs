using UnityEngine;
using TMPro;

[System.Serializable]
public class LineaDialogo
{
    public string nombrePersonaje;
    [TextArea(2, 4)]
    public string texto;
    public Sprite retrato;
}

public class DialogueIntro : MonoBehaviour
{
    [Header("UI")]
    public GameObject panelDialogo;
    public TextMeshProUGUI textoNombre;
    public TextMeshProUGUI textoDialogo;
    public UnityEngine.UI.Image imagenRetrato;

    [Header("Contenido")]
    public LineaDialogo[] lineas;

    [Header("Transición")]
    public string escenaSiguiente = "SampleScene";
    public TransicionEscena transicion;

    private int indiceActual = 0;

    void Start()
    {
        panelDialogo.SetActive(true);
        MostrarLinea();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
        {
            AvanzarDialogo();
        }
    }

    private void MostrarLinea()
    {
        LineaDialogo linea = lineas[indiceActual];
        textoNombre.text = linea.nombrePersonaje;
        textoDialogo.text = linea.texto;

        if (imagenRetrato != null)
        {
            if (linea.retrato != null)
            {
                imagenRetrato.sprite = linea.retrato;
                imagenRetrato.enabled = true;
            }
            else
            {
                imagenRetrato.enabled = false;
            }
        }
    }

    private void AvanzarDialogo()
    {
        indiceActual++;

        if (indiceActual >= lineas.Length)
        {
            TerminarDialogo();
        }
        else
        {
            MostrarLinea();
        }
    }

    private void TerminarDialogo()
    {
        panelDialogo.SetActive(false);
        transicion.CargarEscena(escenaSiguiente);
    }
}