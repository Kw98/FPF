using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonSystem : MonoBehaviour
{
    private Season[] seasons;
    private Manager m;
    [SerializeField] private GlobalTime gt;
    [SerializeField] private WeatherHUD wHUD;
    [SerializeField] private GameObject rain;
    [SerializeField] private GameObject snow;
    private bool weatherUpdated;
    private int weatherDiffDeg;
    // Start is called before the first frame update
    void Start()
    {
        if (!GameObject.Find("GameManager") || !gt)
        {
            Destroy(this);
            return;
        }
        m = GameObject.Find("GameManager").GetComponent<Manager>();
        seasons = new Season[4];
        seasons[0] = new Season(20, 30, 30, 1);
        seasons[1] = new Season(30, 10, 10, 1);
        seasons[2] = new Season(15, 50, 45, 1);
        seasons[3] = new Season(5, 70, 60, 1);
        UpdateWeather();
        weatherUpdated = true;
        weatherDiffDeg = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (gt.hour == 6 && weatherUpdated == false) {
            UpdateWeather();
            weatherUpdated = true;
        } else if (gt.hour != 6 && weatherUpdated == true)
            weatherUpdated = false;
    }

    void UpdateWeather()
    {
        Season season = seasons[m.data.time.season];
        int degree = season.averageDegree;
        if (m.data.time.dayInSeason <= 5 || m.data.time.dayInSeason >= 10)
        {
            int day = m.data.time.dayInSeason % 5;

            if (m.data.time.dayInSeason <= 5)
            {
                day = 5 + m.data.time.dayInSeason;
                int preSeason = m.data.time.season - 1;
                if (preSeason == -1)
                    preSeason = 3;
                degree = seasons[preSeason].averageDegree;
            }


            int diffDeg = ((day) + 1) * season.degreeNext;
            if ((m.data.time.season == 1 && m.data.time.dayInSeason >= 10) || m.data.time.season == 2 || (m.data.time.season == 3 && m.data.time.dayInSeason <= 5))
                diffDeg *= -1;
            degree += diffDeg;
        }
        m.data.weather.degree = Random.Range(degree - 2, degree + 2);
        m.data.weather.raining = CheckRaining(season);
        m.data.weather.wind = Random.Range(-500, 180);
        if (m.data.weather.wind < 0)
            m.data.weather.wind = 0;
        if (m.data.time.season == 3)
        {
            m.data.weather.snowing = m.data.weather.raining;
            m.data.weather.raining = false;
        }
        Debug.Log(m.data.weather.raining);
        rain.SetActive(m.data.weather.raining);
        snow.SetActive(m.data.weather.snowing);
        wHUD.UpdateWeatherHUD();
    }

    bool CheckRaining(Season season)
    {
        m.data.weather.humidity = Random.Range(season.averageHumidity - 10, season.averageHumidity + 10);
        if (Random.Range(0, 100) <= season.rainChance) {
            m.data.weather.humidity = 100;
            return true;
        }
        return false;
    }
}
