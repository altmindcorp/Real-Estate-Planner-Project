﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pointer : MonoBehaviour
{
    public TMP_Dropdown modeDropdown;//0 - add, 1 - delete, 2 - move
    public TMP_Dropdown objectTypeDropdown;//0 - add anchor, 1- add wall, 2 - add window
    public Vector3 scale;
    public Material anchorMaterial;
    public Material wallMaterial;
    public Material windowMaterial;
    public Material doorMaterial;
    private RaycastHit hit;
    //private Ray ray;
    //private bool mouseHold = false;

    GameObject currentGameObject;
    private PlanObject planObj;
    public Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        Plan newPlan = new Plan();
        StaticClass.SetCurrentPlan(newPlan);
    }
    Vector3 direction;
    Vector3[] objectVertices;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (modeDropdown.value == 0)//add
                { 
                    if (objectTypeDropdown.value == 0)//anchor 
                    {
                        if (hit.transform.tag!="PlanObject")
                        {
                            currentGameObject = StaticClass.CreateAnchor(GetStartPoint(hit.point), scale, anchorMaterial);
                        }                            
                    }
                    else if (objectTypeDropdown.value == 1)//wall
                    {
                        if (hit.transform.gameObject.tag == "PlanObject")
                        {
                            planObj = hit.transform.gameObject.GetComponent<PlanObject>();
                            if (planObj is PlanObjectAnchor)
                            {
                                currentGameObject = StaticClass.CreateSimpWall(planObj.GetVertices()[0],planObj.GetScale(), wallMaterial);
                            }
                        }
                    }
                    else if (objectTypeDropdown.value == 2)//object
                    {
                        if (hit.transform.gameObject.tag == "PlanObject")
                        {
                            planObj = hit.transform.gameObject.GetComponent<PlanObject>();
                            
                            if (planObj is PlanObjectSimpWall)
                            {
                                Debug.Log("Create Window");
                                currentGameObject = StaticClass.CreateWindow(GetStartPoint(hit.point, planObj.gameObject), GetWindowScale(planObj.gameObject), windowMaterial);
                            }

                            else if (planObj==null)
                            {
                                Debug.Log("PlanObj is null");
                            }
                        }
                        else
                        {
                            Debug.Log("Not Plan Object");
                        }
                    }

                    else if (objectTypeDropdown.value == 3)
                    {
                        //create door
                    }
                    if (planObj!=null)
                    {
                        objectVertices = planObj.GetMesh().vertices;
                    } 
                }

                else if (modeDropdown.value == 1)//delete
                {
                    //deleteobject
                }

                else if (modeDropdown.value == 2)//move
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
                        if (wallPlanObject.GetStartPoint() != Vector3.zero)
                        {
                            Debug.Log("Setup anchor in point: " + wallPlanObject.GetStartPoint());
                            StaticClass.CreateAnchor(wallPlanObject.GetStartPoint(), scale, anchorMaterial);
                        }
                    }
                }
                
            }
            currentGameObject = null;
            planObj = null;
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
                    
                }
            }
        }     
    }

    Vector3 GetStartPoint(Vector3 hitPoint)
    {
        return new Vector3((int)hitPoint.x, (int)hitPoint.y, hitPoint.z - 0.001f);
    }

    Vector3 GetStartPoint(Vector3 hitPoint, GameObject simpWall)
    {
        PlanObjectSimpWall planObj = simpWall.GetComponent<PlanObjectSimpWall>();
        Vector3 direction = planObj.GetDirection();
        if (direction == Vector3.left || direction == Vector3.right)
        {
            return new Vector3((int)hitPoint.x, planObj.GetVertices()[0].y, 0);
        }
        else if (direction == Vector3.up || direction == Vector3.down)
        {
            return new Vector3(planObj.GetVertices()[0].x, (int)hitPoint.y, 0);
        }
        else return Vector3.zero;
    }

    Vector3 GetWindowScale(GameObject simpWall)
    {
        Vector3 direction = simpWall.GetComponent<PlanObjectSimpWall>().GetDirection();
        Vector3 scale = simpWall.GetComponent<PlanObjectSimpWall>().GetScale();
        if (direction == Vector3.left || direction == Vector3.right)//horizontal
        {
            scale.x = StaticClass.windowLength;
            return scale;
        }
        else if (direction == Vector3.up || direction == Vector3.down)
        {
            scale.y = StaticClass.windowLength;
            return new Vector3(scale.y, scale.x, 0);
        }
        else return Vector3.zero;
    }
}
