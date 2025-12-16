using TMPro;
using UnityEngine;

public class RandomRewardPickup : BasePickup
{
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private int rewardCost;


    protected override void Start()
    {
        base.Start();

        if (rewardCost > 0 )
        {
            costText.gameObject.SetActive(true);
            costText.text = "Cost: " + rewardCost.ToString() + "g";
        }
        else
            costText.gameObject.SetActive(false);
    }

    public override void OnInteract()
    {
        if (rewardCost > 0)
        {
            var inv = FindAnyObjectByType<Inventory>();
            if (inv.currency < rewardCost)
                return;

            inv.RemoveCurrency(rewardCost);
        }
        RandomRewardManager.singleton.EnableUI();
        base.OnInteract();
    }
}
