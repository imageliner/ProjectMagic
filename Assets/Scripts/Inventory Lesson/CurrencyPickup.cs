using NUnit.Framework.Interfaces;
using UnityEngine;

public class CurrencyPickup : BasePickup
{
    public int currencyAmount;

    protected override void PickUpItem()
    {
        FindAnyObjectByType<Inventory>().AddCurrency(currencyAmount);
        base.PickUpItem();
    }
}
