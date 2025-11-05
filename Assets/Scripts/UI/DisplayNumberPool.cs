using System.Collections.Generic;
using UnityEngine;

public class DisplayNumberPool : MonoBehaviour
{
    [SerializeField] private UIDamageNumber numberRef;
    [SerializeField] private List<UIDamageNumber> availableNumbers = new List<UIDamageNumber>();
    [SerializeField] private List<UIDamageNumber> unavailableNumbers = new List<UIDamageNumber>();

    private void Awake()
    {
        for (int index = 0; index < 20; index++)
        {
            CreatePooledNumber();
        }
    }

    private void CreatePooledNumber()
    {
        UIDamageNumber numberClone = Instantiate(numberRef, transform);
        numberClone.InitializePooledNumbers(this);

        numberClone.gameObject.name = availableNumbers.Count.ToString();
        availableNumbers.Add(numberClone);
        numberClone.gameObject.SetActive(false);
    }

    public UIDamageNumber GetAvailableNumber()
    {
        if (availableNumbers.Count == 0)
        {
            //return null;
            CreatePooledNumber();
        }

        UIDamageNumber firstAvailableNumber = availableNumbers[0];

        availableNumbers.RemoveAt(0);
        unavailableNumbers.Add(firstAvailableNumber);

        return firstAvailableNumber;
    }

    public void ReturnNumber(UIDamageNumber usedNumber)
    {
        unavailableNumbers.Remove(usedNumber);
        availableNumbers.Add(usedNumber);
    }
}
