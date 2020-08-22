using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sellitem : MonoBehaviour
{
    public Dialogue dialogue;
    public GameObject displayDialogue;
    private bool active = false;
    public InventoryObject inventory;
    public ItemObject money;
    public string NpcItem;
    public ItemObject NpcItemSell;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("c"))
        {
           
            for (int a = 0; a < inventory.Container.Items.Length; a++)
            {
                if (inventory.Container.Items[a].ID == 0)
                {
                    if (inventory.Container.Items[a].GetAmount() >= 1)
                    {
                        print(inventory.Container.Items[a].GetAmount());
                        inventory.RemoveItem(inventory.Container.Items[a].item, 1);
                        inventory.AddItem(new Item(NpcItemSell), 1);
                    }
                    print(inventory.Container.Items[a].GetAmount());
                    print("pas de thune");
                    return;
                }
            }
            
        }
        if (Input.GetKeyDown("x"))
        {
            for (int i = 0; i < inventory.Container.Items.Length; i++)
            {
                if (inventory.Container.Items[i].GetName() == NpcItem)
                {
                    inventory.AddItem(new Item(money), 2);
                    inventory.RemoveItem(inventory.Container.Items[i].item, 1);
                    return;

                }
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            active = !active;
            if (active == true)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            if (active == false)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = true;
            }
            displayDialogue.SetActive(active);
            TriggerDialogue();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {

            active = !active;
            if (active == true)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            if (active == false)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = true;
            }
            displayDialogue.SetActive(active);
        }
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

}
