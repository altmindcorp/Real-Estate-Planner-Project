using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    int width;
    int height;
    float cellSize;
    private int[,] gridArray;
    public Grid(int width, int height, float cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        gridArray = new int[width, height];

        for (int x=0; x<gridArray.GetLength(0)/cellSize; x++)
        {
            for (int y=0; y< gridArray.GetLength(1)/cellSize; y++)
            {
                //Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100);
                //Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x+ 1, y), Color.white, 100);
                //Debug.Log(x + " " + y);
            }
        }
    }

    public void DrawGrid(LineRenderer gridLine)
    {
        List<Vector3> verticesList = new List<Vector3>();
        
        for (int i = 0, j = 0; i < height; i+=2)
        {
            verticesList.Add(new Vector3(j, 0));
            verticesList.Add(new Vector3(j, width));
            verticesList.Add(new Vector3(j+1, width));
            verticesList.Add(new Vector3(j+1, 0));
            j+=2;
        }    
        for (int i = 0, j = 0; i<width; i+=2)
        {
            verticesList.Add(new Vector3(height, j, 0));
            verticesList.Add(new Vector3(0, j, 0));
            verticesList.Add(new Vector3(0, j+1, 0));
            verticesList.Add(new Vector3(height, j+1, 0));
            j += 2;
        }
        gridLine.positionCount = verticesList.Count;
        gridLine.SetPositions(verticesList.ToArray());
    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize;
    }
}
