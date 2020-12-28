using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using System.Linq;

public static class ObjectsDataRepository
{

    public static float currentFloorArea = 0;
    public static int currentID;
    public static SaveFile currentSaveFile = new SaveFile();
    public static List<string> saveFilesNames = new List<string>();

    public static Vector3 spawnPointPosition = Vector3.zero;
    public static int currentID = 0;
    public static List<PlanObjectData> planObjectsDataList = new List<PlanObjectData>();
    public static GameObject PlayerContainer;


    public static bool LoadSaveFile(string name)
    {
        //load save from file
        if (File.Exists(Application.persistentDataPath + "/" + name + ".save"))
        {
            SurrogateSelector surrogateSelector = new SurrogateSelector();
            //currentSaveFile.spawnPosition = Vector3.one * 5;
            Vector3SerializationSurrogate vector3SS = new Vector3SerializationSurrogate();
            Vector2SerializationSurrogate vector2SS = new Vector2SerializationSurrogate();

            surrogateSelector.AddSurrogate(typeof(Vector3), new StreamingContext(StreamingContextStates.All), vector3SS);
            surrogateSelector.AddSurrogate(typeof(Vector2), new StreamingContext(StreamingContextStates.All), vector2SS);
            BinaryFormatter bf = new BinaryFormatter();
            bf.SurrogateSelector = surrogateSelector;
            
            FileStream file = File.Open(Application.persistentDataPath + "/" + name + ".save", FileMode.Open);
            currentSaveFile = (SaveFile)bf.Deserialize(file);
            Debug.Log(currentSaveFile.name);
            file.Close();
            return true;
        }

        else
        {
            return false;
        }
    }

    public static void SaveCurrentFile(string name)
    {
        SurrogateSelector surrogateSelector = new SurrogateSelector();
        //currentSaveFile.spawnPosition = Vector3.one * 5;
        Vector3SerializationSurrogate vector3SS = new Vector3SerializationSurrogate();
        Vector2SerializationSurrogate vector2SS = new Vector2SerializationSurrogate();

        surrogateSelector.AddSurrogate(typeof(Vector3), new StreamingContext(StreamingContextStates.All), vector3SS);
        surrogateSelector.AddSurrogate(typeof(Vector2), new StreamingContext(StreamingContextStates.All), vector2SS);

        currentSaveFile.name = name;
        BinaryFormatter bf = new BinaryFormatter();

        bf.SurrogateSelector = surrogateSelector;

        FileStream file = File.Create(Application.persistentDataPath + "/" + currentSaveFile.name + ".save");
        bf.Serialize(file, currentSaveFile);
        file.Close();
        Debug.Log("Scene Saved");
    }

    public static void RemoveObject(int id)
    {
        //currentSaveFile.planObjectsDataList.RemoveAll(x => x.id == id);
    }


    static void Update()
    {
        PlayerContainer.transform.position = new Vector3(spawnPointPosition.x, 1, spawnPointPosition.y);


        GameObject.CreatePrimitive(PrimitiveType.Cube);
    }
}
