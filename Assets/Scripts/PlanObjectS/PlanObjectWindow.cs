using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlanObjectWindow : PlanObjectWallChild
{
    public float positionHeight;

    public override void AddAdditionalValues()
    {
        Debug.Log("Window id in window: " + this.id);
        ObjectsDataRepository.currentSaveFile.planObjectsDataList.Add(new WindowObjectData(this.meshFilter.mesh, this.transform.position, this.orientation, this.height, this.positionHeight, this.length, this.id, this.wallID));
    }

    public override void OnMouseDown()
    {
        //throw new System.NotImplementedException();
    }

    public override void OnMouseDrag()
    {
        //throw new System.NotImplementedException();
    }

    public override void ReAddValues(PlanObjectData planObjData)
    {
        var windowData = planObjData as WindowObjectData;
        this.orientation = windowData.orientation;
        this.positionHeight = windowData.positionHeight;
        this.height = windowData.height;
        this.length = windowData.length;
        this.wallID = windowData.wallID;
    }
}
