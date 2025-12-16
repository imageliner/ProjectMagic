using System.Collections;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetCombatState(bool combatState)
    {
        animator.SetBool("inCombat", combatState);
    }

    public enum AnimationStates
    {
        MoveBlend,
        Dash,
        AttackMelee,
        AttackMage,
        InCombat,
        CastingSpell
    }

    public IEnumerator FreezeCurrentAnim(float duration)
    {
        animator.speed = 0f;

        ParticleSystem[] allParticles = FindObjectsByType<ParticleSystem>(FindObjectsSortMode.None);

        foreach (ParticleSystem ps in allParticles)
        {
            if (ps.isPlaying)
            {
                ps.Pause();
            }
        }
        

        yield return new WaitForSeconds(duration);
        animator.speed = 1.0f;
        foreach (ParticleSystem ps in allParticles)
        {
            if (ps)
            {
                ps.Play();
            }
        }
    }


    public void SetAnimationState(AnimationStates states, Vector3 localMove = new Vector3())
    {
        switch (states)
        {
            case AnimationStates.MoveBlend:
                animator.SetFloat("AttackSpeed", Mathf.Clamp(GameManager.singleton.playerStats.finalAtkSpeed, 0.5f, 3.0f));
                animator.SetFloat("Horizontal", localMove.x, 0.1f, Time.deltaTime);
                animator.SetFloat("Vertical", localMove.z, 0.1f, Time.deltaTime);
                break;

            case AnimationStates.Dash:
                animator.SetTrigger("startDash");
                break;

            case AnimationStates.AttackMelee:
                animator.SetFloat("AttackSpeed", Mathf.Clamp(GameManager.singleton.playerStats.finalAtkSpeed, 0.5f, 3.0f));
                animator.SetTrigger("startAttack");
                break;

            case AnimationStates.AttackMage:
                animator.SetFloat("AttackSpeed", Mathf.Clamp(GameManager.singleton.playerStats.finalAtkSpeed, 0.5f, 3.0f));
                animator.SetTrigger("startAttackMage");
                break;

            case AnimationStates.InCombat:
                animator.SetFloat("AttackSpeed", Mathf.Clamp(GameManager.singleton.playerStats.finalAtkSpeed, 0.5f, 3.0f));
                animator.SetFloat("Horizontal", localMove.x, 0.1f, Time.deltaTime);
                animator.SetFloat("Vertical", localMove.z, 0.1f, Time.deltaTime);
                break;

            case AnimationStates.CastingSpell:
                animator.SetTrigger("startCasting");
                break;

            default:
                Debug.LogWarning("Unknown Animation Value");
                break;
        }
    }

    
}
