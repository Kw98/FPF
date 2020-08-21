using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private DpSManager DpsM;
    [SerializeField] private InvenotryManager inventory;
    [SerializeField] private string[] terrainTool;
    public Item item;
    [SerializeField] private GameObject dirtPilePrefab;
    [SerializeField] private GameObject dirtPilePreview;

    void Update()
    {
        //item = inventory.itemequiped;
        if (dirtPilePreview != null)
        {
            if (item.Name != "Hoe")
            {
                Destroy(dirtPilePreview);
                dirtPilePreview = null;
            }
        }
        if (terrainTool.Contains(item.Name))
            TerrainTool();
        else if (item.Name == "CarrotSeed")
            SeedPlanting(0);
        else if (item.Name == "TomatoSeed")
            SeedPlanting(1);
        else if (item.Name == "CornSeed")
            SeedPlanting(2);
        else if (item.Name == "EggplantSeed")
            SeedPlanting(3);
        else if (item.Name == "TurnipSeed")
            SeedPlanting(4);
        else if (item.Name == "PumpkinSeed")
            SeedPlanting(5);
        else
            recolt();
    }

    private void SeedPlanting(int vegetebalId)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            if (hit.transform != null)
            {
                if (hit.transform.gameObject.tag != "Dirt")
                    return;
                if (DpsM.PlantOnDirtPile(hit.transform.gameObject, vegetebalId))
                {
                    // est planté il faut que tu delete l'item de l'inventaire ou que tu réduit le stack de 1
                }

            }
        }
    }

    private void TerrainTool()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

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
    }

    private void hoe(GameObject go, RaycastHit hit)
    {
        if (go.tag != "Terrain")
        {
            Destroy(dirtPilePreview);
            dirtPilePreview = null;
            return;
        }
        Vector3 pos = go.GetComponent<GridSystem>().GetNearestGrid(hit.point);
        if (dirtPilePreview == null)
            dirtPilePreview = Instantiate(dirtPilePrefab, pos, Quaternion.identity);
        else
            dirtPilePreview.transform.position = pos;

        if (Input.GetMouseButtonDown(0))
        {
            if (DpsM.AddDirtPile(dirtPilePreview))
                dirtPilePreview = null;
        }
    }

    private void sprinkler(GameObject go)
    {
        if (go.tag != "Dirt" && go.tag != "Seed" && go.tag != "Plant")
            return;
        if (go.tag == "Plant")
            go = go.transform.parent.gameObject;
        if (Input.GetMouseButtonDown(0))
            DpsM.HumidifyPlant(go);
    }

    private void recolt()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            if (hit.transform != null)
            {
                GameObject go = hit.transform.gameObject;
                if (go.tag != "Dirt" && go.tag != "Seed" && go.tag != "Plant")
                    return;
                if (go.tag == "Plant")
                    go = go.transform.parent.gameObject;
                if (go.tag == "Seed")
                    go = go.transform.parent.gameObject;
                if (Input.GetMouseButtonDown(0))
                    DpsM.RecoltPlant(go);
            }
        }
    }

}
