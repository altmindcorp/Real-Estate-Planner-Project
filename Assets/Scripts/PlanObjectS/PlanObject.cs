using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlanObject : MonoBehaviour
{

    protected Vector3[] coords = new Vector3[4];
    private int id;
    protected Vector3 scale;

    public Mesh GetMesh()
    {
        return GetComponent<MeshFilter>().mesh;
    }

    public int GetID()
    {
        return id;
    }

    public void SetID(int id)
    {
        this.id = id;
    }

    public void SetTag(string tag)
    {
        gameObject.tag = tag;
    }

    public Vector3 GetScale()
    {
        return scale;
    }

    public void SetScale(Vector3 scale)
    {
        this.scale = scale;
    }

    public Vector3[] GetVertices()
    {
        return GetComponent<MeshFilter>().mesh.vertices;
    }
}
