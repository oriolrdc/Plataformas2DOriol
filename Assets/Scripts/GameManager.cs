using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    //Variable Static o estatica, se puede acceder a ellas desde cualquier otro script sin tener que hace getComponent y esas shits
    //el get private set hace que sea privada para los demas pero publica para este, cositas
    public static GameManager instance { get; private set; }

    [SerializeField] private GameObject _pauseCanvas;
    [SerializeField] InputActionAsset playerInputs;
    private InputAction _pauseInput;
    private bool _isPaused = false;
    int _stars = 0;

    void Awake()
    {
        //This is singleton jeje
        //este if busca si ya esta lleno instance, si ya esta lleno comprueba si lo de dentro es este objeto o otro, si es otro se destruye y sino pues rellena instance con el mismo.
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        _pauseInput = InputSystem.actions["Pause"];

        //cuando cambias de escena esto no se destruye, un saludazo
        DontDestroyOnLoad(gameObject);

    }

    void Start()
    {
        AudioManager.instance.ChangeBGM(AudioManager.instance.gameBGM);

    }

    void Update()
    {
        if (_pauseInput.WasPressedThisFrame())
        {
            Pause();
        }
    }

    public void AddStar()
    {
        _stars++;
        Debug.Log("Estrellas recogidas: " + _stars);
    }

    public void Pause()
    {
        if (_isPaused)
        {
            Time.timeScale = 1;
            _pauseCanvas.SetActive(false);
            playerInputs.FindActionMap("Player").Enable();
            _isPaused = false;
        }
        else
        {
            Time.timeScale = 0;
            _pauseCanvas.SetActive(true);
            playerInputs.FindActionMap("Player").Disable();
            _isPaused = true;
        }
        
    }
}
