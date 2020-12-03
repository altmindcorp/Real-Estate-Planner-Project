using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class WindowHoleMaker
{
    public static PlanObjectWindow[] windowsArray;

    /*public static void DivideWall(PlanObjectSimpWall planObjWall)
    {
        Vector3[] dividedWallVertices;
        Vector3[] windowVertices = new Vector3[4];
        windowsArray = planObjWall.planObjectObjectsList.ToArray();

        Vector3 direction = planObjWall.direction;
        Vector3[] wallVertices = planObjWall.vertices;
        if (direction == Vector3.right || direction == Vector3.left)
        {
            var sortedWindows = from obj in windowsArray
                                orderby obj.GetVertices()[0].x
                                select obj;

            windowVertices[2] = wallVertices[1];
            windowVertices[3] = wallVertices[0];

            for (int i = 0; i < windowsArray.Length; i++)
            {
                dividedWallVertices = new Vector3[4];
                dividedWallVertices[1] = windowVertices[2];
                dividedWallVertices[0] = windowVertices[3];

                windowVertices = windowsArray[i].vertices;

                dividedWallVertices[2] = windowVertices[1];
                dividedWallVertices[3] = windowVertices[0];

                Debug.Log("Wall Vertices: " + dividedWallVertices[0] + "\n" + dividedWallVertices[1] + "\n" + dividedWallVertices[2] + "\n" + dividedWallVertices[3]);
                //ConvertObjectTo3D.planObjectSimpWallList.Add();
                //ConvertObjectTo3D.wallVerticesList.Add(dividedWallVertices);
                //ConvertObjectTo3D.windowVerticesList.Add(windowVertices);
            }
            dividedWallVertices = new Vector3[4];
            dividedWallVertices[0] = windowVertices[3];
            dividedWallVertices[1] = windowVertices[2];
            dividedWallVertices[2] = wallVertices[2];
            dividedWallVertices[3] = wallVertices[3];

            //ConvertObjectTo3D.wallVerticesList.Add(dividedWallVertices);
        }

        else if (direction == Vector3.up || direction == Vector3.down)
        {
            wallVertices1[0] = wallVertices[0];
            wallVertices1[1] = windowVertices[0];
            wallVertices1[2] = windowVertices[3];
            wallVertices1[3] = wallVertices[3];

            wallVertices2[0] = windowVertices[1];
            wallVertices2[1] = wallVertices[1];
            wallVertices2[2] = wallVertices[2];
            wallVertices2[3] = windowVertices[2];
        }
    }*/


    
}
