using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LootPool : MonoBehaviour
{
    [SerializeField] private ItemPickup dropPrefab;
    [SerializeField] private CurrencyPickup currencyDropPrefab;

    public List<LootItem> lootTable = new List<LootItem>();

    [System.Serializable]
    public struct CurrencyRange
    {
        public int min;
        public int max;
    }

    [SerializeField] private CurrencyRange currencyRange;

    private float totalWeight()
    {
        return lootTable.Sum(l => l.dropChance);
    }

    public ItemPickup GetRandomDrop()
    {
        Vector3 offset = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
        float randomPoint = Random.Range(0f, totalWeight());
        float currentWeight = 0f;

        foreach (LootItem item in lootTable)
        {
            currentWeight += item.dropChance;
            if (randomPoint <= currentWeight)
            {
                if (item.item != null)
                {
                    var drop = Instantiate(dropPrefab, transform.position += offset, transform.rotation);
                    ItemPickup pickup = drop.GetComponent<ItemPickup>();
                    pickup.itemData = item.item;

                    return pickup;
                }
                
            }
        }
        // Fallback in case no item is selected (e.g., if totalWeight is 0 or randomPoint is out of bounds)
        return null;
    }

    public CurrencyPickup GetCurrencyDropAmount()
    {
        float spawnChance = Random.Range(0f, 1f);
        if (spawnChance <= 0.8f)
        {
            Vector3 offset = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
            int dropAmount = Mathf.CeilToInt(Random.Range(currencyRange.min, currencyRange.max));

            var drop = Instantiate(currencyDropPrefab, transform.position += offset, transform.rotation);
            CurrencyPickup pickup = drop.GetComponent<CurrencyPickup>();
            pickup.currencyAmount = dropAmount;

            return pickup;
        }
        else
            return null;
    }
}
