using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Plane : MonoBehaviour, ISpawner
{
    private bool spawnSet = false;
    public GameObject[] prefabs;
    public static List<PlanObject> PlanObjectsList = new List<PlanObject>();

    public void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (UIController.objectTypeMode == 0)//floor
            {
                if (prefabs[0] != null)
                {
                    var floorObject = Instantiate(prefabs[0], UIController.GetScaledObjectPosition(-0.0001f), Quaternion.identity).GetComponent<Floor>();
                    floorObject.CreatePlanObject(new Vector3(1, 1, 0));
                    
                }
            }

            else if (UIController.objectTypeMode == 1)//wall & anchor
            {
                if (prefabs[1] != null)
                {
                    var anchorObject = Instantiate(prefabs[1], UIController.GetScaledObjectPosition(-0.0003f), Quaternion.identity).GetComponent<PlanObjectAnchor>();
                    anchorObject.CreatePlanObject(ObjectsParams.scale);
                    
                }
            }

            else if (UIController.objectTypeMode == 4)//spawn point
            {
                if (prefabs[5] != null && !spawnSet)
                {
                    ObjectsDataRepository.spawnPointPosition = Instantiate(prefabs[5], UIController.GetScaledObjectPosition(-0.0002f) + new Vector3(0.5f, 0.5f, 0), Quaternion.Euler(Vector3.left * 90)).transform.position;
                    ObjectsParams.spawnPointIsSet = true;
                }
            }
        }       
    }

    public GameObject GetPrefabByIndex(int id)
    {
        return prefabs[id];
    }

    public void OnMouseDrag()
    {
        //throw new System.NotImplementedException();
    }
}
