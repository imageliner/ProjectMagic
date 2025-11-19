using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<InventoryItem> allItems = new List<InventoryItem>();

    private Transform playerPos;

    public InventoryItem equippedHelmet;
    public InventoryItem equippedWeapon;

    private void Awake()
    {
        playerPos = FindAnyObjectByType<PlayerCharacter>().transform;
    }

    public void AddItem(InventoryItem toAdd)
    {
        //check if inv. full
        allItems.Add(toAdd);
        FindAnyObjectByType<UIInventory>().AddItemIconToInventory(toAdd);
        //update UI maybe
    }

    public void RemoveItem(InventoryItem toRemove)
    {
        Instantiate(toRemove.physicalRepresentation, playerPos.transform.position, playerPos.transform.rotation);
        allItems.Remove(toRemove);

        FindAnyObjectByType<UIInventory>().RemoveSpecificItem(toRemove);
    }

    public bool HasItem (InventoryItem toCheck)
    {
        return allItems.Contains(toCheck);
    }

    public void EquipNewHelmet(EquipInventoryItem newHelmet)
    {
        if (equippedHelmet != null)
        {
            //means one is equiped
            //unequip current one
            //update ui (equipped) checkmark 

        }

        equippedHelmet = newHelmet;

        //Update visuals of player
        //Update UI (equipped) checkmark
    }

    public void EquipNewWeapon(EquipInventoryItem newWeapon)
    {
        if (equippedWeapon != null)
        {
            //means one is equiped
            //unequip current one
            //update ui (equipped) checkmark 

        }

        equippedWeapon = newWeapon;

        //Update visuals of player
        //Update UI (equipped) checkmark
    }
}
