using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using System.Linq;
using System.Linq.Expressions;

public class DpSManager : MonoBehaviour
{
    [SerializeField] private GameObject dirtPilePrefab;
    [SerializeField] private GameObject[] seedPrefabs;
    [SerializeField] private GameObject[] seedItemPrefabs;
    [SerializeField] private GameObject[] fruitItemPrefabs;
    public Dictionary<int, SaveFormat_DirtPile> DpS = new Dictionary<int, SaveFormat_DirtPile>();
    private Manager manager;

    private void Start()
    {
        if (!GameObject.Find("GameManager"))
        {
            Destroy(this);
            return;
        }
        manager = GameObject.Find("GameManager").GetComponent<Manager>();
        Load();
    }

    public bool AddDirtPile(GameObject dp)
    {
        foreach (KeyValuePair<int, SaveFormat_DirtPile> dps in DpS)
        {
            if (dps.Value.posX == dp.transform.position.x && dps.Value.posZ == dp.transform.position.z)
                return false;
        }
        int key = Random.Range(-99999, 99999);
        while (DpS.ContainsKey(key))
            key = Random.Range(-99999, 99999);
        dp.name = key.ToString("D6");
        SaveFormat_DirtPile dirtpile = new SaveFormat_DirtPile();
        dirtpile.posX = dp.transform.position.x;
        dirtpile.posY = dp.transform.position.y;
        dirtpile.posZ = dp.transform.position.z;
        dirtpile.vegetebalId = -1;
        DpS.Add(key, dirtpile);
        return true;
    }

    public bool PlantOnDirtPile(GameObject dirtpile, int vegetebalId)
    {
        foreach (KeyValuePair<int, SaveFormat_DirtPile> dp in DpS.ToList())
        {
            if (dirtpile.name == dp.Key.ToString("D6") && dp.Value.vegetebalId == -1)
            {
                SaveFormat_DirtPile sfdp = dp.Value;
                sfdp.vegetebalId = vegetebalId;
                DpS[dp.Key] = sfdp;
                Instantiate(seedPrefabs[dp.Value.vegetebalId], dirtpile.transform);
                return true;
            } else if (dirtpile.name == dp.Key.ToString("D6") && dp.Value.vegetebalId != -1)
                return false;
        }
        return false;
    }

    public void HumidifyPlant(GameObject go)
    {
        if (go.tag == "Seed")
            go.GetComponent<Seed>().Humidify();
        else {
            foreach (KeyValuePair<int, SaveFormat_DirtPile> dp in DpS)
            {
                if (dp.Key.ToString("D6") == go.name && dp.Value.vegetebalId != -1) {
                    for (int i = 0; i < go.transform.childCount; i++)
                    {
                        if (go.transform.GetChild(i).gameObject.tag == "Seed")
                        {
                            go.transform.GetChild(i).gameObject.GetComponent<Seed>().Humidify();
                            return;
                        }
                    }
                    return;
                }
            }
        }
    }

    public void RecoltPlant(GameObject go)
    {
        bool recolted = false;
        int key = 0;
        if (go.tag == "Seed")
            go = transform.parent.gameObject;
        foreach (KeyValuePair<int, SaveFormat_DirtPile> dp in DpS)
        {
            if (dp.Key.ToString("D6") == go.name && dp.Value.vegetebalId != -1)
            {
                for (int i = 0; i < go.transform.childCount; i++)
                {
                    if (go.transform.GetChild(i).gameObject.tag == "Seed" && go.transform.GetChild(i).gameObject.GetComponent<Seed>().IsGrowthEnded())
                    {
                        Dictionary<string, int> recolts = go.transform.GetChild(i).gameObject.GetComponent<Seed>().RecoltFruit();

                        Vector3 basepos = go.transform.position;
                        for (int f = 0; f < recolts["fruit"]; f++)
                            Instantiate(fruitItemPrefabs[dp.Value.vegetebalId], new Vector3(basepos.x + f / 5, basepos.y + f / 5, basepos.z + f / 5), Quaternion.identity);
                        //for (int f = 0; f < recolts["seed"]; f++)
                        //    Instantiate(seedItemPrefabs[dp.Value.vegetebalId], new Vector3(basepos.x + f / 8, basepos.y + f / 8, basepos.z + f / 8), Quaternion.identity);
                        recolted = true;
                        key = dp.Key;
                    }
                }
                return;
            }
        }
        if (recolted)
        {
            DpS.Remove(key);
            Destroy(go);
        }
    }

    private void Load()
    {
        if (File.Exists(manager.data.dpSDictionaryLocation))
        {
            var formatter = new BinaryFormatter();
            FileStream stream = new FileStream(manager.data.dpSDictionaryLocation, FileMode.Open);
            DpS = (Dictionary<int, SaveFormat_DirtPile>)formatter.Deserialize(stream);
        }

        foreach (KeyValuePair<int, SaveFormat_DirtPile> dp in DpS)
        {
            if (dp.Value.vegetebalId != -1)
            {
                GameObject dirt = Instantiate(dirtPilePrefab, new Vector3(dp.Value.posX, dp.Value.posY, dp.Value.posZ), Quaternion.identity);
                dirt.name = dp.Key.ToString("D6");
                GameObject seed = Instantiate(seedPrefabs[dp.Value.vegetebalId], dirt.transform);
                seed.GetComponent<Seed>().ImportData(dp.Value.seed);
            }
        }
    }

    public void Save()
    {
        foreach (KeyValuePair<int, SaveFormat_DirtPile> dp in DpS.ToList())
        {
            if (dp.Value.vegetebalId != -1)
            {
                GameObject dirtpile = GameObject.Find(dp.Key.ToString("D6"));
                for (int i = 0; i < dirtpile.transform.childCount; i++)
                {
                    if (dirtpile.transform.GetChild(i).gameObject.tag == "Seed")
                    {
                        SaveFormat_DirtPile sfdp = dp.Value;
                        sfdp.seed = dirtpile.transform.GetChild(i).gameObject.GetComponent<Seed>().ExportData();
                        DpS[dp.Key] = sfdp;
                        break;
                    }
                }
            }
        }
        var formatter = new BinaryFormatter();
        FileStream stream = new FileStream(manager.data.dpSDictionaryLocation, FileMode.Open);
        formatter.Serialize(stream, DpS);
        stream.Close();
    }

}
