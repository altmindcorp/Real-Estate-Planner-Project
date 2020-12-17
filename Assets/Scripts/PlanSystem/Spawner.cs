using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Floor floorPrefab;
    private string floorPrefabName = "Floor";
    //in UI static class:
    //int mode = 0;

    /*private void OnMouseDown()
    {
        Vector3 pointPosition = Input.mousePosition; 
        pointPosition.z = - Camera.main.transform.position.z;
        SpawnObject(Camera.main.ScreenToWorldPoint(pointPosition));
    }*/

    public void SpawnObject(Vector3 point)
    {
        if (UIController.objectTypeMode == 0)
        {
            SpawnFloor(point);

        }
    }

    private void SpawnAnchor()
    {

    }

    private void SpawnFloor(Vector3 point)
    {
        Debug.Log("Spawn Floor ");
        //Mesh newMesh = MeshCreator.Create2DMesh(-0.001f);
        var floor = Instantiate(floorPrefab, MeshCreator.GetScaledStartPoint(point), Quaternion.identity).GetComponent<Floor>();
        floor.CreatePlanObject();
    }
}
