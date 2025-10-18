using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    [SerializeField] AudioSource _bgmSource;
    [SerializeField] AudioSource _sfxSource;

    public AudioClip manuBGM;
    public AudioClip gameBGM;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {

    }

    public void ReproduceSound(AudioClip clip)
    {
        _sfxSource.PlayOneShot(clip);
    }

    public void ChangeBGM(AudioClip bgmClip)
    {
        _bgmSource.clip = bgmClip;
        _bgmSource.Play();
    }
}
