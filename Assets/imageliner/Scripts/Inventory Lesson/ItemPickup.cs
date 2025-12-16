using UnityEngine;

public class ItemPickup : BasePickup
{
    
    public InventoryItem itemData;
    [SerializeField] private ParticleSystem pickupEffect;

    

    protected void Update()
    {
        var main = pickupEffect.main;
        main.startColor = itemData.SetRarityColor();

    }

    public override void OnInteract()
    {
        FindAnyObjectByType<Inventory>().AddItem(itemData);
        base.OnInteract();
    }
}
