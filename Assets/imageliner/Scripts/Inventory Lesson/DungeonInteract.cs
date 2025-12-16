using NUnit.Framework.Interfaces;
using UnityEngine;

public class DungeonInteract : Interactable
{
    public override void OnInteract()
    {
        FindAnyObjectByType<UIDungeonMenu>().EnableUI();
    }
}
