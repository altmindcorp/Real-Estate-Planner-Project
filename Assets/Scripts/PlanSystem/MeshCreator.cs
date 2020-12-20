using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshCreator
{

    //public static Vector3 scale = new Vector3(3, 3, 0);


    public static Mesh Create2DMesh(Vector3 scale, float zDepth)
    {
        Vector3[] vertices = new Vector3[4];
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

        return newMesh;
    }

    //verticesChange - changing on axis, changeMode - wall +/-
    public static Mesh UpdateWall2DMesh(Mesh updatingMesh, Vector2 verticesChange, int changeMode)
    {
        Vector3[] vertices = updatingMesh.vertices;
        Vector2[] newUV = updatingMesh.uv;
        if (verticesChange.x > 0)
        {
            if (changeMode > 0)
            {
                newUV[2].x += verticesChange.x / (vertices[2].y - vertices[3].y);
                newUV[3].x += verticesChange.x / (vertices[2].y - vertices[3].y);
                vertices[2].x += verticesChange.x;
                vertices[3].x += verticesChange.x;
            }

            else if (changeMode < 0)
            {
                newUV[1].x += verticesChange.x / (vertices[1].y - vertices[0].y);
                newUV[0].x += verticesChange.x / (vertices[1].y - vertices[0].y);
                vertices[1].x += verticesChange.x;
                vertices[0].x += verticesChange.x;
            }
        }

        else if (verticesChange.x < 0)
        {
            if (changeMode > 0)
            {
                newUV[0].x += verticesChange.x / (vertices[1].y - vertices[0].y);
                newUV[1].x += verticesChange.x / (vertices[1].y - vertices[0].y);
                vertices[0].x += verticesChange.x;
                vertices[1].x += verticesChange.x;
            }

            else if (changeMode < 0)
            {
                newUV[2].x += verticesChange.x / (vertices[2].y - vertices[3].y);
                newUV[3].x += verticesChange.x / (vertices[2].y - vertices[3].y);
                vertices[2].x += verticesChange.x;
                vertices[3].x += verticesChange.x;
            }
        }

        else if (verticesChange.y > 0)
        {
            if (changeMode > 0)
            {
                newUV[1].y += verticesChange.y / (vertices[2].x - vertices[1].x);
                newUV[2].y += verticesChange.y / (vertices[2].x - vertices[1].x);
                vertices[1].y += verticesChange.y;
                vertices[2].y += verticesChange.y;
            }

            else if (changeMode < 0)
            {
                newUV[0].y += verticesChange.y / (vertices[3].x - vertices[0].x);
                newUV[3].y += verticesChange.y / (vertices[3].x - vertices[0].x);
                vertices[0].y += verticesChange.y;
                vertices[3].y += verticesChange.y;
            }
            
        }

        else if (verticesChange.y < 0)
        {
            if (changeMode > 0)
            {
                newUV[0].y += verticesChange.y / (vertices[3].x - vertices[0].x);
                newUV[3].y += verticesChange.y / (vertices[3].x - vertices[0].x);
                vertices[0].y += verticesChange.y;
                vertices[3].y += verticesChange.y;
            }

            else if (changeMode < 0)
            {
                newUV[1].y += verticesChange.y / (vertices[2].x - vertices[1].x);
                newUV[2].y += verticesChange.y / (vertices[2].x - vertices[1].x);
                vertices[1].y += verticesChange.y;
                vertices[2].y += verticesChange.y;
            }
        }

        int[] triangles = new int[] { 0, 1, 2, 0, 2, 3 };

        
        /*newUV[0] += ;
        newUV[1] = new Vector2(0, 1);
        newUV[2] = new Vector2(1, 1);
        newUV[3] = new Vector2(1, 0);*/

        Mesh newMesh = new Mesh();
        newMesh.vertices = vertices;
        newMesh.triangles = triangles;
        newMesh.uv = newUV;

        return newMesh;
        
    }

    public static Mesh ChangeFloorMesh(Mesh mesh, int positionNumber, Vector3 positionChange, Vector3 initialScale)
    {
        Vector3[] vertices = mesh.vertices;
        Vector2[] newUVs = mesh.uv;

        vertices[positionNumber] += positionChange;
        newUVs[positionNumber] += new Vector2(positionChange.x / initialScale.x, positionChange.y / initialScale.y);
        Mesh newMesh = new Mesh();
        newMesh.vertices = vertices;
        newMesh.uv = newUVs;
        newMesh.triangles = mesh.triangles;

        return newMesh;
    }

    public static void Create3DMesh()
    {

    }

    public static Vector3 GetScaledStartPoint(Vector3 point)
    {
        
        return new Vector3(((int)(point.x / GridScaler.scaleValue)) * GridScaler.scaleValue, ((int)(point.y / GridScaler.scaleValue)) * GridScaler.scaleValue, point.z);
    }

    public static Vector3 GetUnscaledStartPoint(Vector3 point)
    {
        return new Vector3(((int)(point.x / 0.01f)) * 0.01f, ((int)(point.y / 0.01f)) * 0.01f, point.z);
    }
}
