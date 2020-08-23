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
        data.dpSDictionaryLocation = Application.persistentDataPath + "\\save-Default-dpS";
        data.farmDictionaryLocation = Application.persistentDataPath + "\\save-Default-farm";
    }

    public void NewGame(string gameName)
    {
        filePath = Application.persistentDataPath + "\\save-" +  Random.Range(1, 1000000).ToString("D6") + '-' + gameName;
        data.dpSDictionaryLocation = Application.persistentDataPath + "\\save-" + Random.Range(1, 1000000).ToString("D6") + "-" + gameName + "-dpS";
        data.farmDictionaryLocation = Application.persistentDataPath + "\\save-" + Random.Range(1, 1000000).ToString("D6") + "-" + gameName + "-farm";
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
