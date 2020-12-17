using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlanObjectData : IComparer
{
    public Mesh mesh;
    public Material material;
    public Vector3 position;
    public int id;

    public int Compare(object x, object y)
    {
        throw new System.NotImplementedException();
    }

    public PlanObjectData GetData()
    {
        return this;
    }
}
