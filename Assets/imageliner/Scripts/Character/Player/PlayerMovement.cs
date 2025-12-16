using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool isMoving;
    [SerializeField] private float moveSpeed = 5.0f;
    private float savedMoveSpeed;

    //private Vector3 velocity;
    [SerializeField] private bool isDashing;
    //[SerializeField] private float dashPower = 15f;
    //[SerializeField] private float dashTime = 0.25f;
    //[SerializeField] private float dashCoolDown = 0.5f;
    //[SerializeField] private int dashStaminaNeeded = 0;

    private bool dashing = false;

    private void Start()
    {
        savedMoveSpeed = moveSpeed;
    }

    private float AdjustedMoveSpeed()
    {
        return Mathf.Clamp(moveSpeed + GameManager.singleton.playerStats.finalAtkSpeed, 0f, 15f);
    }

    public void Movement(Vector2 inputAxis, Vector3 moveDir)
    {
        if (dashing) return;

        transform.position += moveDir * AdjustedMoveSpeed() * Time.deltaTime;

        bool moving = moveDir.sqrMagnitude > 0.01f;
        isMoving = moving;
    }

    public void StartDash(Vector3 direction, float force, float duration, Rigidbody rb, GameObject dashTrail)
    {
        if (!dashing)
            StartCoroutine(DashRoutine(direction, force, duration, rb, dashTrail));
    }

    private IEnumerator DashRoutine(Vector3 direction, float force, float duration, Rigidbody rb, GameObject dashTrail)
    {
        dashing = true;

        float time = 0f;

        GameObject cloneDashTrail = Instantiate(dashTrail, transform);

        while (time < duration)
        {
            ////rb.MovePosition(rb.position + direction * force * Time.deltaTime);
            //rb.transform.position += direction * force * Time.deltaTime;
            rb.AddForce(direction * force * 10f, ForceMode.Force);
            time += Time.fixedDeltaTime;
            yield return null;
        }
        
        dashing = false;
        Destroy(cloneDashTrail,0.15f);

        rb.linearVelocity = Vector3.zero;
    }

    public void MovespeedReduce()
    {
        moveSpeed = moveSpeed * 0.6f;
    }

    public void MovespeedNormal()
    {
        moveSpeed = savedMoveSpeed;
    }

}
