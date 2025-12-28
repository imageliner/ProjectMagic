using System;
using System.Collections;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private PlayerGearHandler _gear;
    public bool isAttacking;
    public bool spawnAttack;
    public bool inCombat;
    [SerializeField] private float combatToIdleTimer = 6.0f;
    [SerializeField] private float combatTimer;
    private float attackDashPower = 5f;
    private float attackDashTime = 0.15f;
    private float attackCoolDown = 0.5f;

    public bool canCombo;
    [SerializeField] private int comboCountMax;
    [SerializeField] private int comboCount;


    private int currentAttackID = 0;

    [SerializeField] private GameObject testAttackBox;

    public Action SheatheWeapon;
    public Action UnsheatheWeapon;

    public Action AttackStart;
    public Action AttackEnd;

    private Coroutine attackCoroutine;

    [SerializeField] private CharacterAnimator_FlagHandler flagHandler;

    public float AdjustedAttackSpeed()
    {
        return 1 + GameManager.singleton.playerStats.finalAtkSpeed;
    }

    private void Awake()
    {
        flagHandler = GetComponentInChildren<CharacterAnimator_FlagHandler>();
        _gear = GetComponent<PlayerGearHandler>();
        flagHandler.OnSpawnAttack += SpawnHitbox;
        flagHandler.OnDespawnAttack += DespawnHitbox;

        flagHandler.OnSpawnEffect += SpawnEffect;

        flagHandler.CanCombo += CheckCombo;
    }

    private void Update()
    {
        if (combatTimer > 0)
        {
            combatTimer -= Time.deltaTime;
        }
        if (combatTimer <= 0)
        {
            combatTimer = 0;
            SheatheWeapon?.Invoke();
            inCombat = false;
        } 
    }

    public void StandardAttack(GearItem weapon, Vector3 attackDir, string playerAtk)
    {
        if (!isAttacking || canCombo)
        {
            comboCount++;
            canCombo = false;
            inCombat = true;
            UnsheatheWeapon?.Invoke();

            combatTimer = combatToIdleTimer;

            currentAttackID++;
            attackCoroutine = StartCoroutine(Attack(weapon, attackDir.normalized, currentAttackID, playerAtk));
        }
    }

    public int GetDamageType(GearItem weapon, DamageType abilityDamageType)
    {
        if (abilityDamageType == DamageType.Magic || weapon.GetGearObject().GetGearType() == "Mage")
        {
            return GameManager.singleton.playerStats.statCalcs.CalculateMagAtkDmg(GameManager.singleton.playerStats.finalMAtk);
        }
        else
            return GameManager.singleton.playerStats.statCalcs.CalculatePhysAtkDmg(GameManager.singleton.playerStats.finalPhysAtk);
    }

    private IEnumerator Attack(GearItem weapon, Vector3 direction, int attackID, string playerAtk)
    {
        isAttacking = true;
        AttackStart?.Invoke();

        spawnAttack = false;

        yield return new WaitUntil(() => spawnAttack == true);

        int playerDamage = GetDamageType(weapon, weapon.GetGearObject().attackAbility.GetDamageType());

        weapon.GetGearObject().attackAbility.Use(attackID, transform, playerAtk, playerDamage, null);
        spawnAttack = false;

        float elapsed = 0f;
        while (elapsed < attackDashTime)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            lookRotation.x = 0;
            lookRotation.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 50);
            transform.position += direction * attackDashPower * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(attackCoolDown);

        isAttacking = false;
        AttackEnd?.Invoke();

        canCombo = false;
        comboCount = 0;
    }

    public void CancelAttack()
    {
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }
        isAttacking = false;
        spawnAttack = false;

        AttackEnd?.Invoke();
    }

    public void SpawnHitbox()
    {
        spawnAttack = true;
    }

    public void DespawnHitbox()
    {
        spawnAttack = false;
    }

    public void CheckCombo()
    {
        if (comboCount < comboCountMax)
        {
            canCombo = true;
        }
        else
        {
            canCombo = false;
            comboCount = 0;
        }
    }

    public void SpawnEffect()
    {
        if (_gear.currentWeapon.swingEff != null)
        {
            ParticleSystem cloneEff = _gear.currentWeapon.swingEff;
            Vector3 spawnPosition = transform.position + (transform.forward * 0.5f) + (transform.up * 1f);
            Quaternion rotOffset = transform.rotation * Quaternion.Euler(270f, 0f, 70f);
            Instantiate(cloneEff, spawnPosition, rotOffset, null);
            cloneEff.Play();
        }
    }
}
