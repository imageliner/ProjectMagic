using System.Collections.Generic;
using UnityEngine;

public class AbilityPool : MonoBehaviour
{
    [SerializeField] private PooledAbility abilityRef;
    [SerializeField] private List<PooledAbility> availableAbilities = new List<PooledAbility>();
    [SerializeField] private List<PooledAbility> unavailableAbilities = new List<PooledAbility>();

    private void Awake()
    {
        for (int index = 0; index < 20; index++)
        {
            CreatePooledAbility();
        }
    }

    private void CreatePooledAbility()
    {
        PooledAbility abilityClone = Instantiate(abilityRef, transform);
        abilityClone.InitializePooledAbilities(this);

        abilityClone.name = availableAbilities.Count.ToString();
        availableAbilities.Add(abilityClone);
        //abilityClone.gameObject.SetActive(false);
    }

    public PooledAbility GetAvailableAbility()
    {
        if (availableAbilities.Count == 0)
        {
            //return null;
            CreatePooledAbility();
        }

        PooledAbility firstAvailableAbility = availableAbilities[0];

        availableAbilities.RemoveAt(0);
        unavailableAbilities.Add(firstAvailableAbility);

        return firstAvailableAbility;
    }

    public void ReturnAbility(PooledAbility usedAbility)
    {
        unavailableAbilities.Remove(usedAbility);
        availableAbilities.Add(usedAbility);
    }
}
