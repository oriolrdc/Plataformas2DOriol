using UnityEngine;

public class Star : MonoBehaviour
{
    //GameManager _gameManager;
    [SerializeField] AudioClip _starSFX;
    void Awake()
    {
        //_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void Interaction()
    {
        //_gameManager.AddStar();
        GameManager.Instance.AddStar();
        AudioManager.Instance.ReproduceSound(_starSFX);
        Destroy(gameObject);
    }
}
