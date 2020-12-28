using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlanObjectWindow : PlanObjectWallChild
{
    public float positionHeight;

    public override void AddAdditionalValues()
    {
        ObjectsDataRepository.currentSaveFile.planObjectsDataList.Add(new WindowObjectData(this.meshFilter.mesh, this.transform.position, this.orientation, this.height, this.positionHeight, this.length, this.id));
    }

    public override void OnMouseDown()
    {
        //throw new System.NotImplementedException();
    }

    public override void OnMouseDrag()
    {
        //throw new System.NotImplementedException();
    }

    public void OnDestroy()
    {
        //ObjectsDataRepository.planObjectsDataList.RemoveAt(ObjectsDataRepository.planObjectsDataList.Count - 1);
    }

    public override void ReAddValues(PlanObjectData planObjData)
    {
        var windowData = planObjData as WindowObjectData;
        this.orientation = windowData.orientation;
        this.positionHeight = windowData.positionHeight;
        this.height = windowData.height;
        this.length = windowData.length;
        
    }
}
