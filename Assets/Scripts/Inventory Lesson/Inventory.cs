using System;
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

    public Action<int> onCurrencyChanged;
    public Action onInventoryChanged;

    private void Awake()
    {
        playerPos = FindAnyObjectByType<PlayerCharacter>().transform;
    }

    public void AddItem(InventoryItem toAdd)
    {
        //check if inv. full
        allItems.Add(toAdd);
        onInventoryChanged?.Invoke();
        
    }

    public void RemoveItem(InventoryItem toRemove)
    {
        var drop = Instantiate(pickupPrefab, playerPos.transform.position, playerPos.transform.rotation);
        ItemPickup pickup = drop.GetComponent<ItemPickup>();
        pickup.itemData = toRemove;

        allItems.Remove(toRemove);

        onInventoryChanged?.Invoke();

        //FindAnyObjectByType<UIInventory>().RemoveSpecificItem(toRemove);
    }

    public void DeleteItem(InventoryItem toDelete)
    {
        allItems.Remove(toDelete);

        onInventoryChanged?.Invoke();

        //FindAnyObjectByType<UIInventory>().RemoveSpecificItem(toDelete);
    }

    public void RemoveItemForEquip(InventoryItem toRemove)
    {
        allItems.Remove(toRemove);

        onInventoryChanged?.Invoke();

        //FindAnyObjectByType<UIInventory>().RemoveSpecificItem(toRemove);
    }

    public bool HasItem (InventoryItem toCheck)
    {
        return allItems.Contains(toCheck);
    }
    public void AddCurrency(int currencyAmount)
    {
        currency += currencyAmount;
        onCurrencyChanged?.Invoke(currency);
        //FindAnyObjectByType<UIInventory>().UpdateCurrencyText(currency);
    }

    public void RemoveCurrency(int currencyAmount)
    {
        currency -= currencyAmount;
        onCurrencyChanged?.Invoke(currency);
        //FindAnyObjectByType<UIInventory>().UpdateCurrencyText(currency);
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

        onInventoryChanged?.Invoke();
    }
}
