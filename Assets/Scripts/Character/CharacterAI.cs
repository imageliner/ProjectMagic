using System.Collections;
using UnityEngine;


public class CharacterAI : MonoBehaviour
{
    public EnemyType enemyType;
    [SerializeField] private NPCState currentState;

    [SerializeField] public bool canStrafe;
    [SerializeField] public bool isAggressive;

    private Coroutine movementRoutine;

    public Transform target;
    public bool isMoving;
    public bool targetInRange;
    public float detectionRange = 5;
    public float attackRange = 2;
    public float distanceToTarget;

    public float moveSpeed = 1f;
    public float rotationSpeed { get; private set; } = 6f;

    [SerializeField] private Vector3 moveDir;
    public Vector3 targetDir;
    [SerializeField] private float moveAmount;

    [System.Serializable]
    public struct MoveAmountRange
    {
        public int min;
        public int max;
    }
    [SerializeField] private MoveAmountRange moveAmountRange;

    [SerializeField] private LayerMask layerToTarget;

    private Rigidbody ownerRB;

    [SerializeField] private Animator _animator;

    private void Awake()
    {
        enemyType = gameObject.GetComponent<EnemyType>();
        ownerRB = gameObject.GetComponent<Rigidbody>();
        _animator = gameObject.GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        ChangeState(new NPCWanderingState(this));
    }

    public void ChangeState(NPCState newState)
    {
        if (currentState != null)
        {
            currentState.OnStateExit();
        }

        currentState = newState;
        currentState.OnStateEnter();

        Debug.Log(newState.ToString());
    }


    void Update()
    {

        if (currentState != null)
        {
            currentState.OnStateRun();
        }

        if (!isMoving)
        {
            if (_animator == null)
                return;

            _animator.SetFloat("Horizontal", 0);
            _animator.SetFloat("Vertical", 0);
        }

        Collider[] hitsDetection = Physics.OverlapSphere(transform.position, detectionRange, layerToTarget);
        if (hitsDetection.Length > 0)
        {
            target = hitsDetection[0].transform;
            targetInRange = true;
            targetDir = (target.position - transform.position).normalized;

            distanceToTarget = Vector3.Distance(transform.position, target.position);

            if (distanceToTarget <= attackRange)
            {
                if (!(currentState is NPCInCombat))
                    ChangeState(new NPCInCombat(this));
            }
            else if (!(currentState is NPCChaseState))
            {
                ChangeState(new NPCChaseState(this));
            }
        }
        else
        {
            targetInRange = false;
        }
    }

    public void PlayAttackAnim()
    {
        if (_animator == null)
            return;

        _animator.SetTrigger("startAttack");
    }

    public bool IsMoving()
    {
        return isMoving;
    }

    public void StartMovement(IEnumerator routine)
    {
        if (movementRoutine != null)
            StopCoroutine(movementRoutine);

        movementRoutine = StartCoroutine(routine);
    }

    public void StopMovement()
    {
        if (movementRoutine != null)
            StopCoroutine(movementRoutine);

        movementRoutine = null;
        isMoving = false;
    }

    public void WatchTarget()
    {
        Vector3 toTarget = (target.position - transform.position).normalized;

        Quaternion lookRot = Quaternion.LookRotation(toTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, rotationSpeed * Time.deltaTime);
    }

    private void AnimatorWalkBlend()
    {
        if (_animator == null)
            return;

        Vector3 localMove = transform.InverseTransformDirection(moveDir);

        _animator.SetFloat("Horizontal", localMove.x, 0.1f, Time.deltaTime);
        _animator.SetFloat("Vertical", localMove.z, 0.1f, Time.deltaTime);
    }

    public IEnumerator Wander()
    {
        moveDir = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)).normalized;
        moveAmount = Random.Range(moveAmountRange.min, moveAmountRange.max);
        isMoving = true;

        

        float timer = moveAmount;
        while (timer > 0f)
        {
            transform.position += moveDir * Time.deltaTime * moveSpeed;

            AnimatorWalkBlend();

            if (moveDir.sqrMagnitude > 0.001f)    // prevent LookRotation errors
            {
                Quaternion targetRot = Quaternion.LookRotation(moveDir);
                targetRot.x = 0;
                targetRot.z = 0;
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
            }
            timer -= Time.deltaTime;
            yield return null;
        }

        isMoving = false;
    }

    public IEnumerator Chase()
    {
        isMoving = true;
        while (targetInRange)
        {
            transform.position += targetDir * Time.deltaTime * moveSpeed;

            AnimatorWalkBlend();

            if (targetDir.sqrMagnitude > 0.001f)
            {
                Quaternion targetRot = Quaternion.LookRotation(targetDir);
                targetRot.x = 0;
                targetRot.z = 0;
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
            }
            yield return null;
        }
        isMoving = false;

    }

    public IEnumerator Strafe()
    {
        isMoving = true;

        

        // pick left (-1) or right (+1) randomly
        int side = Random.value < 0.5f ? -1 : 1;

        while (targetInRange)
        {
            if (target == null) break;

            Vector3 toTarget = (target.position - transform.position).normalized;

            // perpendicular strafing direction
            Vector3 strafeDir = Vector3.Cross(toTarget, Vector3.up) * side;
            moveDir = strafeDir;

            transform.position += strafeDir * Time.deltaTime * moveSpeed;

            Quaternion lookRot = Quaternion.LookRotation(toTarget);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, rotationSpeed * Time.deltaTime);

            AnimatorWalkBlend();

            yield return null;
        }

        isMoving = false;
    }

    public IEnumerator Attack1()
    {
        moveDir = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10)).normalized;

        ownerRB.AddForce(transform.forward * 5, ForceMode.Impulse);
        yield return new WaitForSeconds(2);
    }

    public IEnumerator Attack(WeaponObject weapon, Vector3 direction, int attackID, string enemyAtk)
    {
        PlayAttackAnim();
        enemyType.spawnAttack = false;

        yield return new WaitUntil(() => enemyType.spawnAttack == true);
        ownerRB.AddForce(transform.forward * 3, ForceMode.Impulse);

        weapon.attackAbility.Use(attackID, transform, enemyAtk, enemyType.GetDamage());
        enemyType.spawnAttack = false;

        

        yield return new WaitForSeconds(1f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
