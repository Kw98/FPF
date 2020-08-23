using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvenotryManager : MonoBehaviour
{
    public Item itemequiped;
    public int money = 0;
    public InventoryObject inventory;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
            if (inventory.Container.Items[i].ID == 0)
            {
                money = inventory.Container.Items[i].GetAmount();
                return;
            }
            else
                money = 0;
        }
    }

   
}
