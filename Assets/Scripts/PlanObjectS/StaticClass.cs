using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticClass 
{
    private static Plan plan;

    public static void SetCurrentPlan(Plan currentPlan)
    {
        plan = currentPlan;
    }

    public static void DebugInfo()
    {
        foreach (PlanObject planObject in plan.planObjects)
        {
            if (planObject is PlanObjectWindow)
            {
                PlanObjectWindow windowObject = planObject as PlanObjectWindow;
                Debug.Log("Window id: " + windowObject.id + ", bottom height: " + windowObject.GetBottomHeight());
            }

            if (planObject is PlanObjectSimpWall)
            {
                PlanObjectSimpWall simpWallObject = planObject as PlanObjectSimpWall;
                Debug.Log("Wall id: " + simpWallObject.id + ", height: " + simpWallObject.GetHeight());
            }

            if (planObject is PlanObjectDoor)
            {
                PlanObjectDoor doorObject = planObject as PlanObjectDoor;
                Debug.Log("Wall id: " + doorObject.id + ", height: " + doorObject.GettopHeight());
            }
        }
    }
}
