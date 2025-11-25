using System;
using UnityEngine;

public class CharacterAnimator_FlagHandler : MonoBehaviour
{
    public Action OnSpawnAttack;
    public Action OnDespawnAttack;

    public void Flag_SpawnHitbox()
    {
        OnSpawnAttack?.Invoke();
    }

    public void Flag_DespawnHitbox()
    {
        OnDespawnAttack?.Invoke();
    }
}
