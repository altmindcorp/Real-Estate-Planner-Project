using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlanObjectSimpWall : PlanObject
{
    public Vector2 maxSpawnBounds = Vector3.zero;
    public Vector2 minSpawnBounds = Vector2.zero;
    public float height;
    public Vector3 direction;
    public WallObjectData wallObjectData;

    private GameObject GetPrefab(int id)
    {
        Debug.Log("GetPrefab id = " + id);
        return GameObject.Find("Plane").GetComponent<Plane>().GetPrefabByIndex(id);
    }

    //public List<PlanObjectWallChild> planObjectWallChildsList = new List<PlanObjectWallChild>();
    public List<int> planObjectWallChildIdList = new List<int>();

    public void UpdateWall(Vector3 newMousePosition, Vector3 wallDirection)
    {
        int changeValue = 0;
        Vector2 mouseChange = Vector3.zero;
        direction = wallDirection;
        if (direction == Vector3.right)
        {
            if (newMousePosition.x + 0.01f > maxBounds.x)
            {
                
                changeValue = 1;
                mouseChange.x = newMousePosition.x + 0.01f - maxBounds.x;
                maxSpawnBounds.x += mouseChange.x;
            }

            else if (newMousePosition.x + 0.01f < maxBounds.x)
            {
                changeValue = -1;
                mouseChange.x = newMousePosition.x + 0.01f - maxBounds.x;
                maxSpawnBounds.x += mouseChange.x;
            }
        }
        else if (direction == Vector3.left)
        {
            if (newMousePosition.x < minBounds.x)
            {
                changeValue = 1;
                mouseChange.x = newMousePosition.x - minBounds.x;
                minSpawnBounds.x += mouseChange.x;
            }    

            else if (newMousePosition.x > minBounds.x + 0.01f)
            {
                changeValue = -1;
                mouseChange.x = newMousePosition.x - minBounds.x;
                minSpawnBounds.x += mouseChange.x;
            }   
        }

        else if (direction == Vector3.up)
        {
            if (newMousePosition.y + 0.01f > maxBounds.y)
            {
                changeValue = 1;
                mouseChange.y = newMousePosition.y + 0.01f - maxBounds.y;
                maxSpawnBounds.y += mouseChange.y;
            }

            else if (newMousePosition.y + 0.01f < maxBounds.y)
            {
                changeValue = -1;
                mouseChange.y = newMousePosition.y + 0.01f - maxBounds.y;
                maxSpawnBounds.y += mouseChange.y;
            }
        }

        else if (direction == Vector3.down)
        {
            if (newMousePosition.y < minBounds.y)
            {
                changeValue = 1;
                mouseChange.y = newMousePosition.y - minBounds.y;
                minSpawnBounds.y += mouseChange.y;
            }

            else if (newMousePosition.y > minBounds.y + 0.01f)
            {
                changeValue = -1;
                mouseChange.y = newMousePosition.y - minBounds.y;
                minSpawnBounds.y += mouseChange.y;
            }
        }
        
        this.meshFilter.mesh = MeshCreator.UpdateWall2DMesh(this.meshFilter.mesh, mouseChange, changeValue);
        this.RecalculateWorldBounds();
        //Debug.Log(this.id + " " + ObjectsDataRepository.planObjectsDataList.Count);
        //ObjectsDataRepository.currentSaveFile.ChangeMesh(this.meshFilter.mesh, this.id);
        //wallObjectData = ObjectsDataRepository.planObjectsDataList[this.id] as WallObjectData;
        wallObjectData.ChangeMeshProperties(this.meshFilter.mesh);
        wallObjectData.orientation = direction;
        wallObjectData.minSpawnBounds = minSpawnBounds;
        wallObjectData.maxSpawnBounds = maxSpawnBounds;
    }

    

    public override void OnMouseDown()
    {
        var pointPosition = UIController.GetUnscaledObjectPosition(-0.0003f);
        if (UIController.objectTypeMode == 2)//window
        {
            
            if (direction == Vector3.left || direction == Vector3.right)
            {
                if (pointPosition.x > minSpawnBounds.x && pointPosition.x + ObjectsParams.windowLength < maxSpawnBounds.x)
                {
                    var windowPlanObject = Instantiate(GetPrefab(3), new Vector3(pointPosition.x, minBounds.y, -0.003f), Quaternion.identity, this.transform).GetComponent<PlanObjectWindow>();
                    windowPlanObject.orientation = direction;
                    windowPlanObject.length = ObjectsParams.windowLength;
                    windowPlanObject.height = ObjectsParams.windowHeight;
                    windowPlanObject.positionHeight = ObjectsParams.windowPosition;
                    
                    windowPlanObject.CreatePlanObject(new Vector3(ObjectsParams.windowLength, maxBounds.y - minBounds.y));
                    wallObjectData.AddWallChildId(windowPlanObject.id);
                    //wallObjectData.wallChildObjectsDataList.Add(new WindowObjectData(windowPlanObject.meshFilter.mesh, windowPlanObject.transform.position, windowPlanObject.orientation, windowPlanObject.height, windowPlanObject.positionHeight, windowPlanObject.length, windowPlanObject.id));
                }

                else
                {
                    Debug.Log("Min Bounds: " + minSpawnBounds + ", Max Bounds: " + maxSpawnBounds + ", Point Position: " + pointPosition);
                }
            }
            else if (direction == Vector3.up || direction == Vector3.down)
            {
                if (pointPosition.y > minSpawnBounds.y && pointPosition.y + ObjectsParams.windowLength < maxSpawnBounds.y)
                {
                    var windowPlanObject = Instantiate(GetPrefab(3), new Vector3(minBounds.x, pointPosition.y, -0.003f), Quaternion.identity, this.transform).GetComponent<PlanObjectWindow>();
                    windowPlanObject.orientation = direction;
                    windowPlanObject.length = ObjectsParams.windowLength;
                    windowPlanObject.height = ObjectsParams.windowHeight;
                    windowPlanObject.positionHeight = ObjectsParams.windowPosition;

                    windowPlanObject.CreatePlanObject(new Vector3(maxBounds.x - minBounds.x, ObjectsParams.windowLength));
                    wallObjectData.AddWallChildId(windowPlanObject.id);
                    //wallObjectData.wallChildObjectsDataList.Add(new WindowObjectData(windowPlanObject.meshFilter.mesh, windowPlanObject.transform.position, windowPlanObject.orientation, windowPlanObject.height, windowPlanObject.positionHeight, windowPlanObject.length, windowPlanObject.id));
                }
            }
        }

       else if (UIController.objectTypeMode == 3)//door
        {
            if (direction == Vector3.left || direction == Vector3.right)
            {
                if (pointPosition.x > minSpawnBounds.x && pointPosition.x + ObjectsParams.doorLength < maxSpawnBounds.x)
                {
                    var doorPlanObject = Instantiate(GetPrefab(4), new Vector3(pointPosition.x, minBounds.y, -0.003f), Quaternion.identity, this.transform).GetComponent<PlanObjectDoor>();
                    doorPlanObject.CreatePlanObject(new Vector3(ObjectsParams.doorLength, maxBounds.y - minBounds.y));
                    doorPlanObject.orientation = direction;
                    doorPlanObject.length = ObjectsParams.doorLength;

                    wallObjectData.AddWallChildId(doorPlanObject.id);
                    //wallObjectData.wallChildObjectsDataList.Add(new DoorObjectData(doorPlanObject.meshFilter.mesh, doorPlanObject.transform.position, doorPlanObject.orientation,doorPlanObject.length, doorPlanObject.id));
                }
            }
            else if (direction == Vector3.up || direction == Vector3.down)
            {
                if (pointPosition.y > minSpawnBounds.y && pointPosition.y + ObjectsParams.doorLength < maxSpawnBounds.y)
                {
                    var doorPlanObject = Instantiate(GetPrefab(4), new Vector3(minBounds.x, pointPosition.y, -0.003f), Quaternion.identity, this.transform).GetComponent<PlanObjectDoor>();
                    doorPlanObject.orientation = direction;
                    doorPlanObject.length = ObjectsParams.doorLength;
                    doorPlanObject.CreatePlanObject(new Vector3(maxBounds.x - minBounds.x, ObjectsParams.doorLength));
                    wallObjectData.AddWallChildId(doorPlanObject.id);
                    //wallObjectData.wallChildObjectsDataList.Add(new DoorObjectData(doorPlanObject.meshFilter.mesh, doorPlanObject.transform.position, doorPlanObject.orientation, doorPlanObject.length, doorPlanObject.id));
                }
            }
        }
    }

    public override void OnMouseDrag()
    {
       // throw new System.NotImplementedException();
    }

    public override void AddAdditionalValues()
    {
        direction = Vector3.zero;
        this.name = "Simple Wall";
        this.height = ObjectsParams.wallHeight;
        wallObjectData = new WallObjectData(this.meshFilter.mesh, this.transform.position, this.direction, this.minSpawnBounds, this.maxSpawnBounds, this.height, this.id);
        ObjectsDataRepository.currentSaveFile.planObjectsDataList.Add(wallObjectData);
    }

    public void OnDestroy()
    {
        //ObjectsDataRepository.planObjectsDataList.RemoveAt(ObjectsDataRepository.planObjectsDataList.Count-1);
    }

    public override void ReAddValues(PlanObjectData planObjData)
    {
        var wallObjData = planObjData as WallObjectData;
        
        this.direction = wallObjData.orientation;
        this.height = wallObjData.height;
        this.planObjectWallChildIdList = wallObjData.wallChildsIdList;
        this.minSpawnBounds = wallObjData.minSpawnBounds;
        this.maxSpawnBounds = wallObjData.maxSpawnBounds;
    }
}
