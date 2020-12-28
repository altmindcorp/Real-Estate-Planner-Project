using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GridCell : MonoBehaviour
{
    private float borderWidth;
    private float zDepth;
    private Vector3[] vertices = new Vector3[8];
    private Vector2[] uvs = new Vector2[8];
    private int[] triangles = new int[24];

    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;

    private void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        borderWidth = 0.1f;
        meshFilter.mesh = gameObject.GetComponent<MeshFilter>().mesh;
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        SetupCell();
        ScaleCell(GridScaler.scaleValue);
    }

    //Create Cell 
    //innerVertices[0]
    private void SetupCell()
    {

        //outer
        vertices[0] = new Vector3(0, 0, zDepth);
        vertices[1] = new Vector3(0, 1, zDepth);
        vertices[2] = new Vector3(1, 1, zDepth);
        vertices[3] = new Vector3(1, 0, zDepth);

        //inner
        vertices[4] = new Vector3(borderWidth / 2, borderWidth / 2, zDepth);
        vertices[5] = new Vector3(borderWidth / 2, 1 - borderWidth / 2, zDepth);
        vertices[6] = new Vector3(1 - borderWidth / 2, 1 - borderWidth / 2, zDepth);
        vertices[7] = new Vector3(1 - borderWidth / 2, borderWidth / 2, zDepth);

        for (int i = 0; i < 3; i++)
        {
            triangles[0 + i * 6] = 0 + i;
            triangles[1 + i * 6] = 1 + i;
            triangles[2 + i * 6] = 4 + i;

            triangles[3 + i * 6] = 1 + i;
            triangles[4 + i * 6] = 5 + i;
            triangles[5 + i * 6] = 4 + i;
        }

        triangles[18] = 3;
        triangles[19] = 0;
        triangles[20] = 7;

        triangles[21] = 0;
        triangles[22] = 4;
        triangles[23] = 7;

        meshFilter.mesh.vertices = vertices;
        meshFilter.mesh.uv = uvs;
        meshFilter.mesh.triangles = triangles;
    }

    public Mesh GetCellMesh()
    {
        return meshFilter.mesh;
    }

    public void ScaleCell(float scale)
    {
        for (int i = 0; i < 8; i++)
        {
            vertices[i] *= scale;
        }
        meshFilter.mesh.vertices = vertices;
    }
    //Position Cell (float positionX, PositionY)
    //this position = position

    //Resize Cell (float scale)
}
