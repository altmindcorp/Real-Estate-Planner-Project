using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public abstract class PlanObject : MonoBehaviour, ISpawner
{
    public bool isCreatedFromSave;
    public int id;
    public Vector2 maxBounds;
    public Vector2 minBounds;
    public MeshRenderer meshRenderer;
    public MeshFilter meshFilter;
    public MeshCollider meshCollider;

    public abstract void AddAdditionalValues();
    public abstract void ReAddValues(PlanObjectData planObjData);
    public abstract void OnMouseDrag();
    public abstract void OnMouseDown();
    public void CreatePlanObject()
    {
        this.id = ObjectsDataRepository.currentID;
        this.meshFilter.mesh = MeshCreator.Create2DMesh(ObjectsParams.scale, 0);
        RecalculateWorldBounds();
        //Destroy(this.gameObject.GetComponent<MeshCollider>());
        //this.gameObject.AddComponent<BoxCollider>();
        this.meshCollider.sharedMesh = this.meshFilter.mesh;
        AddAdditionalValues();
        Plane.PlanObjectsList.Add(this);
        
        ObjectsDataRepository.currentID++;
    }

    public void CreatePlanObject(Vector3 scale)
    {

        this.id = ObjectsDataRepository.currentID;
        this.meshFilter.mesh = MeshCreator.Create2DMesh(scale, 0);
        RecalculateWorldBounds();
        //this.meshCollider.sharedMesh = this.meshFilter.mesh;
        //Destroy(this.gameObject.GetComponent<MeshCollider>());
        this.gameObject.AddComponent<BoxCollider>();
        AddAdditionalValues();
        Plane.PlanObjectsList.Add(this);
        ObjectsDataRepository.currentID++;
        ObjectsDataRepository.removedPlanObjects.Clear();
    }

    public void RecalculateWorldBounds()
    {
        this.meshFilter.mesh.RecalculateBounds();
        maxBounds = meshFilter.mesh.bounds.max + transform.position;
        minBounds = meshFilter.mesh.bounds.min + transform.position;
        
        this.meshCollider.sharedMesh = null;
        this.meshCollider.sharedMesh = this.meshFilter.mesh;

    }

    public void RecreatePlanObject(PlanObjectData planObjectData)
    {
        /*this.id = planObjectData.id;
        this.meshFilter.mesh = planObjectData.GetMesh();
        RecalculateWorldBounds();
        this.meshCollider.sharedMesh = this.meshFilter.mesh;*/


        ReAddValues(planObjectData);
        this.id = planObjectData.id;
        this.meshFilter.mesh = planObjectData.GetMesh();
        
        //this.gameObject.AddComponent<BoxCollider>();
        RecalculateWorldBounds();
        //this.meshCollider.sharedMesh = this.meshFilter.mesh;
        Plane.PlanObjectsList.Add(this);
    }

    public void DestroyThisObject()
    {
        ObjectsDataRepository.removedPlanObjects.Add(ObjectsDataRepository.currentSaveFile.planObjectsDataList.Find(x => x.id == this.id));
        ObjectsDataRepository.currentSaveFile.planObjectsDataList.RemoveAll(x => x.id == this.id);
        Destroy(this.gameObject);
    }

    public void OnDestroy()
    {
        //ObjectsDataRepository.RemoveObject(this.id);
    }
}
