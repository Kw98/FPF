using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSystem : MonoBehaviour
{
    [SerializeField] private int lvlToUnlock;
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

        if (lvlToUnlock <= m.data.player.specialisation.general.actualLvl)
        {
            lockTerrain.SetActive(false);
        }
    }

    private void Update()
    {
        if (lvlToUnlock <= m.data.player.specialisation.general.actualLvl)
        {
            lockTerrain.SetActive(false);
        }
    }
}
