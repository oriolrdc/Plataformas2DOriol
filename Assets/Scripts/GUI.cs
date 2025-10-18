using UnityEngine;
using UnityEngine.UI;
public class GUI : MonoBehaviour
{
    public static GUI Instance;
    public GameObject _pauseCanvas;
    public GameObject _winCanvas;
    [SerializeField] private Image _heathBar;
    private int _stars;
    [SerializeField] private Text _starCounter;
    private int _coins;
    [SerializeField] private Text _coinCounter;

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
    }
    public void ChangeCanvasStatus(GameObject canvas, bool status)
    {
        canvas.SetActive(status);
    }

    public void Resume()
    {
        GameManager.Instance.Pause();
    }

    public void ChangeScene(string sceneName)
    {
        SceneLoader.Instance.ChangeScene(sceneName);
    }

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        _heathBar.fillAmount = currentHealth / maxHealth;
    }

    public void UpdateStars()
    {
        _stars++;
        _starCounter.text = "0" + _stars.ToString();
    }

    public void UpdateCoin()
    {
        _coins++;
        _coinCounter.text = "0" + _coins.ToString();
    }
}
