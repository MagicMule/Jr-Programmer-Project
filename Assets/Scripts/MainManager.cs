using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;
using System.IO;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance { get; private set; }

    public Color TeamColor;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadColor();
    }
    //* data to be saved between sessions *//
    [System.Serializable]
    class SaveData
    {
        public Color TeamColor;
    }

    //* method to save data *//
    public void SaveColor()
    {
        SaveData data = new SaveData(); // New saved data 
        data.TeamColor = TeamColor; //  fill it TeamColor variable saved in MainManger(here)

        string json = JsonUtility.ToJson(data); // data -> JSON

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json); //Write string to a file
    }

    //* method to load data *//
    public void LoadColor()
    {
        string path = Application.persistentDataPath + "/savefile.json"; // Find saved file
        if (File.Exists(path)) // If .json file exsists
        {
            string json = File.ReadAllText(path); // read the files content
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            TeamColor = data.TeamColor; // Set the TeamColor to the color saved in SaveData
        }
    }
}
