using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    public Material gridLineMat;
    // Start is called before the first frame update
    void Start()
    {
        Grid grid = new Grid(10, 10, 1);
        LineRenderer gridRenderer = GetComponent<LineRenderer>();
        //gridRenderer.SetWidth(0.05f, 0.05f);
        //gridRenderer.startWidth = 0.1f;
        //gridRenderer.endWidth = 0.1f;
        //gridRenderer.loop = false;
        //gridRenderer.material = gridLineMat;
        grid.DrawGrid(gridRenderer);
        //Debug.DrawLine(new Vector3(-10, 0), new Vector3(10, 0), Color.white, 100);
    }

}
