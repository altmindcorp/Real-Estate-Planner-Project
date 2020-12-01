using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Grid : MonoBehaviour
{

    public Camera mainCamera;
    public GridCell gridCell;
    private int xSize = 50;
    private int ySize = 50;
    private List<GridCell> gridCells = new List<GridCell>();
    private List<Vector3> gridPositions = new List<Vector3>();
    private void Start()
    {
        CameraController.onDistanceChanged += ChangeGridShape;
        //CameraDistanceMode.cellSize = 1;
        for (int i = 0; i < xSize; i++)//xLine
        {
            for (int j = 0; j < ySize; j++)//yLine
            {
                gridCells.Add(Instantiate(gridCell, new Vector3(i * GridScaler.scaleValue, j * GridScaler.scaleValue, 0), Quaternion.identity, this.transform));
            }
        }
        transform.Translate(new Vector3(StaticClass.planeScale - xSize * GridScaler.scaleValue / 2, StaticClass.planeScale - ySize * GridScaler.scaleValue / 2, 0));
    }

    private void Update()
    {

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
        for (int i = 0; i < xSize; i++)
        {
            for (int j = 0; j < ySize; j++)
            {
                gridPositions.Add(new Vector3(i * GridScaler.scaleValue, j * GridScaler.scaleValue, 0));
            }
        }
        foreach (GridCell gridCell in gridCells)
        {
            gridCell.ScaleCell(multiplier);
            gridCell.transform.localPosition = gridPositions[k];
            //Debug.Log(gridCell.transform.position);
            k++;
        }
        transform.position = new Vector3(GetRound(mainCamera.transform.position.x) - xSize * GridScaler.scaleValue / 2, GetRound(mainCamera.transform.position.y) - ySize * GridScaler.scaleValue / 2, 0);
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
