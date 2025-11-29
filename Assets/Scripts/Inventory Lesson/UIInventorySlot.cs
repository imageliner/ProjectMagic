using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventorySlot : MonoBehaviour
{
    public bool inUse;
    public Image icon;
    public TextMeshProUGUI nameDisplay;
    [SerializeField] private Image rarityDisplay;

    public InventoryItem itemData;

    [SerializeField] private UIItemDescription itemDesc;
    [SerializeField] private UIUsableDescription usableDesc;

    private void Awake()
    {
        itemDesc = FindAnyObjectByType<UIItemDescription>();
        usableDesc = FindAnyObjectByType<UIUsableDescription>();
    }

    public void InitializeItemDisplay(InventoryItem item)
    {
        inUse = true;
        itemData = item;

        if (nameDisplay != null)
        {
            nameDisplay.text = item.itemName;
            nameDisplay.color = item.SetRarityColor();
        }

        if (icon != null)
        {
            icon.enabled = true;
            icon.sprite = item.itemIcon;
        }

        if (rarityDisplay != null)
        {
            if (item is EquipInventoryItem)
                rarityDisplay.color = item.SetRarityColor();
            else
                rarityDisplay.color = Color.clear;
        }
    }

    public void OnItemClicked()
    {
        Vector3 posOffset = new Vector3(-100, 25, 0);
        Vector3 newPos = transform.position + posOffset;

        if (itemData is UsableItem usable)
        {
            if (usableDesc == null)
            {
                usableDesc = FindAnyObjectByType<UIUsableDescription>();
            }
            usableDesc.EnableWindow();
            usableDesc.transform.position = newPos;
            usableDesc.InitializeUsableDescription(usable);

            return;
        }

        if (itemData is EquipInventoryItem equip)
        {
            
            if (equip.equipType == EquipType.Weapon)
            {
                
                if (itemDesc == null)
                {
                    itemDesc = FindAnyObjectByType<UIItemDescription>();
                }

                itemDesc.EnableWindow();
                itemDesc.transform.position = newPos;
                itemDesc.InitializeEquipDescription(equip);

                //FindAnyObjectByType<Inventory>().EquipNewWeapon(equip);
            }
            else if (equip.equipType == EquipType.Helmet)
            {
                FindAnyObjectByType<Inventory>().EquipNewHelmet(equip);
            }

        }
        else
        {
            //FindAnyObjectByType<Inventory>().RemoveItem(itemData);
            

            //UIItemDescription clonedDescription = Instantiate(itemDesc, newPos, transform.rotation, this.transform);
            //clonedDescription.InitializeItemDescription(itemData);

            if (itemDesc == null)
            {
                 itemDesc = FindAnyObjectByType<UIItemDescription>();
            }

            itemDesc.EnableWindow();
            itemDesc.transform.position = newPos;
            itemDesc.InitializeItemDescription(itemData);
        }
        //can also open another window with 2 options: equip(only if item is compatible) or remove

    }

    public void ClearSlot()
    {
        inUse = false;
        itemData = null;
        nameDisplay.text = "";
        icon.enabled = false;
        rarityDisplay.color = Color.clear;
    }
}
