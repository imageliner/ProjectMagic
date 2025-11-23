using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LootPool : MonoBehaviour
{
    [SerializeField] private ItemPickup dropPrefab;

    public List<LootItem> lootTable = new List<LootItem>();

    private float totalWeight()
    {
        return lootTable.Sum(l => l.dropChance);
    }

    public ItemPickup GetRandomDrop()
    {
        float randomPoint = Random.Range(0f, totalWeight());
        float currentWeight = 0f;

        foreach (LootItem item in lootTable)
        {
            currentWeight += item.dropChance;
            if (randomPoint <= currentWeight)
            {
                if (item.item != null)
                {
                    var drop = Instantiate(dropPrefab, transform.position, transform.rotation);
                    ItemPickup pickup = drop.GetComponent<ItemPickup>();
                    pickup.itemData = item.item;

                    return pickup;
                }
                
            }
        }
        // Fallback in case no item is selected (e.g., if totalWeight is 0 or randomPoint is out of bounds)
        return null;
    }
}
