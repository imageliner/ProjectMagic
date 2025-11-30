using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject pickupPrefab;

    public List<InventoryItem> allItems = new List<InventoryItem>();

    private Transform playerPos;

    [SerializeField] private int currency;

    
    public EquipInventoryItem equippedWeapon;
    public EquipInventoryItem equippedHelmet;
    public EquipInventoryItem equippedArmor;

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
        var drop = Instantiate(pickupPrefab, playerPos.transform.position, playerPos.transform.rotation);
        ItemPickup pickup = drop.GetComponent<ItemPickup>();
        pickup.itemData = toRemove;

        allItems.Remove(toRemove);

        FindAnyObjectByType<UIInventory>().RemoveSpecificItem(toRemove);
    }

    public void DeleteItem(InventoryItem toDelete)
    {
        allItems.Remove(toDelete);

        FindAnyObjectByType<UIInventory>().RemoveSpecificItem(toDelete);
    }

    public void RemoveItemForEquip(InventoryItem toRemove)
    {
        allItems.Remove(toRemove);

        FindAnyObjectByType<UIInventory>().RemoveSpecificItem(toRemove);
    }

    public bool HasItem (InventoryItem toCheck)
    {
        return allItems.Contains(toCheck);
    }
    public void AddCurrency(int currencyAmount)
    {
        currency += currencyAmount;
        FindAnyObjectByType<UIInventory>().UpdateCurrencyText(currency);
    }

    public void RemoveCurrency(int currencyAmount)
    {
        currency -= currencyAmount;
        FindAnyObjectByType<UIInventory>().UpdateCurrencyText(currency);
    }

    public void EquipNewHelmet(EquipInventoryItem newHelmet)
    {
        if (equippedHelmet != null)
        {
            //means one is equiped
            //unequip current one
            //update ui (equipped) checkmark 
            AddItem(equippedHelmet);
            equippedHelmet = null;
        }
        allItems.Remove(newHelmet);
        equippedHelmet = newHelmet;
        FindAnyObjectByType<UIEquipment>().EquipToSlot(newHelmet);
        FindAnyObjectByType<PlayerGearHandler>().EquipGearType(newHelmet.gearItem, newHelmet.gearItem.GetGearObject().GetGearType());

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

            AddItem(equippedWeapon);
            equippedWeapon = null;

        }
        allItems.Remove(newWeapon);
        equippedWeapon = newWeapon;
        FindAnyObjectByType<UIEquipment>().EquipToSlot(newWeapon);
        FindAnyObjectByType<PlayerGearHandler>().EquipGearType(newWeapon.gearItem, newWeapon.gearItem.GetGearObject().GetGearType());

        //Update visuals of player
        //Update UI (equipped) checkmark
    }

    public void UseItem(UsableItem newUsable)
    {
        if (newUsable.usableType == UsableType.Health)
            GameManager.singleton.playerStats.health.AddResource(newUsable.value);
        if (newUsable.usableType == UsableType.Mana)
            GameManager.singleton.playerStats.mana.AddResource(newUsable.value);
        

        DeleteItem(newUsable);


        //Update visuals of player
        //Update UI (equipped) checkmark
    }
}
