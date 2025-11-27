using System;
using System.Collections;
using UnityEngine;
using static PlayerAnimator;

public class PlayerCharacter : CharacterBase
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private GameObject _mesh;

    [Header("Player Components")]
    [SerializeField] private PlayerInputHandler inputHandler;
    [SerializeField] public MouseTracker _mouseTracker;
    [SerializeField] private PlayerAnimator _animator;
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private PlayerGearHandler _gear;
    [SerializeField] private PlayerCombat _combat;

    [SerializeField] private float rotationSpeed = 15.0f;
    [SerializeField] private Vector3 mousePosition;
    [SerializeField] private Transform rotateTarget;
    [SerializeField] private Quaternion lookRotation;

    [SerializeField] bool inCombat;

    public Vector3 mouseWorldPos;

    public Action[] useAbilityEvents = new Action[3];

    private Coroutine[] cooldownCoroutines;

    protected override void Awake()
    {
        base.Awake();

        inputHandler = GetComponent<PlayerInputHandler>();

        if (_mouseTracker == null)
        {
            FindAnyObjectByType<MouseTracker>();
        }

        _animator = GetComponentInChildren<PlayerAnimator>();
        _movement = GetComponent<PlayerMovement>();
        _gear = GetComponent<PlayerGearHandler>();
        _combat = GetComponent<PlayerCombat>();
    }

    private void Start()
    {
        _combat.SheatheWeapon += CombatState;
        _combat.UnsheatheWeapon += CombatState;
        GameManager.singleton.hitstopManager.HitStop += ApplyHitStop;

        cooldownCoroutines = new Coroutine[abilities.Length];
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

        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    abilities[0].Use(UnityEngine.Random.Range(0, 999), transform, characterType.ToString(), 15);
        //    useAbilityQ?.Invoke();
        //}
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    abilities[1].Use(UnityEngine.Random.Range(0, 999), _mouseTracker.transform, characterType.ToString(), 15);
        //    useAbilityE?.Invoke();
        //}
    }

    private void ApplyHitStop()
    {
        StartCoroutine(_animator.FreezeCurrentAnim(1.0f));
    }

    public void CombatState()
    {
        if (_gear == null || _gear.weaponEquipped == null)
            return;

        if (_combat.inCombat)
        {
            _gear.UnsheatheWeapon();
            _animator.SetCombatState(true);
        }
        else if(!_combat.inCombat)
        {
            _gear.SheatheWeapon();
            _animator.SetCombatState(false);
        }
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
            if(!_combat.isAttacking)
            {
                _combat.StandardAttack(_gear.weaponEquipped, _mouseTracker.mouseAim, characterType.ToString());
                if (_gear.weaponEquipped.GetClass() == "Warrior" || _gear.weaponEquipped.GetClass() == "Any")
                {
                    _animator.SetAnimationState(AnimationStates.AttackMelee);
                }
                else
                {
                    _animator.SetAnimationState(AnimationStates.AttackMage);
                }
            }
        }
    }

    public void TakeDamage(int attackID, int dmg)
    {
        Resource health = GameManager.singleton.playerStats.health;
        if (!processedAttackIDs.Contains(attackID))
        {
            if (health.currentValue - dmg <= 0)
            {
                
                //Destroy(gameObject);
            }

            SpawnDmgNumber(dmg, Color.yellow);
            health.SubtractResource(dmg);
        }
    }

    public void AbilityInput(int index)
    {
        var ability = abilities[index];
        if (ability == null) return;

        if (ability.currentCooldown > 0) return;

        Transform t = ability.ability.mousePosAim ? _mouseTracker.transform : transform;
        ability.ability.Use(UnityEngine.Random.Range(0, 999), t, characterType.ToString(), 15);

        ability.currentCooldown = ability.ability.GetCooldown();

        if (cooldownCoroutines[index] == null)
            cooldownCoroutines[index] = StartCoroutine(AbilityCooldown(index, ability));

        useAbilityEvents[index]?.Invoke();
    }

    private IEnumerator AbilityCooldown(int index, AbilityClass ability)
    {
        float cd = ability.ability.GetCooldown();
        ability.currentCooldown = cd;

        while (ability.currentCooldown > 0)
        {
            ability.currentCooldown -= Time.deltaTime;
            yield return null;
        }

        ability.currentCooldown = 0;

        cooldownCoroutines[index] = null;
    }
}
