using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    [SerializeField] private float maxHumidityLost = 2f;
    [SerializeField] private float maxHumidityWin = 2f;
    [SerializeField] private SaveFormat_Spe spec;
    [SerializeField] private GlobalTime gt;
    [SerializeField] private Manager m;
    [SerializeField] private GameObject plant;
    [SerializeField] private GameObject fruit;
    public float timer = 0;
    public float totalTimer = 0;
    public int actualHumidity = 0;
    public int humidityLimit = 0;
    private float humidity = 0;
    public int degreeMini = 0;
    public int degreeMax = 0;
    public int windLimit = 0;
    public int resistance = 0;

    public void ImportData(SaveFormat_Seed data)
    {
        timer = data.timer;
        totalTimer = data.totalTimer;
        actualHumidity = data.actualHumidity;
        humidity = (float)actualHumidity;
        humidityLimit = data.humidityLimit;
        degreeMini = data.degreeMini;
        degreeMax = data.degreeMax;
        windLimit = data.windLimit;
        resistance = data.resistance;
    }

    public SaveFormat_Seed ExportData()
    {
        SaveFormat_Seed ss = new SaveFormat_Seed();
        ss.timer = timer;
        ss.totalTimer = totalTimer;
        ss.actualHumidity = actualHumidity;
        ss.humidityLimit = humidityLimit;
        ss.degreeMini = degreeMini;
        ss.degreeMax = degreeMax;
        ss.windLimit = windLimit;
        ss.resistance = resistance;
        return ss;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!GameObject.Find("GameManager"))
        {
            Destroy(this);
            return;
        }
        m = GameObject.Find("GameManager").GetComponent<Manager>();
        if (timer != 0)
            return;
        if (spec.actualLvl != 1)
            totalTimer -= ((totalTimer / 2f) * spec.actualLvl) / 100f;
        actualHumidity = 50;
        resistance = spec.actualLvl;
        humidity = (float)actualHumidity;
        humidityLimit -= (humidityLimit * spec.actualLvl) / 100;
        degreeMini -= spec.actualLvl / 10;
        degreeMax += spec.actualLvl / 10;
        windLimit += spec.actualLvl;
    }

    // Update is called once per frame
    void Update()
    {
        Humidity();
        if (timer < totalTimer)
        {
            if (checkDeath())
                Destroy(transform.parent.gameObject);
            if (gt.hour >= 6 && gt.hour <= 20)
                timer += Time.deltaTime * gt.daySpeed;
            else
                timer += Time.deltaTime * gt.nightSpeed;
            float growth = timer / totalTimer;
            plant.transform.localScale = new Vector3(growth, growth, growth);
        } else
        {
            if (checkDeath() || m.data.weather.wind >= windLimit)
                Destroy(transform.parent.gameObject);
        }
    }

    private bool checkDeath()
    {
        if (m.data.weather.degree <= degreeMini || m.data.weather.degree >= degreeMax)
            return true;
        if (actualHumidity <= humidityLimit)
            return true;
        return false;
    }

    private void Humidity()
    {
        if (m.data.weather.humidity < 50 && humidity > 0)
        {
            humidity -= (maxHumidityLost * (m.data.weather.humidity / 50f));
            actualHumidity = Mathf.RoundToInt(humidity);
        } else if (m.data.weather.humidity > 50 && humidity < 100)
        {
            humidity += maxHumidityWin * ((m.data.weather.humidity - 50f) / 50f);
            actualHumidity = Mathf.RoundToInt(humidity);
        }
    }

}
