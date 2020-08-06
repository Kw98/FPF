using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Food Object", menuName = "FPF/Inventory/Items/Equipment")]
public class EquipementObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.Equipment;
    }
}