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
                    var floorObject = Instantiate(prefabs[0], transform, true).GetComponent<Floor>();
                    floorObject.transform.position = UIController.GetScaledObjectPosition(-0.0001f);
                    floorObject.CreatePlanObject(new Vector3(1, 1, 0));
                    
                }
            }

            else if (UIController.objectTypeMode == 1)//wall & anchor
            {
                if (prefabs[1] != null)
                {
                    var anchorObject = Instantiate(prefabs[1], transform, true).GetComponent<PlanObjectAnchor>();
                    anchorObject.transform.position = UIController.GetScaledObjectPosition(-0.0003f);
                    anchorObject.CreatePlanObject(ObjectsParams.scale);
                    
                }
            }

            else if (UIController.objectTypeMode == 4)//spawn point
            {
                if (prefabs[5] != null && !ObjectsDataRepository.currentSaveFile.spawnPositionIsSet)
                {
                    GameObject spawnPosition = Instantiate(prefabs[5], transform, true);
                    spawnPosition.transform.position = UIController.GetScaledObjectPosition(-0.0002f) + new Vector3(0.5f, 0.5f, 0);
                    //ObjectsDataRepository.currentSaveFile.spawnPosition = spawnPosition.transform.position;                   
                    ObjectsDataRepository.currentSaveFile.spawnPositionIsSet = true;
                    ObjectsDataRepository.currentSaveFile.spawnPosition = spawnPosition.transform.position;
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

    public void ResetCurrentPlan()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void RecreateObjects()
    {
        foreach (PlanObjectData planObjData in ObjectsDataRepository.currentSaveFile.planObjectsDataList)
        {
            if (planObjData is AnchorObjectData)
            {
                var anchorObject = Instantiate(prefabs[1], this.transform, true).GetComponent<PlanObjectAnchor>();
                anchorObject.transform.Translate(planObjData.position);
                anchorObject.RecreatePlanObject(planObjData);
            }

            if (planObjData is WallObjectData)
            {
                var wallObjectData = planObjData as WallObjectData;
                var wallObject = Instantiate(prefabs[2], this.transform, true).GetComponent<PlanObjectSimpWall>();
                wallObject.transform.Translate(planObjData.position);
                wallObject.RecreatePlanObject(planObjData);
                if (wallObjectData.wallChildsIdList.Count != 0)
                {
                    foreach (int id in wallObjectData.wallChildsIdList)
                    {
                        var objData = ObjectsDataRepository.currentSaveFile.planObjectsDataList.Find(x => x.id == id);
                        if (objData is WindowObjectData)
                        {
                            var windowObject = Instantiate(prefabs[3], objData.position, Quaternion.identity, wallObject.transform).GetComponent<PlanObjectWindow>();
                            windowObject.RecreatePlanObject(objData);
                        }

                        if (objData is DoorObjectData)
                        {
                            var doorObject = Instantiate(prefabs[4], objData.position, Quaternion.identity, wallObject.transform).GetComponent<PlanObjectDoor>();
                            doorObject.RecreatePlanObject(objData);
                        }
                    }
                    
                }
            }

            if (planObjData is FloorObjectData)
            {
                var floorObject = Instantiate(prefabs[0], this.transform, true).GetComponent<Floor>();
                floorObject.transform.Translate(planObjData.position);
                floorObject.RecreatePlanObject(planObjData);
            }
        }
        if (ObjectsDataRepository.currentSaveFile.spawnPositionIsSet)
        {
            Instantiate(prefabs[5], transform, true).transform.position = ObjectsDataRepository.currentSaveFile.spawnPosition;
        }
    }
}
