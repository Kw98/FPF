using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeatherHUD : MonoBehaviour
{
    [SerializeField] private Image weather;
    [SerializeField] private TextMeshProUGUI degree;
    [SerializeField] private TextMeshProUGUI humidityWind;
    [SerializeField] private Sprite[] weatherImages;
    private Manager m;
    // Start is called before the first frame update
    void Start()
    {
        if (!GameObject.Find("GameManager"))
        {
            Destroy(this);
            return;
        }
        m = GameObject.Find("GameManager").GetComponent<Manager>();
        if (m)
            Debug.Log("not null");
    }

    public void UpdateWeatherHUD()
    {
        if (m.data.weather.raining == true)
            weather.sprite = weatherImages[0];
        else if (m.data.weather.snowing == true)
            weather.sprite = weatherImages[1];
        else if (m.data.weather.humidity >= 40)
            weather.sprite = weatherImages[2];
        else
            weather.sprite = weatherImages[3];
        degree.text = m.data.weather.degree + "c";
        humidityWind.text = "humidity: " + m.data.weather.humidity + "%\n" + "wind: " + m.data.weather.wind + "km/h";
    }
}
