using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void btn_change_scene(string scene_name)
    {
<<<<<<< Updated upstream
        foreach (GameObject gameObj in StaticClass.plan.planGameObjects)
        {
            PlanObject planObject = gameObj.GetComponent<PlanObject>();
            if (planObject is PlanObjectSimpWall)
            {
                PlanObjectSimpWall planObjectWall = planObject as PlanObjectSimpWall;
                //ConvertObjectTo3D.wallVerticesList.Add(planObject.GetVertices());
                if (planObjectWall.windowsList != null)
                {
                    WindowHoleMaker.DivideWall(planObjectWall);
                }
                else
                {
                    ConvertObjectTo3D.wallVerticesList.Add(planObjectWall.GetVertices());
                }
            }
        }

=======
>>>>>>> Stashed changes
        ConvertObjectTo3D.GetPlanObjects();
        SceneManager.LoadScene(scene_name);
        
    }



    
}
