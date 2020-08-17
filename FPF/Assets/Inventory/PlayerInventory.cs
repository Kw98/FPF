using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public InventoryObject inventory;
    public GameObject displayInventory;
    private bool active = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.transform != null)
                {
                    Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        var item = hit.transform.GetComponent<GroundItem>();
                        Item _item = new Item(item.item);
                        if (_item != null)
                        {
                            inventory.AddItem(_item, 1);
                            Destroy(hit.transform.gameObject);
                       }
                    }
                }
            }
        }
        if (Input.GetKeyDown("o"))
        {
            print("save");
            inventory.Save();
        }
        if (Input.GetKeyDown("p"))
            inventory.Load();
        if (Input.GetKeyDown("i")) {
            active = !active;
            if (active == true) {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            }
            if (active == false)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = true;
            }
            displayInventory.SetActive(active);

                }
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Items = new InventorySlot[22];
    }

}
