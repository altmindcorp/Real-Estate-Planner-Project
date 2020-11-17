using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    private int id;

    public int GetID()
    {
        return id;
    }
    public void SetID(int id)
    {
        this.id = id;
    }
}
