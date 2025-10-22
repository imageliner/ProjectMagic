using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerInputHandler inputHandler;
    [SerializeField] private Animator animator;
    PlayerStatManager playerStatManager;

    public Rigidbody _rb;
    public float moveSpeed = 5.0f;
    private float rotationSpeed = 100.0f;

    //private Vector3 velocity;
    private bool isDashing;
    private float dashPower = 15f;
    private float dashTime = 0.25f;
    private float dashCoolDown = 0.5f;
    private int dashStaminaNeeded = 25;

    public bool isAttacking;
    private int attackStaminaNeeded = 5;
    private float attackDashPower = 5f;
    private float attackDashTime = 0.15f;
    private float attackCoolDown = 0.15f;
    private int attackCountMax = 2;
    private int attackCount = 0;

    public bool isMoving;
    public bool damageRecovery;
    public float invincibilityTime = 0.5f;

    public Vector3 mousePosition;
    public Transform rotateTarget;
    public Quaternion lookRotation;

    private void Awake()
    {
        inputHandler = GetComponent<PlayerInputHandler>();
        animator = GetComponentInChildren<Animator>();
        playerStatManager = GetComponent<PlayerStatManager>();
    }


    void Update()
    {
        mousePosition = (rotateTarget.position - transform.position).normalized;
        lookRotation = Quaternion.LookRotation(mousePosition);
        lookRotation.x = 0;
        lookRotation.z = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        //create combat controller script? play a function on mouse click towards mouse direciton ^ see dash code
    }

    #region Movement
    public void Movement(Vector2 inputAxis)
    {
        if (isDashing || isAttacking) return;

        Vector3 moveDir = new Vector3(inputAxis.x, 0f, inputAxis.y);

        transform.position += moveDir * moveSpeed * Time.deltaTime;

        bool moving = moveDir.sqrMagnitude > 0.01f;

        Vector3 localMove = transform.InverseTransformDirection(moveDir.normalized);

        animator.SetFloat("Horizontal", localMove.x, 0.1f, Time.deltaTime);
        animator.SetFloat("Vertical", localMove.z, 0.1f, Time.deltaTime);
        //animator.SetBool("isMoving", moving);
    }
    public void Dash()
    {
        if (!isDashing && playerStatManager.staminaCurrent >= dashStaminaNeeded)
        {
            animator.SetTrigger("startDash");
            Vector3 dashDir = new Vector3(mousePosition.x, 0f, mousePosition.z).normalized;
            StartCoroutine(DashCoroutine(dashDir));
        }
    }

    private IEnumerator DashCoroutine(Vector3 direction)
    {
        isDashing = true;

        playerStatManager.staminaCurrent -= dashStaminaNeeded;
        float elapsed = 0f;
        while (elapsed < dashTime)
        {
            transform.position += direction * dashPower * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(dashCoolDown);
        isDashing = false;

    }
    #endregion

    public void Attack()
    {
        if (!isAttacking && playerStatManager.staminaCurrent >= attackStaminaNeeded && attackCount < attackCountMax)
        {
            Vector3 attackDir = new Vector3(mousePosition.x, 0f, mousePosition.z).normalized;
            StartCoroutine(Attack(attackDir));
        }
    }

    private IEnumerator Attack(Vector3 direction)
    {
        isAttacking = true;

        playerStatManager.staminaCurrent -= attackStaminaNeeded;
        float elapsed = 0f;
        animator.SetTrigger("startAttack");
        while (elapsed < attackDashTime)
        {      
            transform.position += direction * attackDashPower * Time.deltaTime;
            elapsed += Time.deltaTime;
            attackCount++;
            yield return null;
        }
        yield return new WaitForSeconds(attackCoolDown);
        isAttacking = false;
        attackCount = 0;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HealBox"))
        {
            int healAmount = 15;
            playerStatManager.TakeHealCalculation(healAmount);
            Debug.Log("Health Recieved");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("DamageBox") && damageRecovery == false)
        {
            Debug.Log("Damage taken");
            int dmgAmount = 15;
            StartCoroutine(TakeDamage(dmgAmount));
        }
    }

    private IEnumerator TakeDamage(int dmgAmount)
    {
        damageRecovery = true;
        playerStatManager.TakeDamageCalculation(dmgAmount);

        float elapsed = 0f;
        while (elapsed < invincibilityTime)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(invincibilityTime);
        damageRecovery = false;
    }
}
