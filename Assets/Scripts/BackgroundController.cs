using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] private GameObject _camera;
    [SerializeField] private float _ParallaxAmount;
    [SerializeField] private float _length;
    [SerializeField] private float _startPosition;

    void Start()
    {
        _startPosition = transform.position.x; //Fa que la posicio inicial del parallax sigui la que tingui x en aquest moment (lo que tiene es lo que es, el mundo al reves)
        _length = GetComponent<SpriteRenderer>().bounds.size.x; // pilla la mida del sprite en pixels des de el sprite renderer
    }

    void Update()
    {
        //Moviementi
        float distance = _camera.transform.position.x * _ParallaxAmount; // calcula la distancia proporcional a la camara desde el punt x on estigui
        // Aplica la distancia que ha calculat abans, es a dir mou el fondo (O linia dabaixo)
        transform.position = new Vector3(_startPosition + distance, transform.position.y, transform.position.z);

        //Bucle
        //Calcula constant ment si la camera esta a punt de passar O longo maximo do Sprite
        float temp = _camera.transform.position.x * (1 - _ParallaxAmount);
        //if camera muito lejos --> sprite palante como los de alicante
        if (temp > _startPosition + _length)
        {
            _startPosition += _length;
        }
        //si camera move muito atras --> Sprite Atras Satanas
        else if (temp < _startPosition - _length)
        {
            _startPosition -= _length;
        }

    }
}
