using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float cameraChange = 0.5f;
    private float distanceSM_DM = -0.4f;
    private float distanceDM_M = -3.4f;
    public delegate void OnDistanceChanged(int change);
    public static event OnDistanceChanged onDistanceChanged;
    public Grid grid;
    private TMPro.TMP_Text scaleText;
    // Start is called before the first frame update
    private void Start()
    {
        scaleText = GameObject.Find("ScaleModeText").GetComponent<TMPro.TMP_Text>();
        scaleText.text = "Grid Cell Scale: 1 sm";
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.mouseScrollDelta.y>0 && transform.position.z < -0.15f)
        {
            transform.Translate(Vector3.forward * cameraChange * GridScaler.scaleValue);
        }

        if (Input.mouseScrollDelta.y<0)
        {
            transform.Translate(Vector3.back * cameraChange * GridScaler.scaleValue);
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * GridScaler.scaleValue);
            grid.transform.Translate(Vector3.left * GridScaler.scaleValue);
            //grid translate
        }

        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * GridScaler.scaleValue);
            grid.transform.Translate(Vector3.right * GridScaler.scaleValue);
            //grid translate
        }

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * GridScaler.scaleValue);
            grid.transform.Translate(Vector3.up * GridScaler.scaleValue);
            //grid translate
        }

        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * GridScaler.scaleValue);
            grid.transform.Translate(Vector3.down * GridScaler.scaleValue);
            //grid translate
        }

        ChangeMode();
    }

    private void ChangeMode()
    {
        if (GridScaler.mode != 1 && transform.position.z < distanceSM_DM && transform.position.z > distanceDM_M)
        {
            if (GridScaler.mode == 0)
            {
                onDistanceChanged?.Invoke(1);
            }

            else if (GridScaler.mode == 2)
            {
                onDistanceChanged?.Invoke(-1);
            }
            GridScaler.mode = 1;
            scaleText.text = "Grid Cell Scale: 10 sm";
        }

        else if (GridScaler.mode != 0 && transform.position.z > distanceSM_DM)
        {
            onDistanceChanged?.Invoke(-1);
            GridScaler.mode = 0;
            scaleText.text = "Grid Cell Scale: 1 sm";
        }

        else if (GridScaler.mode != 2 && transform.position.z < distanceDM_M)
        {
            onDistanceChanged?.Invoke(1);
            GridScaler.mode = 2;
            scaleText.text = "Grid Cell Scale: 1 m";
        }
    }
}
