using NUnit.Framework.Interfaces;
using UnityEngine;

public class CurrencyPickup : BasePickup
{
    public int currencyAmount;

    public override void OnInteract()
    {
        FindAnyObjectByType<Inventory>().AddCurrency(currencyAmount);
        base.OnInteract();
    }
}
