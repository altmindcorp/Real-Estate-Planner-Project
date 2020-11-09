using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.mouseScrollDelta.y>0)
        {
            transform.Translate(Vector3.forward * 0.2f);
        }

        if (Input.mouseScrollDelta.y<0)
        {
            transform.Translate(Vector3.back * 0.2f);
        }
    }
}
