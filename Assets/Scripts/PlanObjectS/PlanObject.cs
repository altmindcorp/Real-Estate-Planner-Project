using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public abstract class PlanObject : MonoBehaviour, ISpawner
{
    public int id;
    public Vector2 maxBounds;
    public Vector2 minBounds;
    public MeshRenderer meshRenderer;
    public MeshFilter meshFilter;
    public MeshCollider meshCollider;

    public abstract void AddAdditionalValues();
    public abstract void OnMouseDrag();
    public abstract void OnMouseDown();
    public void CreatePlanObject()
    {
        this.id = ObjectsDataRepository.currentID;
        Debug.Log("ID: " + this.id);
        Debug.Log("Mesh Scale: " + ObjectsParams.scale);
        this.meshFilter.mesh = MeshCreator.Create2DMesh(ObjectsParams.scale, 0);
        RecalculateWorldBounds();
        this.meshCollider.sharedMesh = this.meshFilter.mesh;
        AddAdditionalValues();
        Plane.PlanObjectsList.Add(this);
        ObjectsDataRepository.currentID++;
    }

    public void CreatePlanObject(Vector3 scale)
    {
        this.id = ObjectsDataRepository.currentID;
        Debug.Log("Mesh Scale: " + scale);
        this.meshFilter.mesh = MeshCreator.Create2DMesh(scale, 0);
        Debug.Log("ID: " + this.id);
        RecalculateWorldBounds();
        this.meshCollider.sharedMesh = this.meshFilter.mesh;
        AddAdditionalValues();
        Plane.PlanObjectsList.Add(this);
        ObjectsDataRepository.currentID++;
    }

    public void RecalculateWorldBounds()
    {
        this.meshFilter.mesh.RecalculateBounds();
        maxBounds = meshFilter.mesh.bounds.max + transform.position;
        minBounds = meshFilter.mesh.bounds.min + transform.position;
        this.meshCollider.sharedMesh = null;
        this.meshCollider.sharedMesh = this.meshFilter.mesh;
    }

    public void DestroyThisObject()
    {
        Destroy(this.gameObject);
    }
}
