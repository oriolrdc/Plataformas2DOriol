using UnityEngine;
using UnityEngine.UI;
public class GUI : MonoBehaviour
{
    public static GUI Instance;
    public GameObject _pauseCanvas;
    [SerializeField] private Image _heathBar;

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
        GameManager.instance.Pause();
    }

    public void ChangeScene(string sceneName)
    {
        SceneLoader.Instance.ChangeScene(sceneName);
    }

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        _heathBar.fillAmount = currentHealth / maxHealth;
    }
}
