using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject panelInstrucciones;
    public AudioSource musicaFondo;
    private bool musicaActiva = true;

    public void IniciarJuego()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void MostrarInstrucciones()
    {
        panelInstrucciones.SetActive(true);
    }

    public void CerrarInstrucciones()
    {
        panelInstrucciones.SetActive(false);
    }

    public void SalirJuego()
    {
        Application.Quit();
    }

    public void ToggleMusica()
    {
        musicaActiva = !musicaActiva;
        if (musicaActiva)
        {
            musicaFondo.Play();
        }
        else
        {
            musicaFondo.Pause();
        }
    }
}
