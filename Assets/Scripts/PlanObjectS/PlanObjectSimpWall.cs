using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanObjectSimpWall : PlanObject
{
    private float height;
    private Vector3 direction;
    private Vector3 startPoint = Vector3.zero;

    public float GetHeight()
    {
        return height;
    }

    public void SetDirection(Vector3 direction)
    {
        
        this.direction = direction;
    }

    public Vector3 GetDirection()
    {
        return direction;
    }

    public void SetStartPoint()
    {
        Vector3[] vertices = GetVertices();
        if (direction == Vector3.right && vertices[2].x >= vertices[1].x + scale.x * 2 * GridScaler.scaleValue)
        {
            startPoint.x = vertices[3].x - scale.x * GridScaler.scaleValue;
            startPoint.y = vertices[3].y;
            startPoint.z = vertices[3].z - 0.001f;
            //Debug.Log("StartPoint: " + startPoint + ", vertices[1]" + vertices[1] + ", scale: " + scale + ", direction: " + direction);
        }

        else if (direction == Vector3.left && vertices[1].x <= vertices[2].x - scale.x * 2 * GridScaler.scaleValue)
        {
            startPoint.x = vertices[1].x;
            startPoint.y = vertices[3].y;
            startPoint.z = vertices[3].z - 0.001f;
        }

        else if (direction == Vector3.up && vertices[1].y >= vertices[0].y + scale.y * 2 * GridScaler.scaleValue)
        {
            startPoint.x = vertices[1].x;
            startPoint.y = vertices[1].y - scale.y * GridScaler.scaleValue;
            startPoint.z = vertices[3].z - 0.001f;
        }
        else if (direction == Vector3.down && vertices[0].y <= vertices[1].y - scale.y * 2 * GridScaler.scaleValue)
        {
            startPoint.x = vertices[0].x;
            startPoint.y = vertices[0].y;
            startPoint.z = vertices[3].z - 0.001f;
        }
        else
        {
            Debug.Log("Start Point is zero");
            startPoint = Vector3.zero;
        }

    }

    public Vector3 GetStartPoint()
    {
        SetStartPoint();
        return startPoint;
    }
}
