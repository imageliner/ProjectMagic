using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public KeyCode pickUpKey;
    public InventoryItem itemData;

    private void Update()
    {
        if (Input.GetKeyDown(pickUpKey))
        {
            PickUpItem();
        }
    }

    public void PickUpItem()
    {
        FindAnyObjectByType<Inventory>().AddItem(itemData);
        Destroy(gameObject);
    }
}
