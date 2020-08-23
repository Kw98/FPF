using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] private GameObject tree;
    [SerializeField] private GameObject woodSappling;
    [SerializeField] private int maxSappling;
    private Manager manager;
    

    void Start()
    {
        if (!GameObject.Find("GameManager") && !GameObject.Find("GlobalSystem"))
        {
            Destroy(this);
            return;
        }
        manager = GameObject.Find("GameManager").GetComponent<Manager>();
    }

    public void cutDownTree()
    {
        int sapplingNb = (manager.data.player.specialisation.wood.actualLvl * maxSappling) / manager.data.player.specialisation.wood.maxLvl;
        if (sapplingNb == 0)
            sapplingNb = 1;
        for (int i = 0; i < sapplingNb; i++)
            Instantiate(woodSappling, transform.position + new Vector3(1, 0, 1), Quaternion.identity);
    }
}
