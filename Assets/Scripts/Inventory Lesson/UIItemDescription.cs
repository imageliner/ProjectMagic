using NUnit.Framework.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItemDescription : MonoBehaviour
{
    public InventoryItem itemData;

    public Image itemIcon;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;

    private void Start()
    {
        //gameObject.SetActive(false);
    }

    public void InitializeItemDescription(InventoryItem item)
    {
        itemData = item;
        itemName.text = item.itemName;
        itemName.color = item.itemColor;
        itemIcon.enabled = true;
        itemIcon.sprite = item.itemIcon;
    }

    public void OnDropClicked()
    {
        FindAnyObjectByType<Inventory>().RemoveItem(itemData);
        gameObject.SetActive(false);
    }

    public void OnWindowClose()
    {
        gameObject.SetActive(false);
    }
}
