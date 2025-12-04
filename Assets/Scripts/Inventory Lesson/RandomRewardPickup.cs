using UnityEngine;

public class RandomRewardPickup : BasePickup
{

    protected override void Update()
    {
        base.Update();
    }


    protected override void PickUpItem()
    {
        RandomRewardManager.singleton.EnableUI();
        base.PickUpItem();
    }
}
