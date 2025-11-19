using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<InventoryItem> allItems = new List<InventoryItem>();

    public void AddItem(InventoryItem toAdd)
    {
        //check if inv. full
        allItems.Add(toAdd);
        FindAnyObjectByType<UIInventory>().AddItemIconToInventory(toAdd);
        //update UI maybe
    }

    public void RemoveItem(InventoryItem toRemove)
    {
        Instantiate(toRemove.physicalRepresentation, transform.position, transform.rotation);
        allItems.Remove(toRemove);

        FindAnyObjectByType<UIInventory>().RemoveSpecificItem(toRemove);
    }

    public bool HasItem (InventoryItem toCheck)
    {
        return allItems.Contains(toCheck);
    }
}
