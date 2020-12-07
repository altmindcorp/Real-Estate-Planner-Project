using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class Pointer : MonoBehaviour
{
    public TMP_Text measureTextHint;
    //public TMP_Text topText;
    //public TMP_Text bottomText;
    //public TMP_Dropdown modeDropdown;//0 - add, 1 - delete, 2 - move
    //public TMP_Dropdown objectTypeDropdown;//0 - add anchor, 1- add wall, 2 - add window
    //public TMP_InputField topInputField;
    //public TMP_InputField bottomInputField;
    [SerializeField]
    //private Vector3 scale;
    public Material anchorMaterial;
    public Material wallMaterial;
    public Material windowMaterial;
    public Material doorMaterial;
    public Material floorMaterial;
    public Floor floorPrefab;
    public GameObject spawnPrefab;
    //public float windowLength = 4;
    //public float doorLength = 4;
    private RaycastHit hit;
    //private Ray ray;
    //private bool mouseHold = false;

    GameObject currentGameObject;
    private PlanObject planObj;
    private Floor floorObj;
    public Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        Plan newPlan = new Plan();
        //topInputField

        //modeDropdown.value = 0;
        //objectTypeDropdown.value = 0;
        StaticClass.SetCurrentPlan(newPlan);
    }
    Vector3 direction;
    Vector3[] objectVertices;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (UIController.createTypeMode == 0)//add
                {
                    Debug.Log("Type: " + UIController.objectTypeMode);
                    if (UIController.objectTypeMode == 0)//anchor 
                    {
                        if (hit.transform.tag != "PlanObject")
                        {
                            currentGameObject = StaticClass.CreateAnchor(GetStartPoint(hit.point), StaticClass.GetScale(), anchorMaterial, measureTextHint);
                        }
                    }
                    else if (UIController.objectTypeMode == 1)//wall
                    {

                        if (hit.transform.gameObject.tag == "PlanObject")
                        {
                            planObj = hit.transform.gameObject.GetComponent<PlanObject>();
                            if (planObj is PlanObjectAnchor)
                            {
                                currentGameObject = StaticClass.CreateSimpWall(planObj.vertices[0], planObj.GetScale(), wallMaterial, measureTextHint);
                            }
                        }
                    }
                    else if (UIController.objectTypeMode == 2)//window
                    {
                        if (hit.transform.gameObject.tag == "PlanObject")
                        {
                            planObj = hit.transform.gameObject.GetComponent<PlanObject>();

                            if (planObj is PlanObjectSimpWall)
                            {
                                Debug.Log("Create Window");
                                currentGameObject = StaticClass.CreateWindow(GetStartPoint(hit.point, planObj.gameObject), GetWindowScale(planObj.gameObject), windowMaterial);
                                planObj.GetComponent<PlanObjectSimpWall>().planObjectObjectsList.Add(currentGameObject.GetComponent<PlanObjectWindow>());
                            }

                            else if (planObj == null)
                            {
                                Debug.Log("PlanObj is null");
                            }
                        }
                        else
                        {
                            //Debug.Log("Not Plan Object");
                        }
                    }

                    else if (UIController.objectTypeMode == 3)
                    {
                        if (hit.transform.gameObject.tag == "PlanObject")
                        {
                            planObj = hit.transform.gameObject.GetComponent<PlanObject>();

                            if (planObj is PlanObjectSimpWall)
                            {
                                Debug.Log("Create Door");
                                currentGameObject = StaticClass.CreateDoor(GetStartPoint(hit.point, planObj.gameObject), GetDoorScale(planObj.gameObject), doorMaterial);
                                planObj.GetComponent<PlanObjectSimpWall>().planObjectObjectsList.Add(currentGameObject.GetComponent<PlanObjectDoor>());
                            }

                            else if (planObj == null)
                            {
                                Debug.Log("PlanObj is null");
                            }
                        }
                    }

                    else if (UIController.objectTypeMode == 4)//floor
                    {
                        if (hit.transform.gameObject.tag != "PlanObject")
                        {
                            if (hit.transform.gameObject.tag == "Floor")
                            {
                                Debug.Log(hit.transform.gameObject.tag);
                                currentGameObject = hit.transform.gameObject;
                                floorObj = currentGameObject.GetComponent<Floor>();
                            }
                            else
                            {
                                Debug.Log(hit.transform.gameObject.tag);
                                //create floor
                                //FloorParams floorParams = new
                                var newFloor = Instantiate(floorPrefab.gameObject).GetComponent<Floor>();
                                newFloor.SetFloor(hit.point, floorMaterial);
                                floorObj = newFloor;
                            }
                        }
                    }

                    else if (UIController.objectTypeMode == 5)//spawn point
                    {
                        if (hit.transform.gameObject.tag == "Finish")
                        {
                            Instantiate(spawnPrefab, hit.point, Quaternion.Euler(Vector3.left*90));
                        }
                    }

                    if (planObj != null)
                    {
                        objectVertices = planObj.GetMesh().vertices;
                    }
                }

                else if (UIController.createTypeMode == 1)//delete
                {
                    //deleteobject
                }

                else if (UIController.createTypeMode == 2)//move
                {
                    //moveobject
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (planObj is PlanObjectAnchor)
            {
                if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    if (hit.transform.gameObject.GetComponent<PlanObject>() is PlanObjectAnchor)
                    {

                    }
                    else
                    {

                        var wallPlanObject = currentGameObject.GetComponent<PlanObjectSimpWall>();
                        //Debug.Log("Wall OBj Direction: " + wallPlanObject.GetDirection());
                        if (wallPlanObject.GetStartPoint() != Vector3.zero)
                        {
                            //Debug.Log("Setup anchor in point: " + wallPlanObject.GetStartPoint());
                            StaticClass.CreateAnchor(wallPlanObject.GetStartPoint(), StaticClass.GetScale(), anchorMaterial, measureTextHint);
                        }

                        else
                        {
                            Debug.LogError("No Start Point");
                        }
                    }
                }

            }
            currentGameObject = null;
            planObj = null;
            floorObj = null;
        }

        if (Input.GetMouseButton(0))
        {
            if (currentGameObject != null)
            {
                if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    if (planObj is PlanObjectAnchor)
                    {
                        direction = StaticClass.GetDirection(objectVertices, hit.point);
                        if (direction != Vector3.zero)
                        {
                            StaticClass.UpdateSimpWall(currentGameObject, hit.point, direction);
                        }
                    }
                    else if (floorObj != null)
                    {
                        Debug.Log("Updating Floor");
                        floorObj.UpdateFloor(0, hit.point);
                    }

                }
            }
        }
    }

    Vector3 GetStartPoint(Vector3 hitPoint)
    {
        Debug.Log("HitPoint: " + hitPoint.x.ToString("F5") + " " + hitPoint.y.ToString("F5"));
        if (GridScaler.mode == 0)
        {
            var newCoords = new Vector3((int)(hit.point.x / 0.01f) * 0.01f, (int)(hit.point.y / 0.01f) * 0.01f, hit.point.z);
            Debug.Log("New Coords: " + newCoords.x.ToString("F5") + " " + newCoords.y.ToString("F5"));
            return newCoords;

        }

        else if (GridScaler.mode == 1)
        {
            var newCoords = new Vector3((int)(hit.point.x / 0.1f) * 0.1f, (int)(hit.point.y / 0.1f) * 0.1f, hit.point.z);
            Debug.Log("New Coords: " + newCoords.x.ToString("F5") + " " + newCoords.y.ToString("F5"));
            return newCoords;
        }

        else if (GridScaler.mode == 2)
        {
            var newCoords = new Vector3((int)hitPoint.x, (int)hitPoint.y, hitPoint.z);
            Debug.Log("New Coords: " + newCoords.x.ToString("F5") + " " + newCoords.y.ToString("F5"));
            return newCoords;
        }

        return Vector3.zero;
    }

    Vector3 GetStartPoint(Vector3 hitPoint, GameObject simpWall)
    {
        PlanObjectSimpWall planObj = simpWall.GetComponent<PlanObjectSimpWall>();
        Vector3 direction = planObj.direction;
        Vector3 startPoint = GetStartPoint(hitPoint);
        if (direction == Vector3.left || direction == Vector3.right)
        {

            return new Vector3(startPoint.x, planObj.vertices[0].y, 0);
        }
        else if (direction == Vector3.up || direction == Vector3.down)
        {
            return new Vector3(planObj.vertices[0].x, startPoint.y, 0);
        }
        else return Vector3.zero;
    }

    Vector3 GetObjectScale(GameObject simpWall)
    {
        Vector3 direction = simpWall.GetComponent<PlanObjectSimpWall>().direction;
        Vector3 scale = simpWall.GetComponent<PlanObjectSimpWall>().GetScale();
        if (direction == Vector3.left || direction == Vector3.right)//horizontal
        {
            Debug.Log("Direction: " + direction);
            scale.x = StaticClass.windowLength;
            return scale;
        }
        else if (direction == Vector3.up || direction == Vector3.down)
        {
            Debug.Log("Direction: " + direction);
            scale.x = scale.y;
            scale.y = StaticClass.windowLength;

            return scale;
        }
        else return Vector3.zero;
    }

    Vector3 GetDoorScale(GameObject simpWall)
    {
        Vector3 direction = simpWall.GetComponent<PlanObjectSimpWall>().direction;
        Vector3 scale = simpWall.GetComponent<PlanObjectSimpWall>().GetScale();
        if (direction == Vector3.left || direction == Vector3.right)//horizontal
        {
            Debug.Log("Direction: " + direction);
            scale.x = StaticClass.doorLength;
            return scale;
        }
        else if (direction == Vector3.up || direction == Vector3.down)
        {
            Debug.Log("Direction: " + direction);
            scale.x = scale.y;
            scale.y = StaticClass.doorLength;

            return scale;
        }
        else return Vector3.zero;
    }

    Vector3 GetWindowScale(GameObject simpWall)
    {
        Vector3 direction = simpWall.GetComponent<PlanObjectSimpWall>().direction;
        Vector3 scale = simpWall.GetComponent<PlanObjectSimpWall>().GetScale();
        if (direction == Vector3.left || direction == Vector3.right)//horizontal
        {
            Debug.Log("Direction: " + direction);
            scale.x = StaticClass.windowLength;
            return scale;
        }
        else if (direction == Vector3.up || direction == Vector3.down)
        {
            Debug.Log("Direction: " + direction);
            scale.x = scale.y;
            scale.y = StaticClass.windowLength;

            return scale;
        }
        else return Vector3.zero;
    }

    void ChangeText(int value)
    {
        if (value == 0)
        {

        }
    }

    public void GetTopInput(TMP_InputField inputField)
    {
        //if object type == 0 => anchor scale X = input.value
        //if 2 => window length = input.value
        // if 3 => door length = input.value
    }

    public void GetBottomInput(TMP_InputField inputField)
    {
        //if object type == 0 => anchor scale Y = input.value
        //if 2 || 3 => disable input
    }
}
