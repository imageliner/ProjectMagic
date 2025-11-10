using UnityEngine;

public class PooledAbility : MonoBehaviour
{
    private AbilityPool poolOwner;

    public CharacterAbility ability;

    public void InitializePooledAbilities(AbilityPool owner)
    {
        poolOwner = owner;
    }
    private void ResetAbility()
    {

        poolOwner.ReturnAbility(this);

        gameObject.SetActive(false);
    }
}
