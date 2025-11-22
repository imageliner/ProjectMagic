using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public KeyCode pickUpKey;
    public InventoryItem itemData;
    [SerializeField] private GameObject interactCanvas;
    [SerializeField] private ParticleSystem pickupEffect;
    private bool inPickupRange;

    private void Start()
    {
        interactCanvas.SetActive(false);
        inPickupRange = false;
    }

    private void Update()
    {
        var main = pickupEffect.main;
        main.startColor = itemData.SetRarityColor();
        if (inPickupRange)
        {
            if (Input.GetKeyDown(pickUpKey))
            {
                PickUpItem();
            }
        }
    }

    public void PickUpItem()
    {
        FindAnyObjectByType<Inventory>().AddItem(itemData);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactCanvas.SetActive(true);
            inPickupRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactCanvas.SetActive(false);
            inPickupRange = false;
        }
    }
}
