using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventorySlot : MonoBehaviour
{
    public bool inUse;
    public Image icon;
    public TextMeshProUGUI nameDisplay;

    public InventoryItem itemData;

    [SerializeField] private UIItemDescription itemDesc;

    private void Awake()
    {
        itemDesc = FindAnyObjectByType<UIItemDescription>();
    }

    public void InitializeItemDisplay(InventoryItem item)
    {
        inUse = true;
        itemData = item;
        nameDisplay.text = item.itemName;
        nameDisplay.color = item.itemColor;
        icon.enabled = true;
        icon.sprite = item.itemIcon;
    }

    public void ClickToDelete()
    {
        if (itemData is EquipInventoryItem equip)
        {
            if (equip.equipType == EquipType.Weapon)
            {
                FindAnyObjectByType<Inventory>().EquipNewWeapon(equip);
            }
            else if (equip.equipType == EquipType.Helmet)
            {
                FindAnyObjectByType<Inventory>().EquipNewHelmet(equip);
            }

        }
        else
        {
            //FindAnyObjectByType<Inventory>().RemoveItem(itemData);
            Vector3 posOffset = new Vector3(-100, 25, 0);
            Vector3 newPos = transform.position + posOffset;

            //UIItemDescription clonedDescription = Instantiate(itemDesc, newPos, transform.rotation, this.transform);
            //clonedDescription.InitializeItemDescription(itemData);

            if (itemDesc == null)
            {
                 itemDesc = FindAnyObjectByType<UIItemDescription>();
            }

            itemDesc.gameObject.SetActive(true);
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
    }
}
