using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using Random = UnityEngine.Random;
using TMPro; 
using Unity.VisualScripting;

public class TestSpawner : MonoBehaviour
{
    public SaveSystem saveSystem;
    public GameObject prefab;
    public GameObject prefab2;
    //public GameObject prefab3;

    //public CopyPasteSystem copyPasteSystem; // Reference to CopyPasteSystem

    public List<GameObject> createdPrefabs = new List<GameObject>();
    public List<GameObject> createdPrefabs2 = new List<GameObject>();
    //public List<GameObject> createdPrefabs3 = new List<GameObject>();
    public void Clear()
    {
        foreach (var item in createdPrefabs)
        {
            Destroy(item);
        }
        //createdPrefabs.Clear();

        foreach (var item in createdPrefabs2)
        {
            Destroy(item);
        }
        createdPrefabs2.Clear();
    }

    public void SpawnPrefab()
    {
        createdPrefabs.Add(Instantiate(prefab, transform.position, Quaternion.identity));
    }
    public void SpawnPrefab2()
    {
        createdPrefabs2.Add(Instantiate(prefab2, transform.position, Quaternion.identity));
    }
    public void SaveGame(int slotNumber)
    {
        SaveData data = new SaveData();

        foreach (var item in createdPrefabs)
        {
            data.Add(item, null);
        }
        foreach (var item in createdPrefabs2)
        {
            data.Add(item, null); // No text for prefab2
        }

        var dataToSave = JsonUtility.ToJson(data);
        saveSystem.SaveData(dataToSave, slotNumber);
    }

    public void LoadGame(int slotNumber)
    {
        Clear();
        string dataToLoad = saveSystem.LoadData(slotNumber);
        if (!string.IsNullOrEmpty(dataToLoad))
        {
            SaveData data = JsonUtility.FromJson<SaveData>(dataToLoad);
            foreach (var objData in data.objectData)
            {
                GameObject newObj = null;
                if (objData.type.StartsWith("Cube"))
                    newObj = Instantiate(prefab, objData.position.GetValue(), Quaternion.identity);
                if (objData.type.StartsWith("Sphere"))
                    newObj = Instantiate(prefab2, objData.position.GetValue(), Quaternion.identity);
            }
        }
    }

    [Serializable]
    public class SaveData
    {
        public List<ObjectSerialization> objectData;

        public SaveData()
        {
            objectData = new List<ObjectSerialization>();
        }
        public void Add(GameObject obj, string text)
        {
            objectData.Add(new ObjectSerialization(obj.name, obj.transform.position, text));
        }
    }

    [Serializable]
    public class ObjectSerialization
    {
        public Vector3Serialization position;
        public string type;
        public string text; // Add text field for prefab3

        public ObjectSerialization(string t, Vector3 pos, string textData)
        {
            this.type = t;
            this.position = new Vector3Serialization(pos);
            this.text = textData; // Store text
        }
    }

    [Serializable]
    public class Vector3Serialization
    {
        public float x, y, z;

        public Vector3Serialization(Vector3 position)
        {
            this.x = position.x;
            this.y = position.y;
            this.z = position.z;
        }
        public Vector3 GetValue()
        {
            return new Vector3(x, y, z);
        }
    }
}