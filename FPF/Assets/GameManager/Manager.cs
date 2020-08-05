using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public SaveFormat   data;
    public string   filePath;

    private void Start() {
        filePath = Application.persistentDataPath + "\\save-Default";
        data.worldName = "Default";
        data.difficulty = "Normal";
        data.time = new SaveFormat_Time();
        data.weather = new SaveFormat_Weather();
    }

    public void NewGame(string gameName)
    {
        filePath = Application.persistentDataPath + "\\save-" +  Random.Range(1, 1000000).ToString("D6") + '-' + gameName;
        Debug.Log(filePath);
        data.worldName = gameName;
        data.difficulty = "Normal";
        GetComponent<SaveSystem>().Save();
    }

    public void LoadGame(SaveFormat sf, string path)
    {
        filePath = path;
        data = sf;
        Debug.Log(filePath);
    }
}
