using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GridScaler
{
    public static int mode = 0;
    public static float scaleValue = 0.01f;

    public static void ChangeMode(int mode)
    {
        if (mode == 0)
        {
            
            scaleValue = 0.01f;
        }

        else if (mode == 1)
        {
            
            scaleValue = 0.1f;
        }

        else if (mode == 2)
        {
            
            scaleValue = 1;
        }
    }
}
