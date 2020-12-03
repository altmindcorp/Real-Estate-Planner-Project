using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanObjectObjectComparer
{
    public static int SortByX(PlanObjectObject p1, PlanObjectObject p2)
    {
        return p1.vertices[0].x.CompareTo(p2.vertices[0].x);
    }

    public static int SortByY(PlanObjectObject p1, PlanObjectObject p2)
    {
        return p1.vertices[0].y.CompareTo(p2.vertices[0].y);
    }
}
