using System.Collections;
using TMPro;
using UnityEngine;

public class UIPickupNotif : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pickupText;


    public void InitializeNotif(InventoryItem newItem)
    {
        pickupText.text = newItem.itemName + " picked up";
        pickupText.color = newItem.SetRarityColor();
    }
}
