    using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
   
    public void btn_switch_scene(string scene_name)
    {
        //ConvertObjectTo3D.GetPlanObjects();
        SceneManager.LoadScene(scene_name);
        
    }

    


    
}
                                                                                                                      