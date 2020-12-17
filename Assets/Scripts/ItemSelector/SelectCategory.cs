using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCategory : MonoBehaviour
{
public Catygories[] category;

    [System.Serializable]
    public struct Catygories{
public List<Sprite> SpriteOfItem;
public List<GameObject> PrefabOfItem;

public Catygories(List<Sprite> spriteOfItem, List<GameObject> prefabOfItem){
SpriteOfItem = spriteOfItem;
PrefabOfItem = prefabOfItem;

}


    }
}
