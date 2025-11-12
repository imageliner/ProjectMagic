using UnityEditor.PackageManager.UI;
using UnityEngine;
using static PlayerStats;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public enum AnimationStates
    {
        MoveBlend,
        Dash,
        AttackMelee,
        AttackMage
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

            default:
                Debug.LogWarning("Unknown Animation Value");
                break;
        }
    }
}
