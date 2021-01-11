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

    public void RedoObject(PlanObjectData planObjData)
    {
        if (planObjData is AnchorObjectData)
        {
            var anchorObject = Instantiate(prefabs[1], this.transform, true).GetComponent<PlanObjectAnchor>();
            anchorObject.transform.Translate(planObjData.position);
            anchorObject.RecreatePlanObject(planObjData);
        }

        else if(planObjData is FloorObjectData)
        {
            var floorObject = Instantiate(prefabs[0], this.transform, true).GetComponent<Floor>();
            floorObject.transform.Translate(planObjData.position);
            floorObject.RecreatePlanObject(planObjData);
        }

        else if (planObjData is WallObjectData)
        {
            var wallObject = Instantiate(prefabs[2], this.transform, true).GetComponent<PlanObjectSimpWall>();
            wallObject.transform.Translate(planObjData.position);
            wallObject.RecreatePlanObject(planObjData);
        }

        else if (planObjData is WindowObjectData)
        {
            var windowObjectData = planObjData as WindowObjectData;
            List<PlanObjectSimpWall> walls = new List<PlanObjectSimpWall>(); 
            this.GetComponentsInChildren(walls);
            //Debug.Log("Walls count: " + walls.Count);

            PlanObjectSimpWall parentWall = walls.Find(x => x.id == windowObjectData.wallID);

            ObjectsDataRepository.currentSaveFile.planObjectsDataList.Add(windowObjectData);
            WallObjectData parentWallData = ObjectsDataRepository.currentSaveFile.planObjectsDataList.Find(x => x.id == windowObjectData.wallID) as WallObjectData;
            parentWallData.wallChildsIdList.Add(windowObjectData.id);
            //walls.Find(x => x.id == windowObjectData.wallID).planObjectWallChildIdList.Add(windowObjectData.id);
            //Debug.Log("Wall Childs Count: " + parentWallData.wallChildsIdList.Count);
            var windowObject = Instantiate(prefabs[3], planObjData.position, Quaternion.identity, parentWall.gameObject.transform).GetComponent<PlanObjectWindow>();
            windowObject.RecreatePlanObject(planObjData);
            //Debug.Log("Window id: " + windowObject.id);
        }

        else if (planObjData is DoorObjectData)
        {
            var doorObjectData = planObjData as DoorObjectData;
            List<PlanObjectSimpWall> doors = new List<PlanObjectSimpWall>();
            this.GetComponentsInChildren(doors);

            doors.Find(x => x.id == doorObjectData.wallID).planObjectWallChildIdList.Add(doorObjectData.id);

            var doorObject = Instantiate(prefabs[4], planObjData.position, Quaternion.identity, doors.Find(x => x.id == doorObjectData.wallID).gameObject.transform).GetComponent<PlanObjectWindow>();
            doorObject.RecreatePlanObject(planObjData);
        }

        ObjectsDataRepository.currentSaveFile.planObjectsDataList.Add(planObjData);
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
