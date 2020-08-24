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
    public ItemObject moneyItem;
    public string NpcItem;
    public ItemObject NpcItemSell;
    public bool friendly = false;
    public GameObject activeQuete;
    public Image friendlyicon;
    public Sprite newicon;
    public TextMeshProUGUI frelndlyText;
    public int prize;
    public GameObject inventoryManager;
    private int money;
    public AudioSource voice;
    // Start is called before the first frame update
    void Start()
    {
        voice.Stop();
        money = inventoryManager.GetComponent<InvenotryManager>().money;
    }

    // Update is called once per frame
    void Update()
    {
        money = inventoryManager.GetComponent<InvenotryManager>().money;
        if (Input.GetKeyDown("c"))
        {
            if (active == true)
            {
                if (money >= 1)
                {
                    inventory.RemoveItem(new Item(moneyItem), 1);
                    inventory.AddItem(new Item(NpcItemSell), 1);
                    return;
                }
            }
        }
        if (Input.GetKeyDown("x"))
        {
            if (active == true)
            {
                for (int i = 0; i < inventory.Container.Items.Length; i++)
                {
                    if (inventory.Container.Items[i].GetName() == NpcItem)
                    {
                        inventory.AddItem(new Item(moneyItem), prize);
                        inventory.RemoveItem(inventory.Container.Items[i].item, 1);
                        return;

                    }
                }
            }
        }
        if (Input.GetKeyDown("m"))
        {
            if (active == true)
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
            voice.Play();
            //TriggerDialogue();
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
            voice.Stop();
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
