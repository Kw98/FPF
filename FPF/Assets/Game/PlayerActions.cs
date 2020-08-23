using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private DpSManager DpsM;
    [SerializeField] private BreedingSystem bs;
    [SerializeField] private InvenotryManager inventory;
    [SerializeField] private string[] terrainTool;
    public Item item;
    [SerializeField] private GameObject dirtPilePrefab;
    [SerializeField] private GameObject dirtPilePreview;

    void Update()
    {
        item = inventory.itemequiped;
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
        else if (item.Name == "seed_carrot")
            SeedPlanting(0);
        else if (item.Name == "seed_tomato")
            SeedPlanting(1);
        else if (item.Name == "seed_corn")
            SeedPlanting(2);
        else if (item.Name == "seed_eggplant")
            SeedPlanting(3);
        else if (item.Name == "seed_turnip")
            SeedPlanting(4);
        else if (item.Name == "seed_pumpkin")
            SeedPlanting(5);
        Action();
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
                    inventory.inventory.RemoveItem(item, 1);
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

    private void Action()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            if (hit.transform != null)
            {
                GameObject go = hit.transform.gameObject;
                if (go.tag == "Plant" && Input.GetMouseButtonDown(0))
                    DpsM.RecoltPlant(go.transform.parent.gameObject.transform.parent.gameObject);
                else if (go.tag == "Seed" && Input.GetMouseButtonDown(0))
                    DpsM.RecoltPlant(go.transform.parent.gameObject);
                else if (go.tag == "Farm" && Input.GetKeyDown(KeyCode.E))
                    go.GetComponent<FarmHUD>().ShowHUD();
                //else if (go.tag == "Animal" && Input.GetKeyDown(KeyCode.E))
                //    go.GetComponent<Animal>().dropRessource();
                else if (go.tag == "Animal" && Input.GetKeyDown(KeyCode.K))
                    go.GetComponent<Animal>().kill();
                else if (go.tag == "Animal" && Input.GetKeyDown(KeyCode.B))
                    bs.Breed(go.name);
                else if (go.tag == "Animal" && Input.GetMouseButtonDown(0))
                    feedAnimal(go);
            }
        }
    }

    private void feedAnimal(GameObject go)
    {
        if (item.Name == "Carrot")
            go.GetComponent<Animal>().stat.food += 20;
        else if (item.Name == "Corn")
            go.GetComponent<Animal>().stat.food += 35;
        else if (item.Name == "Eggplant")
            go.GetComponent<Animal>().stat.food += 5;
        else if (item.Name == "Pumpkin")
            go.GetComponent<Animal>().stat.food += 65;
        else if (item.Name == "Turnip")
            go.GetComponent<Animal>().stat.food += 5;
        else if (item.Name == "Tomato")
            go.GetComponent<Animal>().stat.food += 20;

        if (go.GetComponent<Animal>().stat.food > 100)
            go.GetComponent<Animal>().stat.food = 100;
    }

}
