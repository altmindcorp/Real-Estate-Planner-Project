using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanObjectSimpWall : PlanObject
{
    private float height;

    public PlanObjectSimpWall(Vector2[] coords, int id, float height)
    {
        this.coords = coords;
        this.id = id;
        this.height = height;
    }

    public float GetHeight()
    {
        return height;
    }
}
