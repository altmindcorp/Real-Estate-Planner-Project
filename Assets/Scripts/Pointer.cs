using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    private RaycastHit hit;
    public Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition); 
            Debug.Log("Coords: " + ray.origin);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == "Finish")
                {
                    
                    Debug.Log("Coords:" + (int)hit.point.x + " " + (int)hit.point.y);
                }
            }
            
            /**/
        }
    }
}
