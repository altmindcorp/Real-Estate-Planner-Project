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
        StaticClass.ChangeScaleX(4);
        StaticClass.ChangeScaleY(4);
        ObjectsParams.windowLength = 8;
        ObjectsParams.doorLength = 6;
        topInputField.placeholder.GetComponent<TMP_Text>().text = ((int)StaticClass.GetScale().x).ToString();
        bottomInputField.placeholder.GetComponent<TMP_Text>().text = ((int)StaticClass.GetScale().y).ToString();
        topText.text = "Anchor X length, sm: ";
        bottomText.text = "Anchor Y length, sm: ";
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
            topText.text = "Length X, sm: ";
            bottomText.text = "Length Y, sm: ";
            bottomText.gameObject.SetActive(true);
            bottomInputField.gameObject.SetActive(true);
            //topInputField.placeholder.GetComponent<TMP_Text>().text = ((int)StaticClass.GetScale().x).ToString();
            topInputField.text = ObjectsParams.scale.x.ToString();
            //bottomInputField.placeholder.GetComponent<TMP_Text>().text = ((int)StaticClass.GetScale().y).ToString();
            bottomInputField.text = ObjectsParams.scale.y.ToString();
        }

        else if (dropdown.value == 2)//window
        {
            topText.text = "Window Length, sm: ";
            topInputField.placeholder.GetComponent<TMP_Text>().text = ObjectsParams.windowLength.ToString();
            topInputField.text = ObjectsParams.windowLength.ToString();
            bottomText.gameObject.SetActive(false);
            bottomInputField.gameObject.SetActive(false);
        }

        else if (dropdown.value == 3)//door
        {
            topInputField.text = ObjectsParams.doorLength.ToString();
            topText.text = "Door Length, sm: ";
            topInputField.placeholder.GetComponent<TMP_Text>().text = ObjectsParams.doorLength.ToString();
            bottomText.gameObject.SetActive(false);
            bottomInputField.gameObject.SetActive(false);
        }

        else if (dropdown.value == 0)//floor
        {
            //topInputField.text = StaticClass.doorLength.ToString();
            topText.text = "Floor";
            //topInputField.placeholder.GetComponent<TMP_Text>().text = StaticClass.doorLength.ToString();
            bottomText.gameObject.SetActive(false);
            bottomInputField.gameObject.SetActive(false);
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
            ObjectsParams.scale.x = int.Parse(inputField.text) * 0.01f;
            Debug.Log("Object Params Scale: " + ObjectsParams.scale.x);
            //inputField.placeholder.GetComponent<TMP_Text>().text = ((int)StaticClass.GetScale().x).ToString();
        }

        if (objectTypeMode == 2)
        {
            //inputField.text = StaticClass.windowLength.ToString();
            ObjectsParams.windowLength = int.Parse(inputField.text) * 0.01f;
            //inputField.placeholder.GetComponent<TMP_Text>().text = StaticClass.windowLength.ToString();
        }

        if (objectTypeMode == 3)
        {
            //inputField.text = StaticClass.doorLength.ToString();
            ObjectsParams.doorLength = int.Parse(inputField.text) * 0.01f;
            //inputField.placeholder.GetComponent<TMP_Text>().text = StaticClass.doorLength.ToString();
        }
    }

    public void GetBottomInput(TMP_InputField inputField)
    {
        //0 - get anchor X
        if (objectTypeMode == 1)
        {
            //inputField.text = StaticClass.GetScale().y.ToString();
            ObjectsParams.scale.y = int.Parse(inputField.text) * 0.01f;
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
