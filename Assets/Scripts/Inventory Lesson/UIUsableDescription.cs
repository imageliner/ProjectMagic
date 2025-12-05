using NUnit.Framework.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIUsableDescription : MonoBehaviour
{
    public InventoryItem itemData;
    public UsableItem usableData;

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
        usableData = null;
        itemData = item;
        itemName.text = item.itemName;
        itemName.color = item.SetRarityColor();
        //itemName.color = item.itemColor;
        itemIcon.enabled = true;
        itemIcon.sprite = item.itemIcon;
        itemDescription.text = item.itemDescription;
    }

    public void InitializeUsableDescription(UsableItem usable)
    {
        itemData = null;
        usableData = null;
        usableData = usable;
        itemName.text = usable.itemName;
        itemName.color = usable.SetRarityColor();
        itemIcon.enabled = true;
        itemIcon.sprite = usable.itemIcon;
        itemDescription.text = usableData.itemDescription;
    }

    public void OnUseClick()
    {
        
        FindAnyObjectByType<Inventory>().UseItem(usableData);
        
        DisableWindow();
    }

    public void OnDropClicked()
    {
        if (usableData != null)
        {
            itemToDrop = usableData;
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
