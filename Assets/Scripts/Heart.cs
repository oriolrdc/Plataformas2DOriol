using UnityEngine;

public class Heart : MonoBehaviour
{
    [SerializeField] private AudioClip _HeartSFX;

    public void Heal()
    {
        AudioManager.Instance.ReproduceSound(_HeartSFX);
        Destroy(gameObject);
    } 
}
