using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FloorObjectData : PlanObjectData
{
    public Vector3 initialScale;
    public Vector3[] anchorVertices;
    public FloorObjectData(Mesh mesh, Vector3 position, Vector3[] anchorVertices, Vector3 initialScale, int id)
    {
        this.anchorVertices = anchorVertices;
        this.triangles = mesh.triangles;
        this.uvs = mesh.uv;
        this.vertices = mesh.vertices;
        this.position = position;
        this.initialScale = initialScale;
        this.id = id;
    }
}
