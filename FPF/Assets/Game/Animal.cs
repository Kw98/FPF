using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Animal : MonoBehaviour
{
    [SerializeField] private GameObject animal;
    [SerializeField] private GameObject hudPrefab;
    [SerializeField] private GameObject ressourcePrefab;
    [SerializeField] private GameObject meatPrefab;
    [SerializeField] private SaveFormat_Spe spec;
    public SaveFormat_Animal stat;
    private bool inAction = false;
    private GameObject hud;
    private Manager manager;
    private GlobalTime gt;
    private float ffood;

    // Start is called before the first frame update
    void Start()
    {
        if (!GameObject.Find("GameManager") && !GameObject.Find("GlobalSystem"))
        {
            Destroy(this);
            return;
        }

        manager = GameObject.Find("GameManager").GetComponent<Manager>();
        gt = GameObject.Find("GlobalSystem").GetComponent<GlobalTime>();
        hud = Instantiate(hudPrefab, FindObjectOfType<Canvas>().transform);
        hud.transform.localScale = new Vector3(0.5f, 0.5f, 1);

        if (stat.id == 0)
            spec = manager.data.player.specialisation.cow;
        else if (stat.id == 1)
            spec = manager.data.player.specialisation.chicken;
        else if (stat.id == 1)
            spec = manager.data.player.specialisation.pig;

        ffood = stat.food;
        if (stat.actualGrowthTime != 0)
            return;

        stat.growthTime -= ((stat.growthTime / 2f) * spec.actualLvl) / 100f;
        stat.RPR -= ((stat.RPR / 2f) * spec.actualLvl) / 100f;
        stat.actualRPT = 0;
        stat.actualGrowthTime = 0;
        stat.adult = false;
        stat.food = 100;
        ffood = stat.food;
        stat.posX = transform.position.x;
        stat.posY = transform.position.y;
        stat.posZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        hud.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, -0.5f, 0));
        stat.posX = transform.position.x;
        stat.posY = transform.position.y;
        stat.posZ = transform.position.z;
        if (stat.adult)
        {
            if (gt.hour >= 6 && gt.hour <= 20)
                stat.actualRPT += Time.deltaTime * gt.daySpeed;
            else
                stat.actualRPT += Time.deltaTime * gt.nightSpeed;
            if (stat.actualRPT >= stat.RPR)
            {
                stat.actualRPT = 0;
                stat.ressource = true;
            }
        } else
        {
            if (gt.hour >= 6 && gt.hour <= 20)
                stat.actualGrowthTime += Time.deltaTime * gt.daySpeed;
            else
                stat.actualGrowthTime += Time.deltaTime * gt.nightSpeed;
            if (stat.actualGrowthTime >= stat.growthTime)
            {
                stat.adult = true;

                if (stat.id == 0)
                    manager.data.player.specialisation.cow.GainExp(2000);
                else if (stat.id == 1)
                    manager.data.player.specialisation.chicken.GainExp(2000);
                else if (stat.id == 2)
                    manager.data.player.specialisation.pig.GainExp(2000);
            }
            float growth = (stat.actualGrowthTime / stat.growthTime) / 2;
            animal.transform.localScale = new Vector3(0.5f + growth, 0.5f + growth, 0.5f + growth);
        }
        food();
        if (stat.food <= 0)
        {
            Destroy(hud);
            Destroy(gameObject);
            return;
        }
        hud.GetComponent<AnimalHUD>().UpdateTimer(stat.growthTime - stat.actualGrowthTime);
        hud.GetComponent<AnimalHUD>().UpdateFood(ffood);
    }

    private void food()
    {
        ffood -= 0.02f;
        stat.food = Mathf.RoundToInt(ffood);
    }

    public void  dropRessource()
    {
        if (!stat.ressource && ressourcePrefab)
            return;
        if (stat.id == 0)
        {
            manager.data.player.specialisation.cow.GainExp(500);
            stat.RPR -= ((stat.RPR / 2f) * manager.data.player.specialisation.cow.actualLvl) / 100f;
        }
        else if (stat.id == 1)
        {
            manager.data.player.specialisation.chicken.GainExp(500);
            stat.RPR -= ((stat.RPR / 2f) * manager.data.player.specialisation.chicken.actualLvl) / 100f;
        }
        else if (stat.id == 2)
        {
            manager.data.player.specialisation.pig.GainExp(500);
            stat.RPR -= ((stat.RPR / 2f) * manager.data.player.specialisation.pig.actualLvl) / 100f;
        }
        stat.RPR -= ((stat.RPR / 2f) * spec.actualLvl) / 100f;
        Instantiate(ressourcePrefab, transform.position + new Vector3(1, 0, 1), Quaternion.identity);
    }

    public void kill()
    {
        if (meatPrefab)
            Instantiate(meatPrefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        Destroy(hud);
        Destroy(gameObject);
    }

}
