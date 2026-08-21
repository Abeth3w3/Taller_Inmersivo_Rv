using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void IniciarJuego()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void MostrarInstrucciones(GameObject panelInstrucciones)
    {
        panelInstrucciones.SetActive(true);
    }

    public void CerrarInstrucciones(GameObject panelInstrucciones)
    {
        panelInstrucciones.SetActive(false);
    }

    public void SalirJuego()
    {
        Application.Quit();
    }
}
