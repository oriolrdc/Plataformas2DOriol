using UnityEngine;
using System.Collections.Generic;

public class Repaso : MonoBehaviour
{
    public int variableInt = 4;
    [SerializeField] private int _variableInt = 6;
    public float variableFloat = 0.5f;
    public string variableString = "Silksong ha salido";
    public bool variableBool = true;
    public int[] arrayInt = new int[5] {4, 7, 6, 9, 10}; //esto del array es una caja que tiene cajas dentro rollo carpeta sabes? tipo ahora mismo crea 5 cajas
    // este array tiene 5 valores que he declarado con {} poniendo 5 numeros pq son ints
    public List<int> listInt = new List<int>(9) {99, 7, 4}; //esto es una lista de ints, ahora mismo de 9 elementos
    //al declarar los valores de la lista no dan error pq puede aumentar y disminuir en numero



    void Start()
    {
        int numero = 5;

        if (numero == 7)
        {
            //
        }
        else if (numero == 3)
        {
            //
        }
        else
        {
            //
        }

        /*for (int i = 0; i < length; i++)
        {

        }*/
        
        /*foreach (var item in collection)
        {
            
        }*/
    }

    void Update()
    {
        
    }
}
