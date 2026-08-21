using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    public static Spawner instancia;
    public GameObject npcPrefab;
    public Transform puntoSpawn;
    public Transform pedirCafe;
    public Transform filaEspera;
    public Transform salida;
    public GameObject cafePanel;
    public TextMeshProUGUI cafeText;
    public Vector2 direccionFila = Vector2.left;
    public float espacioFila = 1.2f;
    public int maxNPCs = 5;
    public float tiempoEntreSpawns = 1.5f;

    public AudioSource audioSource;
    public AudioClip sonidoCampana;

    private List<CafeTrigger> cola = new List<CafeTrigger>();

    void Awake()
    {
        instancia = this;
    }

    void Start()
    {
        StartCoroutine(SpawnInicial());
    }

    private IEnumerator SpawnInicial()
    {
        for (int i = 0; i < maxNPCs; i++)
        {
            SpawnNPC();
            yield return new WaitForSeconds(tiempoEntreSpawns);
        }
    }

    public void SpawnNPC()
    {
        if (cola.Count >= maxNPCs) return;

        GameObject npcObj = Instantiate(npcPrefab, puntoSpawn.position, Quaternion.identity);
        CafeTrigger npc = npcObj.GetComponent<CafeTrigger>();
        npc.Configurar(pedirCafe, salida, cafePanel, cafeText);

        cola.Add(npc);
        ActualizarPosicionesFila();

        if (audioSource != null && sonidoCampana != null)
        {
            audioSource.PlayOneShot(sonidoCampana);
        }
    }

    private void ActualizarPosicionesFila()
    {
        for (int i = 0; i < cola.Count; i++)
        {
            bool esFrente = i == 0;
            Vector2 destino;

            if (esFrente)
            {
                destino = pedirCafe.position;
            }
            else
            {
                destino = (Vector2)filaEspera.position + direccionFila.normalized * espacioFila * (i - 1);
            }

            cola[i].AsignarObjetivo(destino, esFrente);
        }
    }

    public void LiberarPuestoCafe(CafeTrigger npc)
    {
        cola.Remove(npc);
        ActualizarPosicionesFila();
        SpawnNPC();
    }

    public void NPCTermino(CafeTrigger npc)
    {
    }
}