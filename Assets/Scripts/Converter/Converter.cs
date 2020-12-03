using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Converter : MonoBehaviour
{
    public Material wallMaterial;
    public Material windowMaterial;

    private static float doorHeight = 2.4f;
    private static float wallHeight = 2.75f;
    private static float windowHeight = 2;
    private static float windowPositionY = 0.6f;
    
    private void Awake()
    {
        SpawnPlane();
        ConvertTo3D();
    }
    
    private void ConvertTo3D()
    {
        foreach (PlanObjectSimpWall wallObject in ConvertObjectTo3D.planObjectSimpWallList)
        {
            if (wallObject.planObjectObjectsList != null)
            {
                CreateWallWithObjects(wallObject);
            }

            else
            {
                CreateSimpleWall(wallObject);
                
            }
        }
    }

    

    public void CreateWallWithObjects(PlanObjectSimpWall planObjWall)
    {

        Vector3[] dividedWallVertices;
        Vector3[] objectVertices = new Vector3[4];

        
        

        Vector3 direction = planObjWall.direction;
        Vector3[] wallVertices = planObjWall.vertices;

        if (direction == Vector3.right || direction == Vector3.left)
        {

            planObjWall.planObjectObjectsList.Sort(PlanObjectObjectComparer.SortByX);
            PlanObjectObject[] objectsArray = planObjWall.planObjectObjectsList.ToArray();
            /*var sortedWindows = from obj in windowsArray
                                orderby obj.GetVertices()[0].x
                                select obj;*/

            objectVertices[2] = wallVertices[1];
            objectVertices[3] = wallVertices[0];

            for (int i = 0; i < objectsArray.Length; i++)
            {
                dividedWallVertices = new Vector3[4];
                dividedWallVertices[1] = objectVertices[2];
                dividedWallVertices[0] = objectVertices[3];

                objectVertices = objectsArray[i].vertices;

                if (objectsArray[i] is PlanObjectDoor)
                {
                    CreateGameObject(ConvertObjectTo3D.CreateHighWallObject(objectVertices, doorHeight, wallHeight), wallMaterial, "High Wall");
                }

                else if (objectsArray[i] is PlanObjectWindow)
                {
                    CreateGameObject(ConvertObjectTo3D.CreateLowWallObject(objectVertices, windowPositionY), wallMaterial, "Low Wall");
                    CreateGameObject(ConvertObjectTo3D.CreateWindowGameObject(objectVertices, windowPositionY, windowHeight), windowMaterial, "Window");
                    CreateGameObject(ConvertObjectTo3D.CreateHighWallObject(objectVertices, windowPositionY + windowHeight, wallHeight), wallMaterial, "High Wall");
                }

                dividedWallVertices[2] = objectVertices[1];
                dividedWallVertices[3] = objectVertices[0];

                CreateGameObject(ConvertObjectTo3D.CreateSimpleWallGameObject(dividedWallVertices, wallHeight), wallMaterial, "Simple Wall");
            }

            dividedWallVertices = new Vector3[4];
            dividedWallVertices[0] = objectVertices[3];
            dividedWallVertices[1] = objectVertices[2];
            dividedWallVertices[2] = wallVertices[2];
            dividedWallVertices[3] = wallVertices[3];

            CreateGameObject(ConvertObjectTo3D.CreateSimpleWallGameObject(dividedWallVertices, wallHeight), wallMaterial, "Simple Wall");
        }

        else if (direction == Vector3.up || direction == Vector3.down)
        {
            planObjWall.planObjectObjectsList.Sort(PlanObjectObjectComparer.SortByY);
            PlanObjectObject[] objectsArray = planObjWall.planObjectObjectsList.ToArray();

            objectVertices[1] = wallVertices[0];
            objectVertices[2] = wallVertices[3];

            for (int i =0; i< objectsArray.Length; i++)
            {
                dividedWallVertices = new Vector3[4];
                dividedWallVertices[0] = objectVertices[1];
                dividedWallVertices[3] = objectVertices[2];

                objectVertices = objectsArray[i].vertices;

                if (objectsArray[i] is PlanObjectDoor)
                {
                    CreateGameObject(ConvertObjectTo3D.CreateHighWallObject(objectVertices, doorHeight, wallHeight), wallMaterial, "High Wall");
                }

                else if (objectsArray[i] is PlanObjectWindow)
                {
                    CreateGameObject(ConvertObjectTo3D.CreateLowWallObject(objectVertices, windowPositionY), wallMaterial, "Low Wall");
                    CreateGameObject(ConvertObjectTo3D.CreateWindowGameObject(objectVertices, windowPositionY, windowHeight), windowMaterial, "Window");
                    CreateGameObject(ConvertObjectTo3D.CreateHighWallObject(objectVertices, windowPositionY + windowHeight, wallHeight), wallMaterial, "High Wall");
                }

                dividedWallVertices[1] = objectVertices[0];
                dividedWallVertices[2] = objectVertices[3];

                CreateGameObject(ConvertObjectTo3D.CreateSimpleWallGameObject(dividedWallVertices, wallHeight), wallMaterial, "Simple Wall " + i);
            }

            dividedWallVertices = new Vector3[4];

            dividedWallVertices[0] = objectVertices[1];
            dividedWallVertices[3] = objectVertices[2];

            dividedWallVertices[1] = wallVertices[1];
            dividedWallVertices[2] = wallVertices[2];

            CreateGameObject(ConvertObjectTo3D.CreateSimpleWallGameObject(dividedWallVertices, wallHeight), wallMaterial, "Last Simple Wall");
        }
    }

    private void CreateGameObject(Mesh mesh, Material material, string name)
    {
        GameObject newGameObject = new GameObject(name);
        newGameObject.AddComponent<MeshFilter>().mesh = mesh;
        newGameObject.AddComponent<MeshRenderer>().material = material;
    }
    private void CreateSimpleWall(PlanObjectSimpWall planObjWall)
    {
        CreateGameObject(ConvertObjectTo3D.CreateSimpleWallGameObject(planObjWall.vertices, wallHeight), wallMaterial, "Not Divided Simple Wall");
        
    }
    private void SpawnPlane()
    {
        GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        plane.transform.position = new Vector3(500, 0, 500);
    }
}
