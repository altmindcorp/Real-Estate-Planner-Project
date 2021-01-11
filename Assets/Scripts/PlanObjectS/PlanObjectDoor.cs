using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlanObjectDoor : PlanObjectWallChild
{
    public float topHeight;

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
        //ObjectsDataRepository.currentSaveFile.planObjectsDataList.RemoveAll(x => x.id == this.id);
    }

    public override void AddAdditionalValues()
    {
        
    }

    public override void ReAddValues(PlanObjectData planObjData)
    {
        var doorData = planObjData as DoorObjectData;
        this.orientation = doorData.orientation;
        this.height = doorData.height;
        this.length = doorData.length;
        this.wallID = doorData.wallID;
    }
}
