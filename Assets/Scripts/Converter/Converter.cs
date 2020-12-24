using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Converter : MonoBehaviour
{
    public Material wallMaterial;
    public Material windowMaterial;
    public Material floorMaterial;
    public Material ceilingMaterial;
    public GameObject PlayerContainer;
    public GameObject doorPrefab;
    //private static float wallHeight = 2.75f;  
    //private static float windowHeight = 2;
    //private static float windowPositionY = 0.6f;
    
    private void Awake()
    {
        SpawnPlane();
        ConvertTo3D();
        PlayerContainer.transform.position = new Vector3 (ObjectsDataRepository.spawnPointPosition.x, 0, ObjectsDataRepository.spawnPointPosition.y); 

    }
    
    private void ConvertTo3D()
    {
        Debug.Log("Count: " + ObjectsDataRepository.planObjectsDataList.Count);
        foreach (PlanObjectData objData in ObjectsDataRepository.planObjectsDataList)
        {
            ConvertObject(objData);
        }


        foreach (Floor floorObject in ConvertObjectTo3D.floorObjectsList)
        {
            CreateFloor(floorObject);
        }
    }

    

    public void CreateWallWithObjects(WallObjectData planObjWallData)
    {
        Vector3[] objectVertices = new Vector3[4];
        Vector3 direction = planObjWallData.orientation;
        Vector3[] wallVertices = planObjWallData.mesh.vertices;

        if (direction == Vector3.right || direction == Vector3.left)
        {
            
            planObjWallData.wallChildObjectsDataList.Sort(PlanObjectObjectComparer.SortByX);
            
            var sortedWindows = from obj in planObjWallData.wallChildObjectsDataList
                                orderby obj.position.x
                                select obj;
            WallChildObjectData[] objectsArray = sortedWindows.ToArray();
            var wallWidth = planObjWallData.mesh.vertices[2].y;
            objectVertices[1] = planObjWallData.mesh.vertices[1];
            objectVertices[0] = planObjWallData.mesh.vertices[0];

            Vector3 wallPosition = planObjWallData.position;
            Vector3 windowPosition = Vector3.zero;
            float windowLength = 0;

            for (int i = 0; i < objectsArray.Length; i++)
            {
                windowLength = objectsArray[i].length;
                windowPosition = objectsArray[i].position;

                objectVertices[2] = windowPosition - wallPosition;
                objectVertices[2].y += wallWidth;
                objectVertices[3] = windowPosition - wallPosition;

                CreateGameObject(ConvertObjectTo3D.CreateSimpleWallGameObject(objectVertices, planObjWallData.height), wallMaterial, wallPosition, "Divided Wall");//wall

                

                objectVertices[0] = objectsArray[i].mesh.vertices[0];
                objectVertices[1] = objectsArray[i].mesh.vertices[1];

                objectVertices[2].x = objectVertices[1].x + windowLength;
                objectVertices[3].x = objectVertices[0].x + windowLength;

                wallPosition.x = windowPosition.x + windowLength;

                //objectVertices = objectsArray[i].vertices;

                if (objectsArray[i] is DoorObjectData)
                {
                    Debug.Log("Door Object");
                    Instantiate(doorPrefab, new Vector3(objectsArray[i].position.x + objectsArray[i].length / 2, 0, objectsArray[i].position.y), Quaternion.identity);
                }

                if (objectsArray[i] is WindowObjectData)
                {
                    var windowObject = objectsArray[i] as WindowObjectData;
                    CreateGameObject(ConvertObjectTo3D.CreateLowWallObject(objectVertices, windowObject.positionHeight), wallMaterial, windowPosition, "Low Wall");
                    CreateGameObject(ConvertObjectTo3D.CreateWindowGameObject(objectVertices, windowObject.positionHeight, windowObject.height), windowMaterial, windowPosition, "Window");
                    CreateGameObject(ConvertObjectTo3D.CreateHighWallObject(objectVertices, windowObject.positionHeight + windowObject.height, planObjWallData.height), wallMaterial, windowPosition, "High Wall");
                }

                objectVertices[0] = Vector3.zero;
                objectVertices[1] = Vector3.zero + Vector3.up * wallWidth;
            }

            wallPosition.x = windowPosition.x + windowLength;

            objectVertices[2].x = planObjWallData.position.x + planObjWallData.mesh.vertices[2].x - (windowPosition.x + windowLength);
            objectVertices[3].x = planObjWallData.position.x + planObjWallData.mesh.vertices[3].x - (windowPosition.x + windowLength);

            CreateGameObject(ConvertObjectTo3D.CreateSimpleWallGameObject(objectVertices, planObjWallData.height), wallMaterial, wallPosition, "Simple Wall");
        }

        else if (direction == Vector3.up || direction == Vector3.down)
        {
            planObjWallData.wallChildObjectsDataList.Sort(PlanObjectObjectComparer.SortByX);

            var sortedWindows = from obj in planObjWallData.wallChildObjectsDataList
                                orderby obj.position.y
                                select obj;
            WallChildObjectData[] objectsArray = sortedWindows.ToArray();
            var wallWidth = planObjWallData.mesh.vertices[2].x;
            objectVertices[0] = planObjWallData.mesh.vertices[0];
            objectVertices[3] = planObjWallData.mesh.vertices[3];

            Vector3 wallPosition = planObjWallData.position;
            Vector3 windowPosition = Vector3.zero;
            float windowLength = 0;

            for (int i = 0; i < objectsArray.Length; i++)
            {
                windowLength = objectsArray[i].length;
                windowPosition = objectsArray[i].position;

                objectVertices[1] = windowPosition - wallPosition;
                objectVertices[2] = windowPosition - wallPosition;
                objectVertices[2].x += wallWidth;

                CreateGameObject(ConvertObjectTo3D.CreateSimpleWallGameObject(objectVertices, planObjWallData.height), wallMaterial, wallPosition, "Divided Wall");//wall



                objectVertices[0] = objectsArray[i].mesh.vertices[0];
                objectVertices[3] = objectsArray[i].mesh.vertices[3];

                objectVertices[1].y = objectVertices[0].y + windowLength;
                objectVertices[2].y = objectVertices[3].y + windowLength;

                wallPosition.y = windowPosition.y + windowLength;

                //objectVertices = objectsArray[i].vertices;

                if (objectsArray[i] is DoorObjectData)
                {
                    Instantiate(doorPrefab, new Vector3(objectsArray[i].position.x, 0, objectsArray[i].position.y + objectsArray[i].length / 2), Quaternion.Euler(0, 90, 0));
                }

                else if (objectsArray[i] is WindowObjectData)
                {
                    var windowObject = objectsArray[i] as WindowObjectData;
                    CreateGameObject(ConvertObjectTo3D.CreateLowWallObject(objectVertices, windowObject.positionHeight), wallMaterial, windowPosition, "Low Wall");
                    CreateGameObject(ConvertObjectTo3D.CreateWindowGameObject(objectVertices, windowObject.positionHeight, windowObject.height), windowMaterial, windowPosition, "Window");
                    CreateGameObject(ConvertObjectTo3D.CreateHighWallObject(objectVertices, windowObject.positionHeight + windowObject.height, planObjWallData.height), wallMaterial, windowPosition, "High Wall");
                }

                objectVertices[0] = Vector3.zero;
                objectVertices[3] = Vector3.zero + Vector3.right * wallWidth;
            }

            wallPosition.y = windowPosition.y + windowLength;

            objectVertices[1].y = planObjWallData.position.y + planObjWallData.mesh.vertices[1].y - (windowPosition.y + windowLength);
            objectVertices[2].y = planObjWallData.position.y + planObjWallData.mesh.vertices[2].y - (windowPosition.y + windowLength);

            CreateGameObject(ConvertObjectTo3D.CreateSimpleWallGameObject(objectVertices, planObjWallData.height), wallMaterial, wallPosition, "Simple Wall");
        }
    }

    private void CreateGameObject(Mesh mesh, Material material, Vector3 position, string name)
    {
        GameObject newGameObject = new GameObject(name);
        newGameObject.AddComponent<MeshFilter>().mesh = mesh;
        newGameObject.AddComponent<MeshRenderer>().material = material;
        newGameObject.AddComponent<MeshCollider>().sharedMesh = mesh;
        newGameObject.transform.Translate(new Vector3(position.x, 0, position.y));
        
    }

    private void CreateGameObject(Mesh mesh, Material material, Vector3 position, float height, string name)
    {
        GameObject newGameObject = new GameObject(name);
        newGameObject.AddComponent<MeshFilter>().mesh = mesh;
        newGameObject.AddComponent<MeshRenderer>().material = material;
        newGameObject.AddComponent<MeshCollider>().sharedMesh = mesh;
        newGameObject.transform.Translate(new Vector3(position.x, height, position.y));

    }

    private void CreateSimpleWall(PlanObjectSimpWall planObjWall)
    {
       //CreateGameObject(ConvertObjectTo3D.CreateSimpleWallGameObject(planObjWall.vertices, wallHeight), wallMaterial, "Not Divided Simple Wall");
        
    }

    private void CreateSimpleWall(WallObjectData wallData)
    {

    }

    private void ConvertObject(PlanObjectData planObj)
    {
        if (planObj is WallObjectData)
        {
            var wallPlanObjData = planObj as WallObjectData;
            
            if (wallPlanObjData.wallChildObjectsDataList.Count!=0)
            {                
                CreateWallWithObjects(wallPlanObjData);
            }

            else
            {
                CreateGameObject(ConvertObjectTo3D.CreateSimpleWallGameObject(planObj.mesh, 2.75f), wallMaterial, planObj.position, "Simple Wall");
            }
            
        }

        else if (planObj is FloorObjectData)
        {
            CreateGameObject(ConvertObjectTo3D.CreateFloorGameObject(planObj.mesh), floorMaterial, planObj.position, "Floor");
            CreateGameObject(ConvertObjectTo3D.CreateCeiling(planObj.mesh), ceilingMaterial, planObj.position, 2.75f, "Ceiling");
            Debug.Log("Floor Position: " + planObj.position);
        }
    }

    private void CreateFloor(Floor floorObject)
    {
        //CreateGameObject(ConvertObjectTo3D.CreateFloorGameObject(floorObject.meshFilter.mesh), floorMaterial, "Floor");
        Destroy(floorObject);
    }
    private void SpawnPlane()
    {
        GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        plane.transform.position = new Vector3(500, 0, 500);
    }
}
