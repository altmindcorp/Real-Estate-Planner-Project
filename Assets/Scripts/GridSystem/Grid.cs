using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Grid : MonoBehaviour
{
    private MeshFilter meshFilter;
    public Camera mainCamera;
    //public GridCell gridCell;
    private int xSize = 50;
    private int ySize = 50;
    //private List<GridCell> gridCells = new List<GridCell>();
    private List<Vector3> gridPositions = new List<Vector3>();
    private void Start()
    {
        Mesh combinedMesh = new Mesh();
        Mesh meshToCombine = MeshCreator.GetGridMesh(0.08f);
        List<CombineInstance> combinedInstances = new List<CombineInstance>();
        meshFilter = GetComponent<MeshFilter>();
        CameraController.onDistanceChanged += ChangeGridShape;
        //CameraDistanceMode.cellSize = 1;
        for (int i = 0; i < xSize; i++)//xLine
        {
            for (int j = 0; j < ySize; j++)//yLine
            {
                CombineInstance combine = new CombineInstance();
                meshFilter.mesh = new Mesh();
                meshFilter.transform.position = new Vector3(i, j, 0);
                combine.mesh = meshToCombine;
                combine.transform = meshFilter.transform.localToWorldMatrix;
                combinedInstances.Add(combine);
            }
            combinedMesh.CombineMeshes(combinedInstances.ToArray());
            //meshFilter.mesh = combinedMesh;
        }
        combinedMesh.CombineMeshes(combinedInstances.ToArray());
        meshFilter.mesh = new Mesh();
        meshFilter.mesh = combinedMesh;

        transform.localScale *= 0.01f;
        transform.position = new Vector3(500 - GridScaler.scaleValue * xSize / 2, 500 - GridScaler.scaleValue * ySize / 2, 0);
    }

    private void ChangeGridShape(int change)
    {
        int k = 0;
        float multiplier = 0;

        gridPositions.Clear();
        if (change > 0)
        {
            multiplier = 10;
        }
        else if (change < 0)
        {
            multiplier = 0.1f;
        }

        GridScaler.scaleValue *= multiplier;
        transform.localScale *= multiplier;
        transform.position = new Vector3(500 - GridScaler.scaleValue * xSize / 2, 500 - GridScaler.scaleValue * ySize / 2, 0);
        /*foreach (GridCell gridCell in gridCells)
        {
            gridCell.ScaleCell(multiplier);
            gridCell.transform.localPosition = gridPositions[k];
            //Debug.Log(gridCell.transform.position);
            k++;
        }*/
        //transform.position = new Vector3(GetRound(mainCamera.transform.position.x) - xSize * GridScaler.scaleValue / 2, GetRound(mainCamera.transform.position.y) - ySize * GridScaler.scaleValue / 2, 0);
    }

    private float GetRound(float value)
    {
        if (GridScaler.scaleValue > 1)
        {
            return (int)(value * GridScaler.scaleValue) / GridScaler.scaleValue;

        }
        else if (GridScaler.scaleValue < 1)
        {
            return (int)(value / GridScaler.scaleValue) * GridScaler.scaleValue;
        }
        else if (GridScaler.scaleValue == 1)
        {
            return (int)value;
        }
        else return 0;
    }
}
