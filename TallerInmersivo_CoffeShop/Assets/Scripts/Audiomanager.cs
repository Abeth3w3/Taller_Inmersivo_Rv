using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Fuentes de audio")]
    public AudioSource sfxSource;
    public AudioSource ambientSource;

    [Header("Sonido ambiental por defecto (opcional)")]
    public AudioClip defaultAmbientClip;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (sfxSource == null)
            sfxSource = gameObject.AddComponent<AudioSource>();

        if (ambientSource == null)
        {
            ambientSource = gameObject.AddComponent<AudioSource>();
            ambientSource.loop = true;
        }
    }

    void Start()
    {
        if (defaultAmbientClip != null)
            PlayAmbient(defaultAmbientClip);
    }

    // Sonidos cortos: pasos, golpes, interacciones, etc.
    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if (clip == null || sfxSource == null) return;
        sfxSource.PlayOneShot(clip, volume);
    }

    // Sonido ambiental o música (se reproduce en loop)
    public void PlayAmbient(AudioClip clip, bool loop = true)
    {
        if (clip == null || ambientSource == null) return;

        if (ambientSource.clip == clip && ambientSource.isPlaying)
            return;

        ambientSource.clip = clip;
        ambientSource.loop = loop;
        ambientSource.Play();
    }

    public void StopAmbient()
    {
        if (ambientSource != null)
            ambientSource.Stop();
    }
}