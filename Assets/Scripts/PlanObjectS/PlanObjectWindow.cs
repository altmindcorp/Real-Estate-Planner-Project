using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlanObjectWindow : PlanObjectWallChild
{
    public float positionHeight;

    public override void AddAdditionalValues()
    {

        //ObjectsDataRepository.planObjectsDataList.Add(new WindowObjectData(this.meshFilter.mesh, this.orientation, this.position, this.height, this.positionHeight, this.id));
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
}
