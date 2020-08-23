using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnimalHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerTxt;
    [SerializeField] private Scrollbar foodBar;
    [SerializeField] private TextMeshProUGUI foodTxt;

    public void UpdateTimer(float timer)
    {
        int hour = (int)timer / 60;
        int minute = (int)timer % 60;
        if (timer <= 0)
        {
            timerTxt.text = "Adult";
        } else
            timerTxt.text = hour.ToString("D2") + "h" + minute.ToString("D2");
    }

    public void UpdateFood(float food)
    {
        foodBar.value = (int)food / 100f;
        foodTxt.text = ((int)food) + "%";
    }
}
