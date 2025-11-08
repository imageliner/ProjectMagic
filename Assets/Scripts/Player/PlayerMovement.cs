using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool isMoving;
    [SerializeField] private float moveSpeed = 5.0f;

    //private Vector3 velocity;
    [SerializeField] private bool isDashing;
    [SerializeField] private float dashPower = 15f;
    [SerializeField] private float dashTime = 0.25f;
    [SerializeField] private float dashCoolDown = 0.5f;
    [SerializeField] private int dashStaminaNeeded = 0;

    public void Movement(Vector2 inputAxis, Vector3 moveDir)
    {
        //if (isDashing || isAttacking) return;

        transform.position += moveDir * moveSpeed * Time.deltaTime;

        bool moving = moveDir.sqrMagnitude > 0.01f;
        isMoving = moving;
    }
    public void Dash()
    {
        if (!isDashing && GameManager.singleton.playerStats.currentStamina >= dashStaminaNeeded)
        {
            //animator.SetTrigger("startDash");
            //Vector3 dashDir = new Vector3(mousePosition.x, 0f, mousePosition.z).normalized;
            //StartCoroutine(DashCoroutine(dashDir));
        }
    }

    //private IEnumerator DashCoroutine(Vector3 direction)
    //{
    //    isDashing = true;
    //
    //    GameManager.singleton.playerStats.SubtractStamina(dashStaminaNeeded);
    //    float elapsed = 0f;
    //    while (elapsed < dashTime)
    //    {
    //        transform.position += direction * dashPower * Time.deltaTime;
    //        elapsed += Time.deltaTime;
    //        yield return null;
    //    }
    //    yield return new WaitForSeconds(dashCoolDown);
    //    isDashing = false;
    //
    //}
}
