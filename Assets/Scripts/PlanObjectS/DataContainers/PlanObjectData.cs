using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlanObjectData : IComparer
{
    protected int[] triangles;
    protected Vector3[] vertices;
    protected Vector2[] uvs;
    public Vector3 position;
    public int id;

    public int Compare(object x, object y)
    {
        throw new System.NotImplementedException();
    }

    public Vector3[] GetVertices()
    {
        return vertices;
    }

    public void ChangeMeshProperties(Mesh mesh)
    {
        this.triangles = mesh.triangles;
        this.uvs = mesh.uv;
        this.vertices = mesh.vertices;
    }

    public Mesh GetMesh()
    {
        
        Vector3[] newVertices = new Vector3[4];
        newVertices[0] = new Vector3(this.vertices[0].x, this.vertices[0].y, this.vertices[0].z);
        newVertices[1] = new Vector3(this.vertices[1].x, this.vertices[1].y, this.vertices[1].z);
        newVertices[2] = new Vector3(this.vertices[2].x, this.vertices[2].y, this.vertices[2].z);
        newVertices[3] = new Vector3(this.vertices[3].x, this.vertices[3].y, this.vertices[3].z);

        int[] newTriangles = new int[] { 0, 1, 2, 0, 2, 3 };
        Vector2[] newUV = new Vector2[4];
        newUV[0] = new Vector2(uvs[0].x, uvs[0].y);
        newUV[1] = new Vector2(uvs[1].x, uvs[1].y);
        newUV[2] = new Vector2(uvs[2].x, uvs[2].y);
        newUV[3] = new Vector2(uvs[3].x, uvs[3].y);

        Mesh newMesh = new Mesh();

        newMesh.vertices = newVertices;
        newMesh.triangles = newTriangles;
        newMesh.uv = newUV;       
        return newMesh;
    }

    /*Vector3[] vertices = new Vector3[4];
    vertices[0] = new Vector3(0, 0, zDepth);
    vertices[1] = new Vector3(0, scale.y, zDepth);
    vertices[2] = new Vector3(scale.x, scale.y , zDepth);
    vertices[3] = new Vector3(scale.x, 0, zDepth);

    int[] triangles = new int[] { 0, 1, 2, 0, 2, 3 };

    Vector2[] newUV = new Vector2[4];
    newUV[0] = new Vector2(0, 0);
    newUV[1] = new Vector2(0, 1);
    newUV[2] = new Vector2(1, 1);
    newUV[3] = new Vector2(1, 0);

    Mesh newMesh = new Mesh();
    newMesh.vertices = vertices;
        newMesh.triangles = triangles;
        newMesh.uv = newUV;

        return newMesh;*/
}
