using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class PlanObjectAnchor: PlanObject
{
    
    Vector3 wallDirection = Vector3.zero;
    Vector3 scale;
    PlanObjectSimpWall wallObject;
    private GameObject GetPrefab(int id)
    {
        Debug.Log("GetPrefab id = " + id);
        return GameObject.Find("Plane").GetComponent<Plane>().GetPrefabByIndex(id);
    }

    public void Start()
    {
        
    }

    public override void OnMouseDown()
    {
        if (UIController.objectTypeMode == 1 && !EventSystem.current.IsPointerOverGameObject()) //wall 
        {
            
            Debug.Log("Create Wall on Anchor");
            wallObject = Instantiate(GetPrefab(2), GameObject.Find("Plane").transform, true).GetComponent<PlanObjectSimpWall>();
            wallObject.transform.Translate(this.transform.position + Vector3.forward * 0.0001f);
            wallObject.CreatePlanObject(this.scale);
            //ObjectsDataRepository.planObjectsDataList.Add(new);
        }
        
    }



    public override void OnMouseDrag()
    {
        
        var newMousePosition = UIController.GetUnscaledObjectPosition(-0.0003f);
        if (wallDirection == Vector3.zero && wallObject!=null)
        {
            if (newMousePosition.x > maxBounds.x - 0.009f)
            {
                Debug.Log("Right");
                wallDirection = Vector3.right;
                wallObject.minSpawnBounds.x = this.maxBounds.x;
                wallObject.maxSpawnBounds.x = this.minBounds.x;
                wallObject.UpdateWall(newMousePosition, wallDirection);
            }

            else if (newMousePosition.x < minBounds.x)
            {
                Debug.Log("Left");
                wallDirection = Vector3.left;
                wallObject.minSpawnBounds.x = this.maxBounds.x;
                wallObject.maxSpawnBounds.x = this.minBounds.x;
                wallObject.UpdateWall(newMousePosition, wallDirection);
            }
           
            else if (newMousePosition.y > maxBounds.y - 0.009f)
            {
                Debug.Log("Up");
                wallDirection = Vector3.up;
                wallObject.minSpawnBounds.y = this.maxBounds.y;
                wallObject.maxSpawnBounds.y = this.minBounds.y;
                wallObject.UpdateWall(newMousePosition, wallDirection);
            }

            else if (newMousePosition.y < minBounds.y)
            {
                Debug.Log("Down");
                wallDirection = Vector3.down;
                wallObject.minSpawnBounds.y = this.maxBounds.y;
                wallObject.maxSpawnBounds.y = this.minBounds.y;
                wallObject.UpdateWall(newMousePosition, wallDirection);
            }
        }

        else if (wallDirection != Vector3.zero)
        {
            
            
            if (newMousePosition.x < maxBounds.x && newMousePosition.x + 0.01f > minBounds.x &&
                 newMousePosition.y < maxBounds.y && newMousePosition.y + 0.01f > minBounds.y)
            {
                
                wallObject.UpdateWall(newMousePosition, wallDirection);
                wallDirection = Vector3.zero;
                wallObject.maxSpawnBounds = Vector2.zero;
                wallObject.minSpawnBounds = Vector2.zero;
            }

            else
            {
                wallObject.UpdateWall(newMousePosition, wallDirection);
            }
        }
    }

    public void OnMouseUp()
    {
        if (wallDirection == Vector3.zero && wallObject != null)
        {
            wallObject.DestroyThisObject();
        }
        RaycastHit hit; 
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit);
        if (hit.transform.name != "Anchor")
        {
            Vector3 position = Vector3.zero;
            if (wallDirection == Vector3.right)
            {
                position = new Vector3(wallObject.maxSpawnBounds.x, wallObject.minBounds.y, -0.0003f);
               
            }

            else if (wallDirection == Vector3.left)
            {
                position = new Vector3(wallObject.minBounds.x, wallObject.minBounds.y, -0.0003f);

            }

            else if (wallDirection == Vector3.up)
            {
                position = new Vector3(wallObject.minBounds.x, wallObject.maxSpawnBounds.y, -0.0003f);

            }

            else if (wallDirection == Vector3.down)
            {
                position = new Vector3(wallObject.minBounds.x, wallObject.minBounds.y, -0.0003f);

            }
            Destroy(wallObject.GetComponent<BoxCollider>());
            wallObject.gameObject.AddComponent<BoxCollider>();
            var anchorObject = Instantiate(GetPrefab(1), GameObject.Find("Plane").transform, true).GetComponent<PlanObjectAnchor>();
            anchorObject.transform.Translate(position);
            anchorObject.CreatePlanObject();
        }
        wallDirection = Vector3.zero;
    }

    public override void AddAdditionalValues()
    {
        this.name = "Anchor";
        this.scale = ObjectsParams.scale;
        ObjectsDataRepository.currentSaveFile.planObjectsDataList.Add(new AnchorObjectData(this.meshFilter.mesh, this.transform.position, this.scale, this.id));
    }

    public override void ReAddValues(PlanObjectData planObjData)
    {
        var planObjAnchorData = planObjData as AnchorObjectData;
        this.scale = planObjAnchorData.scale;
        //Debug.Log("Anchor scale: " + this.scale);
        

    }
}
