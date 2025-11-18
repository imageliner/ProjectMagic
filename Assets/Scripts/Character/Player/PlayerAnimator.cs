using System.Collections;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerCombat _combat;

    public bool spawnHitbox;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        _combat = GetComponentInParent<PlayerCombat>();
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
        InCombat
    }

    public IEnumerator FreezeCurrentAnim(float duration)
    {
        animator.speed = 0f;
        yield return new WaitForSeconds(duration);
        animator.speed = 1f;
    }

    public void SetAnimationState(AnimationStates states, Vector3 localMove = new Vector3())
    {
        switch (states)
        {
            case AnimationStates.MoveBlend:
                animator.SetFloat("Horizontal", localMove.x, 0.1f, Time.deltaTime);
                animator.SetFloat("Vertical", localMove.z, 0.1f, Time.deltaTime);
                break;

            case AnimationStates.Dash:
                animator.SetTrigger("startDash");
                break;

            case AnimationStates.AttackMelee:
                animator.SetTrigger("startAttack");
                break;

            case AnimationStates.AttackMage:
                animator.SetTrigger("startAttackMage");
                break;

            case AnimationStates.InCombat:
                animator.SetFloat("Horizontal", localMove.x, 0.1f, Time.deltaTime);
                animator.SetFloat("Vertical", localMove.z, 0.1f, Time.deltaTime);
                break;

            default:
                Debug.LogWarning("Unknown Animation Value");
                break;
        }
    }

    public void Flag_SpawnHitbox()
    {
        _combat.spawnAttack = true;
    }

    public void Flag_DespawnHitBox()
    {
        _combat.spawnAttack = false;
    }
}
