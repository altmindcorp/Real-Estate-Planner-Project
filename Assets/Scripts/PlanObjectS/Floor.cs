﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public List<Vector3> vertices = new List<Vector3>();
    

    public Material material;
    public MeshRenderer meshRenderer;
    public MeshFilter meshFilter;
    public MeshCollider meshCollider;
    private Vector3 startPoint;

    public void SetFloor(Vector3 startPoint, Material material)
    {
        if (meshFilter == null)
        {
            Debug.LogError("Mesh is null");
        }
        this.startPoint = startPoint;
        SetStartMeshParameters();
        SetMaterial(material);
        AddToPlanList();
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void UpdateFloor(int mode, Vector3 mousePosition)
    {
        if (mode == 0)
        {
            if (mousePosition.x > meshFilter.mesh.bounds.max.x)
            {
                Vector3 newVertice2 = new Vector3(vertices[2].x + GridScaler.scaleValue,vertices[2].y, vertices[2].z);
                Vector3 newVertice3 = new Vector3(vertices[3].x + GridScaler.scaleValue, vertices[3].y, vertices[3].z);
                vertices[2] = newVertice2;
                vertices[3] = newVertice3;
            }

            else if (mousePosition.y > meshFilter.mesh.bounds.max.y)
            {
                Vector3 newVertice1 = new Vector3(vertices[1].x, vertices[1].y + GridScaler.scaleValue, vertices[1].z);
                Vector3 newVertice2 = new Vector3(vertices[2].x, vertices[2].y + GridScaler.scaleValue, vertices[2].z);
                vertices[1] = newVertice1;
                vertices[2] = newVertice2;
            }

            else if (mousePosition.x < meshFilter.mesh.bounds.min.x - GridScaler.scaleValue)
            {
                Vector3 newVertice1 = new Vector3(vertices[1].x - GridScaler.scaleValue, vertices[1].y, vertices[1].z);
                Vector3 newVertice0 = new Vector3(vertices[0].x - GridScaler.scaleValue, vertices[0].y, vertices[0].z);
                vertices[1] = newVertice1;
                vertices[0] = newVertice0;
            }

            else if (mousePosition.y < meshFilter.mesh.bounds.min.y - GridScaler.scaleValue)
            {
                Vector3 newVertice0 = new Vector3(vertices[0].x, vertices[0].y - GridScaler.scaleValue, vertices[0].z);
                Vector3 newVertice3 = new Vector3(vertices[3].x, vertices[3].y - GridScaler.scaleValue, vertices[3].z);
                vertices[0] = newVertice0;
                vertices[3] = newVertice3;
            }
        }

        ResizeMesh();
    }

    private void ResizeMesh()
    {
        meshFilter.mesh.vertices = vertices.ToArray();
        meshCollider.sharedMesh = meshFilter.mesh;
        meshFilter.mesh.RecalculateBounds();

    }
    private void SetVertices()
    {

    }

    private void AddVertices()
    {

    }

    private void AddToPlanList()
    {
        StaticClass.plan.planGameObjects.Add(this.gameObject);
    }

    private Vector3[] GetStartVertices()
    {

        Vector3 vertice0 = new Vector3((int)(startPoint.x / GridScaler.scaleValue) * GridScaler.scaleValue, (int)(startPoint.y / GridScaler.scaleValue) * GridScaler.scaleValue, -0.02f);
        Vector3 vertice1 = new Vector3((int)(startPoint.x / GridScaler.scaleValue) * GridScaler.scaleValue, (int)(startPoint.y / GridScaler.scaleValue) * GridScaler.scaleValue + GridScaler.scaleValue, -0.02f);
        Vector3 vertice2 = new Vector3((int)(startPoint.x / GridScaler.scaleValue) * GridScaler.scaleValue + GridScaler.scaleValue, (int)(startPoint.y / GridScaler.scaleValue) * GridScaler.scaleValue + GridScaler.scaleValue, -0.02f);
        Vector3 vertice3 = new Vector3((int)(startPoint.x / GridScaler.scaleValue) * GridScaler.scaleValue + GridScaler.scaleValue, (int)(startPoint.y / GridScaler.scaleValue) * GridScaler.scaleValue, -0.02f);

        /*Vector3 vertice0 = new Vector3(0, 0, 0);
        Vector3 vertice1 = new Vector3(0, 1, 0);
        Vector3 vertice2 = new Vector3(1, 1, 0);
        Vector3 vertice3 = new Vector3(1, 0, 0);*/

        vertices.Add(vertice0);
        vertices.Add(vertice1);
        vertices.Add(vertice2);
        vertices.Add(vertice3);
        return vertices.ToArray();
    }

    private Vector2[] SetUVs()
    {
        Vector2[] newUVs = new Vector2[4];
        newUVs[0] = new Vector2(0, 0);
        newUVs[1] = new Vector2(0, 1);
        newUVs[2] = new Vector2(1, 1);
        newUVs[3] = new Vector2(1, 0);
        return newUVs;
    }

    private int[] SetTriangles()
    {
        int[] newTriangles = new int[] { 0, 1, 2, 0, 2, 3 };
        return newTriangles;
    }

    private void SetMaterial(Material material)
    {
        meshRenderer.material = material;
    }

    private void SetStartMeshParameters()
    {
        Mesh newMesh = new Mesh();
        newMesh.vertices = GetStartVertices();
        newMesh.uv = SetUVs();
        newMesh.triangles = SetTriangles();
        meshFilter.mesh = newMesh;
        meshCollider.sharedMesh = meshFilter.mesh;
        Debug.Log(meshCollider.bounds);
        meshFilter.mesh.RecalculateBounds();
    }
}