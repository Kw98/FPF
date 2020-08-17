using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSystem : MonoBehaviour
{
    [SerializeField] private int lvlToUnlock;
    [SerializeField] private GameObject trigger;
    [SerializeField] private GameObject lockTerrain;
    private Manager m;

    void Start()
    {
        if (!GameObject.Find("GameManager"))
        {
            Destroy(this);
            return;
        }
        m = GameObject.Find("GameManager").GetComponent<Manager>();
        trigger.SetActive(false);

        if (lvlToUnlock <= m.data.player.specialisation.general.actualLvl)
        {
            lockTerrain.SetActive(false);
            trigger.SetActive(true);
        }
    }

    private void Update()
    {
        if (!trigger.activeInHierarchy && lvlToUnlock <= m.data.player.specialisation.general.actualLvl)
        {
            trigger.SetActive(true);
            lockTerrain.SetActive(false);
        }
    }
}
