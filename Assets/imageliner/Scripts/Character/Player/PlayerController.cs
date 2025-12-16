using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject testAttackBox;

    public Rigidbody _rb;
    public float moveSpeed = 5.0f;
    private float rotationSpeed = 100.0f;

    //private Vector3 velocity;


    public bool isAttacking;
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
        animator = GetComponentInChildren<Animator>();
    }


    void Update()
    {
        mousePosition = (rotateTarget.position - transform.position).normalized;
        lookRotation = Quaternion.LookRotation(mousePosition);
        lookRotation.x = 0;
        lookRotation.z = 0;
        if (isMoving)
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        //create combat controller script? play a function on mouse click towards mouse direciton ^ see dash code
    }

    #region Movement
    public void Movement(Vector2 inputAxis)
    {
        if (isAttacking) return;

        Vector3 moveDir = new Vector3(inputAxis.x, 0f, inputAxis.y);

        transform.position += moveDir * moveSpeed * Time.deltaTime;

        bool moving = moveDir.sqrMagnitude > 0.01f;
        isMoving = moving;

        Vector3 localMove = transform.InverseTransformDirection(moveDir.normalized);

        animator.SetFloat("Horizontal", localMove.x, 0.1f, Time.deltaTime);
        animator.SetFloat("Vertical", localMove.z, 0.1f, Time.deltaTime);
        //animator.SetBool("isMoving", moving);
    }
    //public void Dash()
    //{
    //    if (!isDashing && GameManager.singleton.playerStats.currentStamina >= dashStaminaNeeded)
    //    {
    //        animator.SetTrigger("startDash");
    //        Vector3 dashDir = new Vector3(mousePosition.x, 0f, mousePosition.z).normalized;
    //        StartCoroutine(DashCoroutine(dashDir));
    //    }
    //}

    //private IEnumerator DashCoroutine(Vector3 direction)
    //{
    //    isDashing = true;

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

    //}
    #endregion

    public void Attack()
    {
        //if (!isAttacking && GameManager.singleton.playerStats.currentStamina >= attackStaminaNeeded && attackCount < attackCountMax)
        if (!isAttacking && attackCount < attackCountMax)
        {
            Vector3 attackDir = new Vector3(mousePosition.x, 0f, mousePosition.z).normalized;
            StartCoroutine(Attack(attackDir));
        }
    }

    private IEnumerator Attack(Vector3 direction)
    {
        isAttacking = true;

        //GameManager.singleton.playerStats.SubtractStamina(dashStaminaNeeded);
        float elapsed = 0f;
        animator.SetTrigger("startAttack");
        while (elapsed < attackDashTime)
        {      
            transform.position += direction * attackDashPower * Time.deltaTime;
            elapsed += Time.deltaTime;
            attackCount++;
            
            yield return null;
        }
        Destroy(Instantiate(testAttackBox, transform), 0.5f);
        yield return new WaitForSeconds(attackCoolDown);
        isAttacking = false;
        attackCount = 0;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HealBox"))
        {
            int healAmount = 15;
            GameManager.singleton.playerStats.health.AddResource(healAmount);
            Debug.Log("Health Recieved");
        }
        if (other.CompareTag("ManaBox"))
        {
            int manaAmount = 5;
            GameManager.singleton.playerStats.mana.AddResource(manaAmount);
            Debug.Log("Mana Recieved");
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
        GameManager.singleton.playerStats.health.SubtractResource(dmgAmount);

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
