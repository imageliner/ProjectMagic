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
    public TextMeshProUGUI itemDamage;
    public TextMeshProUGUI itemDescription;
    public TextMeshProUGUI itemStatsText;

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
        itemStatsText.text = "";
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
        itemDamage.text = "Phys Atk " + equip.GetDamage().ToString();
        itemDescription.text = equip.itemDescription;
        itemStatsText.text = equip.GetStats();
    }

    public void OnEquipClick()
    {
        if (equipData.equipType == EquipType.Helmet)
        {
            FindAnyObjectByType<Inventory>().EquipNewHelmet(equipData);
        }
        if (equipData.equipType == EquipType.Weapon)
        {
            FindAnyObjectByType<Inventory>().EquipNewWeapon(equipData);
        }

        SoundManager.singleton.PlayAudio(SoundManager.singleton.sfx_Equip);
        
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

        SoundManager.singleton.PlayAudio(SoundManager.singleton.sfx_Pickup);

        DisableWindow();
    }

    public void OnWindowClose()
    {
        SoundManager.singleton.PlayAudio(SoundManager.singleton.sfx_Deny);
        DisableWindow();
    }
}
