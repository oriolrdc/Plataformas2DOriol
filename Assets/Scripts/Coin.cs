using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private AudioClip _coinSFX;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            CoinInteraction();
        }
    }

    void CoinInteraction()
    {
        AudioManager.Instance.ReproduceSound(_coinSFX);
        GameManager.Instance.AddCoin();
        Destroy(gameObject);
    }
}
