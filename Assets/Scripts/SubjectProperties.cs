using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubjectProperties : MonoBehaviour
{

   public float Size = 0;

   void Update(){
       Vector3 VectorSize = new Vector3(0.3f + Size,0.3f + Size,0.3f + Size);
       gameObject.transform.localScale = VectorSize;
   }

}
