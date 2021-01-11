using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlanObjectWallChild : PlanObject 
{
    public float height;
    public float length;
    //public Vector3 position;
    public Vector3 orientation;
    public int wallID;

    new public void DestroyThisObject()
    {
        Destroy(this.gameObject);
        ObjectsDataRepository.removedPlanObjects.Add(ObjectsDataRepository.currentSaveFile.planObjectsDataList.Find(x => x.id == this.id));
        (ObjectsDataRepository.currentSaveFile.planObjectsDataList.Find(x => x.id == this.wallID) as WallObjectData).RemoveWallChildId(this.id);
    }

    new public void OnDestroy()
    {
        var wallData = ObjectsDataRepository.currentSaveFile.planObjectsDataList.Find(x => x.id == this.wallID) as WallObjectData;
        //wallData.wallChildsIdList.RemoveAll(x => x == this.id);
    }
}
