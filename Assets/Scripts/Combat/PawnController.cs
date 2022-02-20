using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PAWN_STATUS { IDLE, SEARCH, ATTACK }


public class PawnController : MonoBehaviour
{
    public bool draggable;
    bool isDragged;
    private Vector3 mouseDragStartPos;
    private Vector3 objDragStartPos;

    GridManager m_board;
    Vector3 m_position;
    Vector2 m_tilePosition;
    Vector3 m_previousPosition;

    [SerializeField] Transform m_positionToGo;
    [SerializeField] bool m_isMyTurn;

    PAWN_STATE m_state;
    Vector3[] m_directions = { Vector3.right, Vector3.down, Vector3.left, Vector3.up };



    private void Start()
    {
        m_position = transform.position;
        m_previousPosition = m_position;
    }
    private void Update()
    {

        switch (m_state)
        {
            default:
                break;
            case PAWN_STATE.SEARCH:
                 Search();
                break;
            case PAWN_STATE.IDLE:
                if (Input.GetKeyDown(KeyCode.Space) && m_isMyTurn)
                {
                    m_state = PAWN_STATE.SEARCH;
                }
                break;
        }
    }
    private void OnMouseDown()
    {
        if (draggable)
        {
            isDragged = true;
            objDragStartPos = this.transform.position;
        }
    }

    private void OnMouseDrag()
    {
        if (isDragged)
        {
            Vector3 screenCoordinate = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));

            transform.position = new Vector3(screenCoordinate.x, screenCoordinate.y, transform.position.z);
        }
    }

    private void OnMouseUp()
    {
        if (isDragged)
        {

            isDragged = false;
            Vector2 tilePosition = GridManager.Instance.ScreenToTilePosition(Input.mousePosition);

            if (tilePosition.x == Vector2.positiveInfinity.x)
            {
                transform.position = m_position;
                return;
            }

            if (GridManager.Instance.IsTileEmpty(tilePosition))
            {
                GridManager.Instance.TakePawnFromTile(m_tilePosition);
                m_tilePosition = tilePosition;
                GridManager.Instance.AssignPawnToTile(this.gameObject, tilePosition);
            }
            else
            {
                transform.position = m_position;
            }
        }
    }

    void Search()
    {
        if ((transform.position - m_positionToGo.transform.position).magnitude == 1)
        {
            m_state = PAWN_STATE.IDLE;
            return;
        }

        Vector3 closestDirection = Vector2.zero;
        float closestDistance = 100000;

        // check all 4 tiles and pick the closes one to the objective
        for (int i = 0; i < m_directions.Length; i++)
        {
            Vector2 positionToCheck = GridManager.Instance.ScreenToTilePosition(Camera.main.WorldToScreenPoint(transform.position + m_directions[i]));

            if (GridManager.Instance.IsTileEmpty(positionToCheck))
            {
                float distance = (transform.position + m_directions[i] - m_positionToGo.position).magnitude;
                if (distance < closestDistance && (transform.position + m_directions[i]) != m_previousPosition)
                {
                    closestDistance = distance;
                    closestDirection = m_directions[i];
                }
            }
        }

        Vector2 positionToMove = transform.position + closestDirection;
        Vector2 tileToMove = GridManager.Instance.ScreenToTilePosition(Camera.main.WorldToScreenPoint(positionToMove));

        // move into the designated tile
        m_previousPosition = transform.position;

        GridManager.Instance.TakePawnFromTile(m_tilePosition);
        m_tilePosition = tileToMove;
        GridManager.Instance.AssignPawnToTile(this.gameObject, tileToMove);

    }

    IEnumerator MovementInterval() {
        yield return new WaitForSeconds(1f);
    }

    public void SetPosition(Vector3 p_position)
    {
        transform.position = p_position;
        m_position = p_position;
    }



}
