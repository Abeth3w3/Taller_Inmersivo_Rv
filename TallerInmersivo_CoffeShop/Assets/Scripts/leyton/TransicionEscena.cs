using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TransicionEscena : MonoBehaviour
{
    public GameObject imagenTransicion;
    public float tiempoMinimoVisible = 1f;

    public void CargarEscena(string nombreEscena)
    {
        StartCoroutine(CargarConImagen(nombreEscena));
    }

    private IEnumerator CargarConImagen(string nombreEscena)
    {
        imagenTransicion.SetActive(true);

        yield return new WaitForSeconds(tiempoMinimoVisible);

        AsyncOperation carga = SceneManager.LoadSceneAsync(nombreEscena);

        while (!carga.isDone)
        {
            yield return null;
        }
    }
}