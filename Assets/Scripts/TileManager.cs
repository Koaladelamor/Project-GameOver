using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{

    //Colliders no funciona en ppio porque la información del Drag sobreescribe el onTrigger enter
    //Raycast para detectar casilla?

    private bool isOccupied;


    public Vector3 getTransform() {
        Vector3 position = this.transform.position;
        return position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isOccupied = true;

        /*if (collision.gameObject.CompareTag("Player")) {
            if (collision.gameObject.GetComponent<Draggable>().isDragged == false) { 
                collision.gameObject.transform.position = this.transform.position;
            }
        }*/
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isOccupied = false;
    }


}
