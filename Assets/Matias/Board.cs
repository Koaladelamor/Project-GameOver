using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    static Board m_instance = null;

    [SerializeField] int MAX_FILAS = 5;
    [SerializeField] int MAX_COLUMNAS = 10;

    [SerializeField] Transform m_initialPosition;
    public GameObject[,] m_tiles;
    public GameObject m_tilePrefab;

    float m_tileSize;
    bool m_hasBlackBeenPainted = false;

    private void Awake()
    {
        if(m_instance == null) { m_instance = this; }
        else { Destroy(this.gameObject); }

        m_tiles = new GameObject[MAX_FILAS, MAX_COLUMNAS];
        //Inicializar tabla de posiciones

        for (int i = 0; i < MAX_FILAS; i++)
        {
            for (int j = 0; j < MAX_COLUMNAS; j++)
            {
                //Celdas[i, j] = new GameObject();

                m_tiles[i, j] = Instantiate(m_tilePrefab);
                m_tiles[i, j].transform.position = new Vector3(m_initialPosition.position.x + j, m_initialPosition.position.y - i, m_initialPosition.position.z);
                m_tiles[i, j].transform.parent = this.transform;
                m_tiles[i, j].name = "Celda_" + i + "-" + j;

                m_tiles[i, j].transform.parent = null;

                if (!m_hasBlackBeenPainted)
                {
                    //negro
                    m_tiles[i, j].GetComponent<SpriteRenderer>().color = new Vector4(0.5f, 0.5f, 0.5f, 1f);
                }
                m_hasBlackBeenPainted = !m_hasBlackBeenPainted;

            }
            m_hasBlackBeenPainted = !m_hasBlackBeenPainted;
        }
    }

    static public Board Instance
    {
        get { return m_instance; }
        private set { }
    }

    public bool AssignPawnToTile(GameObject p_pawn, Vector2 p_tilePosition)
    {
        Tile currentTile = m_tiles[(int)p_tilePosition.y, (int)p_tilePosition.x].GetComponent<Tile>();

        //Debug.Log(p_tilePosition.x + " | " + p_tilePosition.y);

        return currentTile.AddPawn(p_pawn);
        
    }

    public void TakePawnFromTile(Vector2 p_tilePosition)
    {
        Tile currentTile = m_tiles[(int)p_tilePosition.y, (int)p_tilePosition.x].GetComponent<Tile>();

        currentTile.TakePawn();

    }

    public Vector2 ScreenToTilePosition(Vector3 p_mousePosition)
    {
        float tileX;
        float tileY;

        m_tileSize = (m_tiles[0, 0].transform.position - m_tiles[0, 1].transform.position).magnitude;

        Vector2 leftTopBoardPosition = new Vector2(m_initialPosition.position.x - m_tileSize/2, m_initialPosition.position.y + m_tileSize / 2);

        p_mousePosition = Camera.main.ScreenToWorldPoint(p_mousePosition);

        Vector2 rightBottomBoardPosition = leftTopBoardPosition + new Vector2(MAX_COLUMNAS * m_tileSize, -MAX_FILAS * m_tileSize);

        if ( p_mousePosition.x < leftTopBoardPosition.x || p_mousePosition.x > rightBottomBoardPosition.x || p_mousePosition.y > leftTopBoardPosition.y || p_mousePosition.y < rightBottomBoardPosition.y) { 
            return Vector2.positiveInfinity; 
        }
        else
        {
            tileX = (int)((p_mousePosition.x - leftTopBoardPosition.x) / m_tileSize);
            tileY = -(int)((p_mousePosition.y -  leftTopBoardPosition.y)/ m_tileSize);

            return new Vector2(tileX, tileY);
        }
    }

    public bool IsTileEmpty(Vector2 p_tilePosition)
    {
        if(p_tilePosition.x >= 0 && p_tilePosition.x < MAX_COLUMNAS && p_tilePosition.y >= 0 && p_tilePosition.y < MAX_FILAS)
        {
            return m_tiles[(int)p_tilePosition.y, (int)p_tilePosition.x].GetComponent<Tile>().IsEmpty;
        }
        return false;
    }

}