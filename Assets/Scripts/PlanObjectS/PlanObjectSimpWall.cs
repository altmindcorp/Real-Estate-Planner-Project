using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlanObjectSimpWall : PlanObject
{
    public float height;
    public Vector3 direction;
    private Vector3 startPoint = Vector3.zero;

    public List<PlanObjectObject> planObjectObjectsList = new List<PlanObjectObject>();

    public void SetStartPoint()
    {
        
        if (direction == Vector3.right && vertices[2].x >= vertices[1].x + scale.x * 2 * 0.01f)
        {
            startPoint.x = vertices[3].x - scale.x * 0.01f; ;
            startPoint.y = vertices[3].y;
            startPoint.z = vertices[3].z - 0.001f;
            //Debug.Log("StartPoint: " + startPoint + ", vertices[1]" + vertices[1] + ", scale: " + scale + ", direction: " + direction);
        }

        else if (direction == Vector3.left && vertices[1].x <= vertices[2].x - scale.x * 2 * 0.01f)
        {
            startPoint.x = vertices[1].x;
            startPoint.y = vertices[3].y;
            startPoint.z = vertices[3].z - 0.001f;
        }

        else if (direction == Vector3.up && vertices[1].y >= vertices[0].y + scale.y * 2 * 0.01f)
        {
            startPoint.x = vertices[1].x;
            startPoint.y = vertices[1].y - scale.y * 0.01f;
            startPoint.z = vertices[3].z - 0.001f;
        }
        else if (direction == Vector3.down && vertices[0].y <= vertices[1].y - scale.y * 2 * 0.01f)
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
