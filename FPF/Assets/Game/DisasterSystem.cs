using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class DisasterSystem : MonoBehaviour
{
    [SerializeField] private Manager manager;
    [SerializeField] private GlobalTime gt;
    [SerializeField] private SeasonSystem ss;
    [SerializeField] private DpSManager DpS;
    [SerializeField] private TextMeshProUGUI disasterText;
    [SerializeField] private int rate;
    [SerializeField] private string[] disasters;

    void Start()
    {
        if (!GameObject.Find("GameManager"))
        {
            Destroy(this);
            return;
        }
        manager = GameObject.Find("GameManager").GetComponent<Manager>();
        disasterText.text = "";
    }

    void Update()
    {
        if (manager.data.weather.disaster.timerLeft <= 0)
        {
            disasterText.text = "";
            if (Random.Range(0, 101) <= rate && manager.data.time.dayInSeason < gt.daysBySeason - 1 && manager.data.time.timer % 1440 <= 1)
                LaunchNewDisaster();
        }
        else if (manager.data.weather.disaster.timerLeft >= 0)
        {
            if (gt.hour >= 6 && gt.hour <= 20)
                manager.data.weather.disaster.timerLeft -= Time.deltaTime * gt.daySpeed;
            else
                manager.data.weather.disaster.timerLeft -= Time.deltaTime * gt.nightSpeed;
            if (manager.data.weather.disaster.timerLeft < 0)
                manager.data.weather.disaster.timerLeft = 0;


            if (manager.data.weather.disaster.id == 0)
                Fire();
            else if (manager.data.weather.disaster.id == 1)
                Inondation();
            else if (manager.data.weather.disaster.id == 2)
                WindTempest();
            else
                Blizzard();

            disasterText.text = disasters[manager.data.weather.disaster.id] + " " + (int)manager.data.weather.disaster.timerLeft / 60 + ":" + (int)manager.data.weather.disaster.timerLeft % 60;
        }

    }

    private void Blizzard()
    {
        if (manager.data.weather.disaster.timerLeft % 60 == 0)
            --manager.data.weather.degree;
        manager.data.weather.snowing = true;
        manager.data.weather.raining = false;
        manager.data.weather.wind = 70;
        ss.UpdateHUD();
    }

    private void Fire()
    {
        var toRemove = DpS.DpS.Where(pair => Random.Range(0, 101) <= 20).ToList();
        foreach (var v in toRemove)
        {
            Destroy(GameObject.Find(v.Key.ToString("D6")));
            DpS.DpS.Remove(v.Key);
        }
    }

    private void WindTempest()
    {
        manager.data.weather.wind = 180;
        ss.UpdateHUD();
    }

    private void Inondation()
    {
        manager.data.weather.snowing = false;
        manager.data.weather.raining = true;
        manager.data.weather.humidity = 100;
        ss.UpdateHUD();
        foreach (var dp in DpS.DpS.ToList())
        {
            if (dp.Value.vegetebalId != -1)
            {
//                Seed s = GameObject.Find(dp.Key.ToString("D6")).GetComponentInChildren<Seed>().transform.GetChild(0).gameObject.GetComponent<Seed>();
  //              if (s.actualHumidity >= 100)
    //                Destroy(GameObject.Find(dp.Key.ToString("D6")));
            }
        }
    }

    private void LaunchNewDisaster()
    {
        manager.data.weather.disaster.timerLeft = Random.Range(60, 1441);
        if (manager.data.time.season >= 0 && manager.data.time.season < 3)
            manager.data.weather.disaster.id = Random.Range(1, 4);
        else
            manager.data.weather.disaster.id = Random.Range(0, 3);
    }


}
