using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public static int objectTypeMode = 0;
    public static int createTypeMode = 0;

    public static Vector3 GetScaledObjectPosition(float zDepth)
    {
        Vector3 pointPosition = Input.mousePosition;
        pointPosition.z = -Camera.main.transform.position.z + zDepth;
        return MeshCreator.GetScaledStartPoint(Camera.main.ScreenToWorldPoint(pointPosition));
    }

    public static Vector3 GetUnscaledObjectPosition(float zDepth)
    {
        Vector3 pointPosition = Input.mousePosition;
        pointPosition.z = -Camera.main.transform.position.z + zDepth;
        return MeshCreator.GetUnscaledStartPoint(Camera.main.ScreenToWorldPoint(pointPosition));
    }

    public TMP_InputField topInputField;
    public TMP_InputField bottomInputField;
    public TMP_Text topText;
    public TMP_Text bottomText;
    public TMP_Dropdown objectTypeDropdown;
    public TMP_Dropdown modeTypeDropdown;
    // Start is called before the first frame update
    void Start()
    {
        objectTypeDropdown.value = 0;
        modeTypeDropdown.value = 0;
        ObjectsParams.windowLength = 120;
        ObjectsParams.doorLength = 60;
        //topInputField.placeholder.GetComponent<TMP_Text>().text = ((int)StaticClass.GetScale().x).ToString();
        //bottomInputField.placeholder.GetComponent<TMP_Text>().text = ((int)StaticClass.GetScale().y).ToString();
        topText.text = "Floor Scale: 1x1 m ";
        topInputField.gameObject.SetActive(false);
        bottomText.gameObject.SetActive(false);
        bottomInputField.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeObjectType(TMP_Dropdown dropdown)
    {
        objectTypeMode = dropdown.value;
        Debug.Log("Mode: " + objectTypeMode);
        if (dropdown.value == 1)//wall
        {
            topText.text = "Length X, m: ";
            bottomText.gameObject.SetActive(true);
            bottomText.text = "Length Y, m: ";
            bottomInputField.gameObject.SetActive(true);
            topInputField.gameObject.SetActive(true);
            topInputField.text = ObjectsParams.scale.x.ToString();
            bottomInputField.text = ObjectsParams.scale.y.ToString();
            Debug.Log("Scale: " + ObjectsParams.scale);
        }

        else if (dropdown.value == 2)//window
        {
            topInputField.gameObject.SetActive(true);
            bottomText.gameObject.SetActive(false);
            bottomInputField.gameObject.SetActive(false);
            topText.text = "Window Length, m: ";
            topInputField.placeholder.GetComponent<TMP_Text>().text = ObjectsParams.windowLength.ToString();
            topInputField.text = ObjectsParams.windowLength.ToString();
            Debug.Log("Scale: " + ObjectsParams.windowLength);

        }

        else if (dropdown.value == 3)//door
        {
            topInputField.text = (ObjectsParams.doorLength / 0.01f).ToString();
            topText.text = "Door Length, m: ";
            topInputField.placeholder.GetComponent<TMP_Text>().text = ObjectsParams.doorLength.ToString();
            bottomText.gameObject.SetActive(false);
            bottomInputField.gameObject.SetActive(false);
        }

        else if (dropdown.value == 0)//floor
        {
            topText.text = "Floor";
            topInputField.gameObject.SetActive(false);
            bottomText.gameObject.SetActive(false);
            bottomInputField.gameObject.SetActive(false);
            Debug.Log("Scale: " + ObjectsParams.scale);
        }
    }

    public void GetTopInput(TMP_InputField inputField)
    {
        //0 - get anchor X
        //2 - get window Length
        //3 - get door length
        if (objectTypeMode == 1)
        {
            //inputField.text = StaticClass.GetScale().x.ToString();
            //StaticClass.ChangeScaleX(int.Parse(inputField.text));
            ObjectsParams.scale.x = float.Parse(inputField.text);
            Debug.Log("Object Params Scale: " + ObjectsParams.scale.x);
            //inputField.placeholder.GetComponent<TMP_Text>().text = ((int)StaticClass.GetScale().x).ToString();
        }

        if (objectTypeMode == 2)
        {
            //inputField.text = StaticClass.windowLength.ToString();
            ObjectsParams.windowLength = float.Parse(inputField.text);
            //inputField.placeholder.GetComponent<TMP_Text>().text = StaticClass.windowLength.ToString();
        }

        if (objectTypeMode == 3)
        {
            //inputField.text = StaticClass.doorLength.ToString();
            ObjectsParams.doorLength = float.Parse(inputField.text);
            //inputField.placeholder.GetComponent<TMP_Text>().text = StaticClass.doorLength.ToString();
        }
    }

    public void GetBottomInput(TMP_InputField inputField)
    {
        //0 - get anchor X
        if (objectTypeMode == 1)
        {
            //inputField.text = StaticClass.GetScale().y.ToString();
            ObjectsParams.scale.y = float.Parse(inputField.text);
            //inputField.placeholder.GetComponent<TMP_Text>().text = ((int)StaticClass.GetScale().y).ToString();
        }
    }

    public void Undo()
    {
        Debug.Log("Count: " + Plane.PlanObjectsList.Count);
        Plane.PlanObjectsList[Plane.PlanObjectsList.Count-1].DestroyThisObject();
        Plane.PlanObjectsList.RemoveAt(Plane.PlanObjectsList.Count - 1);
        
        Debug.Log(Plane.PlanObjectsList.Count);
    }
}
