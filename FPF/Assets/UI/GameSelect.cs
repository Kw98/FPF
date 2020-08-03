using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSelect : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI    name;
    [SerializeField] private TextMeshProUGUI    timePlayed;
    [SerializeField] private TextMeshProUGUI    difficulty;

    private SaveFormat  data;
    private string  filePath;

    public void    Init(SaveFormat saveFormat, string path)
    {
        data = saveFormat;
        filePath = path;
        name.text = data.worldName;
        int seconds = (int)(data.time.timer) % 60;
        int minutes = (int)(data.time.timer) / 60;
        int hours = minutes / 60;
        timePlayed.text = hours.ToString("D2") + ':' + (minutes % 60).ToString("D2") + ':' + (seconds % 60).ToString("D2");
        difficulty.text = data.difficulty;
    }

    public void    OnClick_delete()
    {
        Destroy(gameObject);
        if (File.Exists(filePath))
            File.Delete(filePath);
    }

    public void OnClick_play()
    {
        GameObject.Find("GameManager").GetComponent<Manager>().LoadGame(data, filePath);
        SceneManager.LoadScene("Map_begin");
    }
}
