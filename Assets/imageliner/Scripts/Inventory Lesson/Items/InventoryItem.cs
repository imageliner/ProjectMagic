using Unity.Burst.Intrinsics;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory Item")]
public class InventoryItem : ScriptableObject
{
    public int itemID;
    public string itemName;
    public string itemDescription;
    public Sprite itemIcon;
    //public Color itemColor;
    
    public ItemRarity itemRarity;

    

    public bool canDrop = true;

    public Color SetRarityColor()
    {
        switch (itemRarity)
        {
            case ItemRarity.Common:
                    return Color.white;

            case ItemRarity.Uncommon:
                return Color.green;

            case ItemRarity.Rare:
                return Color.blue;

            case ItemRarity.Epic:
                return Color.magenta;

            case ItemRarity.Legendary:
                return Color.yellow;

            default:
                return Color.white;

        }
    }
}

public enum ItemRarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
}
