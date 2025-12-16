using NUnit.Framework.Interfaces;
using UnityEngine;

public class BasePickup : Interactable
{

    public override void OnInteract()
    {
        FindAnyObjectByType<PlayerInputHandler>().interactableList.Remove(this);
        Destroy(gameObject);
    }
}
