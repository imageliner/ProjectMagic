using System;
using UnityEngine;

public class CharacterAnimator_FlagHandler : MonoBehaviour
{
    [SerializeField] private AudioSource footstepSource;

    public Action OnSpawnAttack;
    public Action OnDespawnAttack;

    public Action OnSpawnEffect;


    public Action CanCombo;
    public Action StopCombo;

    public void Flag_Footstep()
    {
        if (!footstepSource || !footstepSource.clip) return;

        //if (footstepSource.isPlaying) return;

        footstepSource.pitch = UnityEngine.Random.Range(0.95f, 1.05f);
        footstepSource.volume = UnityEngine.Random.Range(0.85f, 1.0f);
        footstepSource.Play();
    }

    public void Flag_SpawnHitbox()
    {
        OnSpawnAttack?.Invoke();
    }

    public void Flag_DespawnHitbox()
    {
        OnDespawnAttack?.Invoke();
    }

    public void Flag_SpawnEffect()
    {
        OnSpawnEffect?.Invoke();
    }

    public void Flag_CanCombo()
    {
        CanCombo?.Invoke();
    }

    public void Flag_StopCombo()
    {
        StopCombo?.Invoke();
    }
}
