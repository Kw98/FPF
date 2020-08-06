using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Food Object", menuName = "FPF/Inventory/Items/Food")]
public class FoodObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.Food;
    }
}