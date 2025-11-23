using UnityEngine;

public class ItemPickup : BasePickup
{
    
    public InventoryItem itemData;
    [SerializeField] private ParticleSystem pickupEffect;

    

    protected override void Update()
    {
        var main = pickupEffect.main;
        main.startColor = itemData.SetRarityColor();

        base.Update();
    }

    protected override void PickUpItem()
    {
        FindAnyObjectByType<Inventory>().AddItem(itemData);
        base.PickUpItem();
    }
}
