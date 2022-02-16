using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] int MAX_FILAS = 6;
    [SerializeField] int MAX_COLUMNAS = 8;

    [SerializeField] Transform posicionInicial;
    public GameObject[,] Celdas;
    public GameObject Square;

    private void Awake()
    {
        Celdas = new GameObject[MAX_FILAS, MAX_COLUMNAS];
        //Inicializar tabla de posiciones

        for (int i = 0; i < MAX_FILAS; i++)
        {
            for (int j = 0; j < MAX_COLUMNAS; j++)
            {
                //Celdas[i, j] = new GameObject();

                Celdas[i, j] = Instantiate(Square);
                Celdas[i, j].transform.position = new Vector3(posicionInicial.position.x + j, posicionInicial.position.y - i, posicionInicial.position.z);
                Celdas[i, j].transform.parent = this.transform;
                Celdas[i, j].name = "Celda_" + i + "-" + j;



            }
        }
    }

}