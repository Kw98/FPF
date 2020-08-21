using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private string[] terrainTool;
    public GameObject farmingZone = null;
    public Item item;
    [SerializeField] private GameObject dirtPilePrefab;
    [SerializeField] private GameObject dirtPilePreview;

    void Update()
    {
        if (farmingZone && terrainTool.Contains(item.Name))
            TerrainTool();
    }

    private void TerrainTool()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        farmingZone.SetActive(false);
        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            if (hit.transform != null)
            {
                if (item.Name == "Hoe")
                {
                    hoe(hit.transform.gameObject, hit);
                }
                else if (item.Name == "Arosoire")
                {
                    sprinkler(hit.transform.gameObject);
                }
            }
        }
        farmingZone.SetActive(true);
    }

    private void hoe(GameObject go, RaycastHit hit)
    {
        if (go.tag != "Terrain")
            return;
        Vector3 pos = farmingZone.transform.parent.GetComponent<GridSystem>().GetNearestGrid(hit.point);
        if (dirtPilePreview == null)
            dirtPilePreview = Instantiate(dirtPilePrefab, pos, Quaternion.identity);
        else
            dirtPilePreview.transform.position = pos;

        if (Input.GetMouseButtonDown(0))
        {
        }
    }

    private void sprinkler(GameObject go)
    {
        Debug.Log("other: " + go.name);
    }

}
