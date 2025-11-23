using System.Collections;
using UnityEngine;


public class CharacterAI : MonoBehaviour
{
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

    public float moveSpeed { get; private set; } = 1f;
    public float rotationSpeed { get; private set; } = 6f;

    [SerializeField] private Vector3 moveDir;
    public Vector3 targetDir;
    [SerializeField] private float moveAmount;
    private float moveTimer;

    [SerializeField] private LayerMask layer;

    private Rigidbody ownerRB;

    private void Awake()
    {
        ownerRB = gameObject.GetComponent<Rigidbody>();
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

        Collider[] hitsDetection = Physics.OverlapSphere(transform.position, detectionRange, layer);
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

    public IEnumerator Wander()
    {
        moveDir = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10)).normalized;
        moveAmount = Random.Range(2, 8);
        isMoving = true;
        float timer = moveAmount;
        while (timer > 0f)
        {
            transform.position += moveDir * Time.deltaTime * moveSpeed;

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
        while (targetInRange)
        {
            transform.position += targetDir * Time.deltaTime * moveSpeed;

            if (targetDir.sqrMagnitude > 0.001f)
            {
                Quaternion targetRot = Quaternion.LookRotation(targetDir);
                targetRot.x = 0;
                targetRot.z = 0;
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
            }
            yield return null;
        }

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

            transform.position += strafeDir * Time.deltaTime * moveSpeed;

            Quaternion lookRot = Quaternion.LookRotation(toTarget);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, rotationSpeed * Time.deltaTime);

            yield return null;
        }

        isMoving = false;
    }

    public IEnumerator Attack()
    {
        moveDir = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10)).normalized;

        ownerRB.AddForce(transform.forward * 5, ForceMode.Impulse);
        yield return new WaitForSeconds(2);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
