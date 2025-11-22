using NUnit.Framework.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItemDescription : MonoBehaviour
{
    public InventoryItem itemData;
    public EquipInventoryItem equipData;

    private InventoryItem itemToDrop;

    [SerializeField] private GameObject uiVisuals;

    public Image itemIcon;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;

    private void Start()
    {
        DisableWindow();
    }

    public void EnableWindow()
    {
        uiVisuals.SetActive(true);
    }

    public void DisableWindow()
    {
        uiVisuals.SetActive(false);
    }

    public void InitializeItemDescription(InventoryItem item)
    {
        itemData = null;
        equipData = null;
        itemData = item;
        itemName.text = item.itemName;
        itemName.color = item.SetRarityColor();
        //itemName.color = item.itemColor;
        itemIcon.enabled = true;
        itemIcon.sprite = item.itemIcon;
        itemDescription.text = item.itemDescription;
    }

    public void InitializeEquipDescription(EquipInventoryItem equip)
    {
        itemData = null;
        equipData = null;
        equipData = equip;
        itemName.text = equip.itemName;
        itemName.color = equip.SetRarityColor();
        itemIcon.enabled = true;
        itemIcon.sprite = equip.itemIcon;
        itemDescription.text = equipData.itemDescription;
    }

    public void OnEquipClick()
    {
        
        FindAnyObjectByType<Inventory>().EquipNewWeapon(equipData);
        
        //FindAnyObjectByType<Inventory>().RemoveItem(itemData);
        DisableWindow();
    }

    public void OnDropClicked()
    {
        if (equipData != null)
        {
            itemToDrop = equipData;
        }
        if (itemData != null)
        {
            itemToDrop = itemData;
        }
        FindAnyObjectByType<Inventory>().RemoveItem(itemToDrop);
        DisableWindow();
    }

    public void OnWindowClose()
    {
        DisableWindow();
    }
}
