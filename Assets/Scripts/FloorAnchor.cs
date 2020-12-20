using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorAnchor : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 startMousePosition;
    public Floor floor;
    public int verticeNumber;


    //Color bright
    private void OnMouseOver()
    {
        
    }

    //Move
    private void OnMouseDrag()
    {

        if (startMousePosition != UIController.GetUnscaledObjectPosition(-0.0002f))
        {
            var changePosition = UIController.GetUnscaledObjectPosition(-0.0002f) - startMousePosition;
            floor.ChangeVerticePosition(verticeNumber, changePosition);
            this.transform.Translate(changePosition, Space.World);
            startMousePosition = UIController.GetUnscaledObjectPosition(-0.0002f);
        }
    }

    //Start move
    private void OnMouseDown()
    {
        startPosition = this.transform.position;
        startMousePosition = UIController.GetUnscaledObjectPosition(-0.0002f);
    }

    //End Move
    private void OnMouseUp()
    {
        
    }
}
