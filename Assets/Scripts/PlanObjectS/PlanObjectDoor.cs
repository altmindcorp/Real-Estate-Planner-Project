using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanObjectDoor : PlanObject
{
    private float topHeight;

    public PlanObjectDoor(Vector2[] coords, int id, float topHeight)
    {
        this.coords = coords;
        this.id = id;
        this.topHeight = topHeight;
    }

    public float GettopHeight()
    {
        return topHeight;
    }
}
