using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    public bool draggable;
    public bool isDragged;
    private Vector3 mouseDragStartPos;
    private Vector3 objDragStartPos;

    private void OnMouseDown()
    {
        if (draggable) { 
            isDragged = true;
            mouseDragStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            objDragStartPos = this.transform.position;
        }
    }

    private void OnMouseDrag()
    {
        if (isDragged) {
            this.transform.position = objDragStartPos + (Camera.main.ScreenToWorldPoint(Input.mousePosition) - mouseDragStartPos);
        }
    }

    private void OnMouseUp()
    {
        isDragged = false;
    }


}
