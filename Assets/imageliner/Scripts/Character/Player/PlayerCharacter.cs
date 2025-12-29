using System;
using System.Collections;
using UnityEngine;
using static PlayerAnimator;

public class PlayerCharacter : CharacterBase
{
    [SerializeField] private Rigidbody _rb;

    [Header("Player Components")]
    [SerializeField] private PlayerInputHandler inputHandler;
    [SerializeField] public MouseTracker _mouseTracker;
    [SerializeField] private PlayerAnimator _animator;
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private PlayerGearHandler _gear;
    [SerializeField] private PlayerCombat _combat;

    public Vector3 moveDirAim;

    [SerializeField] private float rotationSpeed = 15.0f;
    [SerializeField] private Vector3 mousePosition;
    [SerializeField] private Transform rotateTarget;
    [SerializeField] private Quaternion lookRotation;

    [SerializeField] bool inCombat;

    public Vector3 mouseWorldPos;

    [SerializeField] private AbilityClass dashAbility;

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
        _mouseTracker = FindAnyObjectByType<MouseTracker>();
        rotateTarget = _mouseTracker.transform;
    }

    private void Start()
    {
        _combat.SheatheWeapon += CombatState;
        _combat.UnsheatheWeapon += CombatState;
        _combat.AttackStart += () => _movement.MovespeedReduce();
        _combat.AttackEnd += () => _movement.MovespeedNormal();

        GameManager.singleton.hitstopManager.HitStop += ApplyHitStop;
        GameManager.singleton.hitstopManager.HitStop += ()=> _combat.SetCombo(true);

        cooldownCoroutines = new Coroutine[abilities.Length];

        GameManager.singleton.RegisterPlayer(this);
    }

    private void OnDestroy()
    {
        if (GameManager.singleton != null &&
            GameManager.singleton.hitstopManager != null)
        {
            GameManager.singleton.hitstopManager.HitStop -= ApplyHitStop;
        }
    }

    private void OnDisable()
    {
        if (GameManager.singleton != null &&
            GameManager.singleton.hitstopManager != null)
        {
            GameManager.singleton.hitstopManager.HitStop -= ApplyHitStop;
        }
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

    private void ApplyHitStop()
    {
        if (!this) return; // destroyed object safety guard
        StartCoroutine(_animator.FreezeCurrentAnim(0.5f));
        //StartCoroutine(FreezeAllParticles(1f));
    }

    public void SetComboLimit(int comboLimit)
    {
        _combat.comboCountMax = comboLimit;
    }

    private IEnumerator FreezeAllParticles(float duration)
    {
        ParticleSystem[] allParticles = FindObjectsByType<ParticleSystem>(FindObjectsSortMode.None);

        foreach (ParticleSystem ps in allParticles)
        {
            if (ps.isPlaying)
            {
                ps.Pause();
            }
        }

        yield return new WaitForSeconds(duration);

        foreach (ParticleSystem ps in allParticles)
        {
            if (ps)
            {
                ps.Play();
            }
        }
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
            moveDirAim = transform.position + moveDir;

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
                _combat.ResetCombo();
                _combat.StandardAttack(_gear.currentWeapon, _mouseTracker.mouseAim, characterType.ToString());
                if (_gear.currentWeapon.GetGearObject().GetClass() == "Warrior")
                {
                    _animator.SetAnimationState(AnimationStates.AttackMelee);
                }
                else
                {
                    _animator.SetAnimationState(AnimationStates.AttackMage);
                }
            }
            else if (_combat.canCombo && _combat.comboCountMax > 1)
            {
                _combat.CancelAttack();

                _combat.StandardAttack(_gear.currentWeapon, _mouseTracker.mouseAim, characterType.ToString());
                if (_gear.currentWeapon.GetGearObject().GetClass() == "Warrior")
                {
                    if (_combat.comboCount == _combat.comboCountMax)
                        _animator.SetAnimationState(AnimationStates.FinisherAttack);
                    else
                        _animator.ComboAnimation();
                }
                else
                {
                    _animator.SetAnimationState(AnimationStates.AttackMage);
                }
            }
        }
    }

    public void Dash()
    {
        if (dashAbility != null)
        {
            if (GameManager.singleton.playerStats.stamina.currentValue == 0) //|| _combat.isAttacking
            {
                return;
            }

            GameObject temp = new GameObject("DashDirection");
            temp.transform.position = moveDirAim;
            
            temp.transform.rotation = Quaternion.LookRotation(moveDirAim);

            if (inputHandler.movementAxisValue == Vector2.zero)
            {
                temp.transform.position = transform.position + transform.forward * 10f;
                temp.transform.rotation = transform.rotation;
            }

            Transform t = _mouseTracker.transform;

            dashAbility.ability.Use(0, temp.transform, characterType.ToString(), 0, _rb);

            GameManager.singleton.playerStats.stamina.SubtractResource(1);
        }
    }

    public void TakeDamage(int attackID, int dmg, DamageType damageType)
    {
        Resource health = GameManager.singleton.playerStats.health;
        if (!processedAttackIDs.Contains(attackID))
        {
            int calculatedDmg;
            if (damageType == DamageType.Magic)
            {
                calculatedDmg = Mathf.RoundToInt(dmg - ((GameManager.singleton.playerStats.finalMDef) / 2f));

            }
            else
                calculatedDmg = Mathf.RoundToInt(dmg - ((GameManager.singleton.playerStats.finalPhysDef) / 2f));

            if (calculatedDmg <= 0)
                calculatedDmg = 0;


            Debug.Log(damageType.ToString() + "damage taken");

            SpawnDmgNumber(calculatedDmg, Color.white);
            health.SubtractResource(calculatedDmg);

            if (health.currentValue <= 0)
            {
                _animator.SetAnimationState(AnimationStates.Death);
                GameManager.singleton.GameFailed();
                inputHandler.enabled = false;
                this.enabled = false;
                return;
            }
        }

        CheckLowHP();
    }

    public void TakeHeal(int attackID, int amt)
    {
        Resource health = GameManager.singleton.playerStats.health;
        if (!processedAttackIDs.Contains(attackID))
        {
            SpawnDmgNumber(amt, Color.green);
            health.AddResource(amt);
        }
        CheckLowHP();
    }

    public void CheckLowHP()
    {
        float percent = (float)GameManager.singleton.playerStats.health.currentValue /
                        GameManager.singleton.playerStats.maxHealth;

        if (percent < 0.3f)
        {
            UIPlayerHUD.OnLowHP?.Invoke();
            SoundManager.singleton.SlowMusicPitch();
        }
        else
        {
            UIPlayerHUD.OnNormalHP?.Invoke();
            SoundManager.singleton.ReturnMusicPitch();
        }
            
    }

    public void UseMana(int amt)
    {
        Resource mana = GameManager.singleton.playerStats.mana;

        mana.SubtractResource(amt);
    }

    public void AbilityInput(int index)
    {
        var ability = abilities[index];
        if (ability == null) return;
        int manaCost = ability.ability.GetManaCost();

        if (ability.currentCooldown > 0 || GameManager.singleton.playerStats.mana.currentValue < manaCost || _combat.isAttacking)
        {
            SoundManager.singleton.PlayAudio(SoundManager.singleton.sfx_Deny);
            return;
        }  


        Transform t = ability.ability.mousePosAim ? _mouseTracker.transform : transform;

        int abilityDmg = _combat.GetDamageType(_gear.currentWeapon, ability.ability.GetDamageType());

        ability.ability.Use(UnityEngine.Random.Range(0, 999), t, characterType.ToString(), abilityDmg, _rb);

        _animator.SetAnimationState(AnimationStates.CastingSpell);

        ability.currentCooldown = ability.ability.GetCooldown();

        if (cooldownCoroutines[index] == null)
            cooldownCoroutines[index] = StartCoroutine(AbilityCooldown(index, ability));

        

        useAbilityEvents[index]?.Invoke();

        UseMana(manaCost);
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

    public void SwapAbility(int index, CharacterAbility newAbility)
    {
        AbilityClass newSlot = new AbilityClass();

        newSlot.ability = newAbility;

        newSlot.currentCooldown = 0;

        abilities[index] = newSlot;
    }


}
