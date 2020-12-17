using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanObjectObjectComparer
{
    public static int SortByX(WallChildObjectData p1, WallChildObjectData p2)
    {
        return p1.mesh.vertices[0].x.CompareTo(p2.mesh.vertices[0].x);
        
    }

    public static int SortByY(WallChildObjectData p1, WallChildObjectData p2)
    {
        return p1.mesh.vertices[0].y.CompareTo(p2.mesh.vertices[0].y);
    }
}
