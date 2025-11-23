using NUnit.Framework.Interfaces;
using UnityEngine;

public class BasePickup : MonoBehaviour
{
    public KeyCode pickUpKey;
    [SerializeField] private GameObject interactCanvas;
    protected bool inPickupRange;

    protected virtual void Start()
    {
        interactCanvas.SetActive(false);
        inPickupRange = false;
    }

    protected virtual void Update()
    {
        if (inPickupRange)
        {
            if (Input.GetKeyDown(pickUpKey))
            {
                PickUpItem();
            }
        }
    }

    protected virtual void PickUpItem()
    {
        Destroy(gameObject);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactCanvas.SetActive(true);
            inPickupRange = true;
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactCanvas.SetActive(false);
            inPickupRange = false;
        }
    }
}
