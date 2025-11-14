using System.Collections.Generic;
using UnityEngine;
using static WeaponObject;

public class CharacterBase : MonoBehaviour
{
    [SerializeField] protected CharacterType characterType;

    [SerializeField] private DisplayNumberPool numberPool;

    //keep track of attack hitboxes received
    protected HashSet<int> processedAttackIDs = new HashSet<int>();

    public enum CharacterType
    {
        NPC,
        Enemy,
        Player
    }

    protected virtual void Awake()
    {
        if (numberPool == null)
        {
            numberPool = FindAnyObjectByType<DisplayNumberPool>();
        }
    }

    protected void SpawnDmgNumber(int number)
    {
        UIDamageNumber newNumber = numberPool.GetAvailableNumber();

        if (newNumber == null) return;

        newNumber.transform.position = transform.position;
        newNumber.transform.rotation = transform.rotation;
        newNumber.gameObject.SetActive(true);
        newNumber.UseNumber(number);
    }
}
