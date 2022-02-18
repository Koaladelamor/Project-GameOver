using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    bool m_isBeingDragged;
    Board m_board;
    Vector3 m_position;
    Vector2 m_tilePosition;

    private void Start()
    {
        m_position = transform.position;
    }

    private void OnMouseDrag()
    {
        Vector3 screenCoordinate = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));

        transform.position = new Vector3(screenCoordinate.x, screenCoordinate.y, transform.position.z);
    }

    private void OnMouseDown()
    {
        m_isBeingDragged = true;
    }

    private void OnMouseUp()
    {
        if (m_isBeingDragged) {

            m_isBeingDragged = false;
            Vector2 tilePosition = Board.Instance.ScreenToTilePosition(Input.mousePosition);
            if (tilePosition.x == Vector2.positiveInfinity.x) {
                transform.position = m_position;
                return;
            }
            m_tilePosition = tilePosition;
            Board.Instance.TakePawnFromTile(tilePosition);
            Board.Instance.AssignPawnToTile(this.gameObject, tilePosition);
        }
    }

    public void SetPosition(Vector3 p_position)
    {
        transform.position = p_position;
        m_position = p_position;
    }
}
