using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory Item")]
public class InventoryItem : ScriptableObject
{
    public int itemID;
    public string itemName;
    public string itemDescription;
    public Sprite itemIcon;
    public Color itemColor;
    public GameObject physicalRepresentation;

    public bool canEquip;
    public bool canDrop;
}
