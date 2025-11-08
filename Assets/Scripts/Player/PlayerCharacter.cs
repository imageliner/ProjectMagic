using UnityEngine;
using static PlayerAnimator;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private GameObject _mesh;

    [Header("Player Components")]
    [SerializeField] private PlayerInputHandler inputHandler;
    [SerializeField] public MouseTracker _mouseTracker;
    [SerializeField] private PlayerAnimator _animator;
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private PlayerCombat _combat;

    [SerializeField] private float rotationSpeed = 15.0f;
    [SerializeField] private Vector3 mousePosition;
    [SerializeField] private Transform rotateTarget;
    [SerializeField] private Quaternion lookRotation;

    public Vector3 mouseWorldPos;

    private void Awake()
    {
        inputHandler = GetComponent<PlayerInputHandler>();

        if (_mouseTracker == null)
        {
            FindAnyObjectByType<MouseTracker>();
        }

        _animator = GetComponent<PlayerAnimator>();
        _movement = GetComponent<PlayerMovement>();
        _combat = GetComponent<PlayerCombat>();
    }

    private void Update()
    {
        

        mouseWorldPos = _mouseTracker.mouseWorldPosition;

        mousePosition = (rotateTarget.position - transform.position).normalized;
        lookRotation = Quaternion.LookRotation(mousePosition);
        lookRotation.x = 0;
        lookRotation.z = 0;
        if (_movement.isMoving)
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }



    public void Movement(Vector2 inputAxis)
    {
        if (_movement != null)
        {
            
            Vector3 moveDir = new Vector3(inputAxis.x, 0f, inputAxis.y);
            Vector3 localMove = transform.InverseTransformDirection(moveDir.normalized);

            _movement.Movement(inputAxis, moveDir);

            _animator.SetAnimationState(AnimationStates.MoveBlend, localMove);
        }
    }

    public void Attack()
    {
        if (_combat != null)
        {
            _combat.StandardAttack(_mouseTracker.mouseAim);
            _animator.SetAnimationState(AnimationStates.Attack);
        }
    }
}
