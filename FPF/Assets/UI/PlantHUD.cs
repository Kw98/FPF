using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlantHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerTxt;
    [SerializeField] private Scrollbar humidityBar;
    [SerializeField] private TextMeshProUGUI humidityTxt;

    public void UpdateTimer(float timer)
    {
        int hour = (int)timer / 60;
        int minute = (int)timer % 60;
        if (timer <= 0)
        {
            hour = 0;
            minute = 0;
        }
        timerTxt.text = hour.ToString("D2") + "h" + minute.ToString("D2");
    }

    public void UpdateHumidity(float humidity)
    {
        humidityBar.value = (int)humidity / 100f;
        humidityTxt.text = ((int)humidity) + "%";
    }
}
