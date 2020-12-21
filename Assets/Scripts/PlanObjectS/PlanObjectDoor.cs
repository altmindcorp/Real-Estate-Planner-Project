using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlanObjectDoor : PlanObjectWallChild
{
    public float topHeight;

    public override void AddAdditionalValues()
    {
        //throw new System.NotImplementedException();
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
        ObjectsDataRepository.planObjectsDataList.RemoveAll(x => x.id == this.id);
    }


}
