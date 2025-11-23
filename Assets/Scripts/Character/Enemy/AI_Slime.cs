using UnityEngine;
using static UnityEngine.UI.Image;

public class AI_Slime : MonoBehaviour
{
    [SerializeField] private float detectionRange;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private Vector3 moveDir;
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
        moveTimer = 0;
    }

    private void Update()
    {

        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRange, layer);
        foreach (Collider hit in hits)
        {
            moveDir = (hit.transform.position - transform.position).normalized;
        }

        if (moveTimer > 0)
        {
            transform.position += moveDir * Time.deltaTime * moveSpeed;
            moveTimer -= Time.deltaTime;
        }
        if (moveTimer <= 0)
        {
            GetMoveDir();
            moveTimer = GetMoveAmount();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

    private float GetMoveAmount()
    {
        return Random.Range(2, 8);
    }
    private void GetMoveDir()
    {
        moveDir = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10)).normalized;
    }
}
