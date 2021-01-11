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

    private bool objectsLoaded = false;
    List<WallChildObjectData> wallChildObjectDataList = new List<WallChildObjectData>();



    private void Awake()
    {
        //Create3DScene();
        //_ = GameObject.CreatePrimitive(PrimitiveType.Cube);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space) && !objectsLoaded)
        {
            objectsLoaded = true;
            ObjectsDataRepository.LoadSaveFile("testsave");
            Create3DScene();
        }
    }
    //load from current save
    private void ConvertTo3D()
    {
        //Debug.Log("Count: " + ObjectsDataRepository.currentSaveFile.planObjectsDataList.Count);
        foreach (PlanObjectData objData in ObjectsDataRepository.currentSaveFile.planObjectsDataList)
        {
            ConvertObject(objData);
        }

        //Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), new Vector3(ObjectsDataRepository.currentSaveFile.spawnPosition.x, 0, ObjectsDataRepository.currentSaveFile.spawnPosition.y), Quaternion.identity);
        /*foreach (Floor floorObject in ConvertObjectTo3D.floorObjectsList)
        {
            CreateFloor(floorObject);
        }*/
    }

    public void Create3DScene()
    {
        //SpawnPlane();
        ConvertTo3D();
        PlayerContainer.transform.position = new Vector3(ObjectsDataRepository.currentSaveFile.spawnPosition.x, 1, ObjectsDataRepository.currentSaveFile.spawnPosition.y);
    }

    public void CreateWallWithObjects(WallObjectData planObjWallData)
    {
        Vector3[] objectVertices = new Vector3[4];
        Vector3 direction = planObjWallData.orientation;
        Vector3[] wallVertices = planObjWallData.GetVertices();

        if (direction == Vector3.right || direction == Vector3.left)
        {
            wallChildObjectDataList.Sort(PlanObjectObjectComparer.SortByX);

            var sortedWindows = from obj in wallChildObjectDataList
                                orderby obj.position.x
                                select obj;


            WallChildObjectData[] objectsArray = sortedWindows.ToArray();
            var wallWidth = planObjWallData.GetVertices()[2].y;
            objectVertices[1] = planObjWallData.GetVertices()[1];
            objectVertices[0] = planObjWallData.GetVertices()[0];

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


                
                objectVertices[0] = objectsArray[i].GetVertices()[0];
                objectVertices[1] = objectsArray[i].GetVertices()[1];

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

            objectVertices[2].x = planObjWallData.position.x + planObjWallData.GetVertices()[2].x - (windowPosition.x + windowLength);
            objectVertices[3].x = planObjWallData.position.x + planObjWallData.GetVertices()[3].x - (windowPosition.x + windowLength);

            CreateGameObject(ConvertObjectTo3D.CreateSimpleWallGameObject(objectVertices, planObjWallData.height), wallMaterial, wallPosition, "Simple Wall");
        }

        else if (direction == Vector3.up || direction == Vector3.down)
        {
            wallChildObjectDataList.Sort(PlanObjectObjectComparer.SortByY);

            var sortedWindows = from obj in wallChildObjectDataList
                                orderby obj.position.y
                                select obj;
            WallChildObjectData[] objectsArray = sortedWindows.ToArray();
            var wallWidth = planObjWallData.GetVertices()[2].x;
            objectVertices[0] = planObjWallData.GetVertices()[0];
            objectVertices[3] = planObjWallData.GetVertices()[3];

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



                objectVertices[0] = objectsArray[i].GetVertices()[0];
                objectVertices[3] = objectsArray[i].GetVertices()[3];

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

            objectVertices[1].y = planObjWallData.position.y + planObjWallData.GetVertices()[1].y - (windowPosition.y + windowLength);
            objectVertices[2].y = planObjWallData.position.y + planObjWallData.GetVertices()[2].y - (windowPosition.y + windowLength);

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
        newGameObject.transform.parent = this.transform;
    }

    private void CreateGameObject(Mesh mesh, Material material, Vector3 position, float height, string name)
    {
        GameObject newGameObject = new GameObject(name);
        newGameObject.AddComponent<MeshFilter>().mesh = mesh;
        newGameObject.AddComponent<MeshRenderer>().material = material;
        newGameObject.AddComponent<MeshCollider>().sharedMesh = mesh;
        newGameObject.transform.Translate(new Vector3(position.x, height, position.y));
        newGameObject.transform.parent = this.transform;
    }

    private void ConvertObject(PlanObjectData planObjData)
    {
        if (planObjData is WallObjectData)
        {
            wallChildObjectDataList.Clear();
            var wallPlanObjData = planObjData as WallObjectData;
            Debug.Log("Child Count in Converter: " + (ObjectsDataRepository.currentSaveFile.planObjectsDataList.Find(x => x.id == planObjData.id) as WallObjectData).wallChildsIdList.Count);
            Debug.Log("Wall id in Converter: " + wallPlanObjData.id);
            foreach (int id in wallPlanObjData.wallChildsIdList)
            {
                Debug.Log("Object id in converter: " + id);
                wallChildObjectDataList.Add((WallChildObjectData)ObjectsDataRepository.currentSaveFile.planObjectsDataList.Find(x => x.id == id));
            }

            //Debug.Log("Wall Child objects count: " + wallChildObjectDataList.Count);

            if (wallChildObjectDataList.Count!=0)
            {                
                CreateWallWithObjects(wallPlanObjData);
            }

            else
            {
                CreateGameObject(ConvertObjectTo3D.CreateSimpleWallGameObject(planObjData.GetVertices(), 2.75f), wallMaterial, planObjData.position, "Simple Wall");
            }
            
        }

        else if (planObjData is FloorObjectData)
        {
            CreateGameObject(ConvertObjectTo3D.CreateFloorGameObject(planObjData.GetMesh()), floorMaterial, planObjData.position, "Floor");
            CreateGameObject(ConvertObjectTo3D.CreateCeiling(planObjData.GetMesh()), ceilingMaterial, planObjData.position, 2.75f, "Ceiling");
            //Debug.Log("Floor Position: " + planObjData.position);
        }
    }

    private void SpawnPlane()
    {
        GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        plane.transform.position = new Vector3(500, 0, 500);
    }
}
