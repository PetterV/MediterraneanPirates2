using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

[System.Serializable]
public static class SaveController
{
    public static string fileSlot = "SaveSlot1";

    public static void SetFileSlot(int slot)
    {
        fileSlot = "SaveSlot" + slot.ToString();
        Debug.Log("Save slot set to slot " + slot.ToString());
    }
    
    static void SaveGameController()
    {
        string directoryPath = Application.persistentDataPath + "/" + fileSlot;
        Debug.Log("Saving Game Controller to directory " + directoryPath);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(directoryPath + "/GameController.save");
        bf.Serialize(file, GameObject.Find("GameController").GetComponent<GameController>().data);
        file.Close();
    }
    static void LoadGameController()
    {
        string directoryPath = Application.persistentDataPath + "/" + fileSlot;
        if (File.Exists(directoryPath + "/GameController.save"))
        {
            GCData loadedController = new GCData();
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(directoryPath + "/GameController.save", FileMode.Open);
            loadedController = (GCData)bf.Deserialize(file);
            file.Close();
            GameObject.Find("GameController").GetComponent<GameController>().LoadGameController(loadedController);
        }
        else
        {
            Debug.Log("No save file found for slot!");
        }
    }

    public static void SaveAll()
    {
        SaveGameController();
        Debug.Log("Saved data to slot " + fileSlot);
    }
    public static void LoadAll()
    {
        LoadGameController();
        Debug.Log("Loaded data from slot " + fileSlot);
    }
}

