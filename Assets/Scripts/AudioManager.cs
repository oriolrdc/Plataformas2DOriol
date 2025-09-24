using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }
    [SerializeField] AudioSource _bgmSource;
    [SerializeField] AudioSource _sfxSource;

    public AudioClip manuBGM;
    public AudioClip gameBGM;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
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
