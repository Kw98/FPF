using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Sellitem : MonoBehaviour
{
    public Dialogue dialogue;
    public GameObject displayDialogue;
    private bool active = false;
    public InventoryObject inventory;
    public ItemObject money;
    public string NpcItem;
    public ItemObject NpcItemSell;
    public bool friendly = false;
    public GameObject activeQuete;
    public Image friendlyicon;
    public Sprite newicon;
    public TextMeshProUGUI frelndlyText;
    public int prize;
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
                    inventory.AddItem(new Item(money), prize);
                    inventory.RemoveItem(inventory.Container.Items[i].item, 1);
                    return;

                }
            }
        }
        if (Input.GetKeyDown("m"))
        {
            activeQuete.SetActive(true);
            for (int i = 0; i < inventory.Container.Items.Length; i++)
            {
                if (inventory.Container.Items[i].GetName() == "Quete")
                {
                    inventory.RemoveItem(inventory.Container.Items[i].item, 1);
                    validatequete();
                    return;
                }
            }


        }

      
    }
    void OnTriggerEnter(Collider other)
    {
        if (friendly == true)
            activeQuete.SetActive(false);

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

    public void validatequete()
    {
        activeQuete.SetActive(false);
        frelndlyText.text = "friendly";
        friendlyicon.sprite = newicon;
        friendly = true;
        prize = prize * 2;

    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

}
