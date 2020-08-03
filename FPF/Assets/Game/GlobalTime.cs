using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Temps de jeu total sur la partie
public class GlobalTime : MonoBehaviour
{
    private Manager manager;
    public int hour;
    public int minute;
    [SerializeField] public int daysBySeason;
    [SerializeField] private float daySpeed;
    [SerializeField] private float nightSpeed;
    [SerializeField] private float miniIntensity;
    [SerializeField] private float maxIntensity;
    // Start is called before the first frame update
    // 0.0416666667
    void Start()
    {
        if (!GameObject.Find("GameManager")) {
            Destroy(this);
            return;
        }
        manager = GameObject.Find("GameManager").GetComponent<Manager>();
        hour = (int)(manager.data.time.timer % 1440) / 60;
        minute = (int)(manager.data.time.timer % 60);
    }

    // Update is called once per frame
    // 1440min = 24h * 60min
    void Update()
    {
        hour = (int)(manager.data.time.timer % 1440) / 60;
        minute = (int)(manager.data.time.timer % 60);

        //if (hour >= 5 && hour < 10)
        //{
        //    if (manager.data.time.lightIntensity < maxIntensity)
        //        manager.data.time.lightIntensity += 0.0000009f * daySpeed;
        //}
        //else if (hour == 12)
        //    manager.data.time.lightIntensity = maxIntensity;
        //else if (hour > 19 && hour <= 23)
        //{
        //    if (manager.data.time.lightIntensity > miniIntensity)
        //        manager.data.time.lightIntensity -= 0.000002f * nightSpeed;
        //}
        //else if (hour == 0)
        //    manager.data.time.lightIntensity = miniIntensity;
        if (hour >= 6 && hour <= 20)
            manager.data.time.timer += Time.deltaTime * daySpeed;
        else
            manager.data.time.timer += Time.deltaTime * nightSpeed;
        int newHour = (int)(manager.data.time.timer % 1440) / 60;
        if (hour != newHour)
        {
            Debug.Log("diff");
            if (newHour > 3 && newHour < 12)
            {
                if (manager.data.time.lightIntensity < maxIntensity)
                    manager.data.time.lightIntensity += 0.09f;
            } else if (newHour > 16 || newHour == 0)
            {
                if (manager.data.time.lightIntensity > miniIntensity)
                    manager.data.time.lightIntensity -= 0.09f;
            }
        }

        if (hour == 23 && (int)(manager.data.time.timer % 1440) / 60 == 0)
        {
            manager.data.time.dayInSeason += 1;
            if (manager.data.time.dayInSeason > daysBySeason)
            {
                manager.data.time.dayInSeason = 1;
                manager.data.time.season = (manager.data.time.season + 1) % 4;
            }
        }
    }
}
