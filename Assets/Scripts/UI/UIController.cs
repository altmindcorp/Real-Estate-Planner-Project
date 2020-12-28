using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public TextHint topText;
    public TextHint middleText;
    public TextHint bottomText;
    public Button resetSpawnPointButton;
    public TMP_Dropdown objectTypeDropdown;
    public TMP_Dropdown modeTypeDropdown;
    public float currentFloorArea = 0;

    // Start is called before the first frame update
    void Start()
    {
        objectTypeDropdown.value = 0;
        modeTypeDropdown.value = 0;
        resetSpawnPointButton.gameObject.SetActive(false);
        //ObjectsDataRepository.currentFloorArea.ToString(), "");
        ChangeActiveText(middleText, false, false, "", "");
        ChangeActiveText(bottomText, false, false, "", "");
    }

    // Update is called once per frame
    void Update()
    {
        if (objectTypeMode == 0)
        {
            var text = ObjectsDataRepository.currentFloorArea == 0 ? "Floor not selected" : ObjectsDataRepository.currentFloorArea.ToString();
            ChangeActiveText(topText, true, false, "Floor Area: " + text, "");
        }
    }

    public void ChangeObjectType(TMP_Dropdown dropdown)
    {
        objectTypeMode = dropdown.value;
        Debug.Log("Mode: " + objectTypeMode);
        if (dropdown.value == 1)//wall
        {
            ChangeActiveText(topText, true, true, "Length X, m: ", ObjectsParams.scale.x.ToString());
            ChangeActiveText(middleText, true, true, "Length Y, m: ", ObjectsParams.scale.y.ToString());
            ChangeActiveText(bottomText, true, true, "Wall Height: ", ObjectsParams.wallHeight.ToString());
            resetSpawnPointButton.gameObject.SetActive(false);
        }

        else if (dropdown.value == 2)//window
        {
            ChangeActiveText(topText, true, true, "Window Length, m: ", ObjectsParams.windowLength.ToString());
            ChangeActiveText(middleText, true, true, "Window Heigth, m: ", ObjectsParams.windowHeight.ToString());
            ChangeActiveText(bottomText, true, true, "WIndow Position: ", ObjectsParams.windowPosition.ToString());
            resetSpawnPointButton.gameObject.SetActive(false);
        }

        else if (dropdown.value == 3)//door
        {
            ChangeActiveText(topText, true, true, "Door", "");
            ChangeActiveText(middleText, false, false, "", "");
            ChangeActiveText(bottomText, false, false, "", "");
            resetSpawnPointButton.gameObject.SetActive(false);
        }

        else if (dropdown.value == 0)//floor
        {
            ChangeActiveText(topText, true, false, "Floor Area: " + ObjectsDataRepository.currentFloorArea == 0.ToString() ? "Floor not selected" : ObjectsDataRepository.currentFloorArea.ToString(), "");
            ChangeActiveText(middleText, false, false, "", "");
            ChangeActiveText(bottomText, false, false, "", "");
            resetSpawnPointButton.gameObject.SetActive(false);
        }

        else if (dropdown.value == 4)
        {
            ChangeActiveText(topText, true, false, "Spawn Point: ", ObjectsParams.spawnPointIsSet? ObjectsParams.spawnPointPosition.ToString() : "");
            ChangeActiveText(middleText, false, false, "", "");
            ChangeActiveText(bottomText, false, false, "", "");
            resetSpawnPointButton.gameObject.SetActive(true);
        }
    }


    private void ChangeActiveText(TextHint textObject, bool textIsActive, bool inputFieldIsActive, string text, string inputFieldText)
    {
        textObject.gameObject.SetActive(textIsActive);
        textObject.text.text = text;
        
        
        if (textObject.inputField != null)
        {
            textObject.inputField.gameObject.SetActive(inputFieldIsActive);
            textObject.inputField.text = inputFieldText;
        }    
        else
        {
            Debug.LogError("Input Field is Null");
        }
    }

    public void GetTopInput(TMP_InputField inputField)
    {

        if (objectTypeMode == 1)//wall
        {
            ObjectsParams.scale.x = float.Parse(inputField.text);
        }

        if (objectTypeMode == 2)//window
        {
            ObjectsParams.windowLength = float.Parse(inputField.text);
        }

        if (objectTypeMode == 3)//door
        {
            //ObjectsParams.doorLength = float.Parse(inputField.text);
        }
    }

    public void GetMiddleInput(TMP_InputField inputField)
    {

        if (objectTypeMode == 1)//wall
        {
            ObjectsParams.scale.y = float.Parse(inputField.text);
        }

        if (objectTypeMode == 2)//window
        {
            ObjectsParams.windowHeight = float.Parse(inputField.text);
        }

        if (objectTypeMode == 3)//door
        {
            //ObjectsParams.doorLength = float.Parse(inputField.text);
        }
    }

    public void GetBottomInput(TMP_InputField inputField)
    {
        //0 - get anchor X
        if (objectTypeMode == 1)//wall
        {
            ObjectsParams.wallHeight = float.Parse(inputField.text);
        }

        if (objectTypeMode == 2)//window
        {
            ObjectsParams.windowPosition = float.Parse(inputField.text);
        }
    }

    public void Undo()
    {
        Plane.PlanObjectsList[Plane.PlanObjectsList.Count - 1].DestroyThisObject();
        Plane.PlanObjectsList.RemoveAt(Plane.PlanObjectsList.Count - 1);
    }

    public void ResetSpawnPoint()
    {
        ObjectsDataRepository.currentSaveFile.spawnPosition = Vector3.zero;
        ObjectsDataRepository.currentSaveFile.spawnPositionIsSet = false;
        Destroy(GameObject.Find("Spawn Point(Clone)"));
    }
}
