using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ActionBar : MonoBehaviour
{
    public GameObject inventoryBox;
    public GameObject inventoryManager;
    public InventoryObject inventory;
    public GameObject[] slotInventory = new GameObject[3];
    public Inventory Container;
    private int nb = 0;
    // Start is called before the first frame update
    void Start()
    {
        var mainitem = inventoryBox.GetComponent<DisplayInventory>();
        slotInventory[0].GetComponent<Image>().sprite = mainitem.itemEquipedSprite.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        var e = inventoryManager.GetComponent<InvenotryManager>();
        var mainitem = inventoryBox.GetComponent<DisplayInventory>();
        var money = inventory.database.GetItem[0].uiDisplay;
        slotInventory[0].GetComponent<Image>().sprite = mainitem.itemEquipedSprite.sprite;
        slotInventory[2].GetComponent<Image>().sprite = money;
        slotInventory[2].GetComponentInChildren<TextMeshProUGUI>().text = e.money.ToString("n0");
    }
}
