using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class EnemyType : CharacterBase
{
    public bool useAbilities = true;
    [SerializeField] private bool isMiniBoss = false;
    public GearItem enemyWeapon;
    private GearObject enemyWeaponEquipped;
    private Transform weaponHandSocket;
    private GameObject weaponInstance;

    public bool spawnAttack;

    private bool dashing;

    [SerializeField] private CharacterAnimator_FlagHandler flagHandler;

    [SerializeField] private string enemyName;
    [SerializeField] private Resource health;
    [SerializeField] private int level;
    [SerializeField] private int baseDamage;
    [SerializeField] private int defence;
    [SerializeField] private int expToGive;

    [SerializeField] private Slider hpBar;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private UIDamageNumber dmgNumberTest;
    [SerializeField] private TextMeshProUGUI lvlText;
    [SerializeField] private TextMeshProUGUI nameText;

    [SerializeField] private Rigidbody _rb;

    [SerializeField] LootPool lootPool;

    [SerializeField] private ParticleSystem deathEffect;

    private EnemyGroupManager groupManager;

    protected override void Awake()
    {
        base.Awake();
        _rb = GetComponent<Rigidbody>();
        lootPool = GetComponent<LootPool>();
        flagHandler = GetComponentInChildren<CharacterAnimator_FlagHandler>();
        if (flagHandler != null)
        {
            flagHandler.OnSpawnAttack += SpawnHitbox;
            flagHandler.OnDespawnAttack += DespawnHitbox;
        }

        foreach (var t in GetComponentsInChildren<Transform>(true))
        {
            if (t.name == "DEF-hand.socket.R")
                weaponHandSocket = t;
        }

        
    }

    private void Start()
    {
        health.SetCurrentValue(health.baseValue);

        if (weaponHandSocket != null)
        {
            if (enemyWeapon == null)
            {
                enemyWeapon = Resources.Load<GearItem>("Default_WeaponItem");
                enemyWeaponEquipped = enemyWeapon.GetGearObject();
                weaponInstance = Instantiate(enemyWeapon.gameObject, weaponHandSocket);
            }
            else
            {
                enemyWeaponEquipped = enemyWeapon.GetGearObject();
                weaponInstance = Instantiate(enemyWeapon.gameObject, weaponHandSocket);
            }
        }
        else
        {
            if (enemyWeapon == null)
            {
                enemyWeapon = Resources.Load<GearItem>("Default_WeaponItem");
                enemyWeaponEquipped = enemyWeapon.GetGearObject();
            }
            else
            {
                enemyWeaponEquipped = enemyWeapon.GetGearObject();
            }
        }
        
    }

    private void Update()
    {
        int currentHP = health.currentValue;
        int maxHP = health.baseValue;
        hpText.text = $"{currentHP} / {maxHP}";
        lvlText.text = $"Lv. {level}";

        if (isMiniBoss)
            nameText.text = "Boss - " + $"{enemyName}";

        if (!isMiniBoss)
            nameText.text = $"{enemyName}";

        float healthCurrentPercent = (float)currentHP / maxHP;
        hpBar.value = healthCurrentPercent;
    }

    public void InitializeHealthAfterStats()
    {
        health.SetCurrentValue(health.baseValue);
    }

    public void SetGroupOwner(EnemyGroupManager owner)
    {
        groupManager = owner;
    }

    public void SetValues(int newLevel, bool miniboss)
    {
        isMiniBoss = miniboss;
        int randomLevel = Random.Range(newLevel - 2, newLevel + 2);
        level = Mathf.Clamp(randomLevel, 1, 9999);

        SetStats();
    }

    private void SetStats()
    {
        health.SetBaseValue(health.baseValue + (level * 6));
        baseDamage += level * 2;

        int newDefence = Mathf.RoundToInt(level * 1.2f);
        defence += newDefence;

        int newEXP = Mathf.RoundToInt(level * 1.5f);
        expToGive += newEXP;

        if (isMiniBoss)
        {
            health.SetBaseValue(health.baseValue + level * 10);
            baseDamage += level * 3;
            int newBossDefence = Mathf.RoundToInt(level * 1.5f);
            defence += newBossDefence;
            int newBossEXP = Mathf.RoundToInt(level * 1.5f);
            expToGive += newBossEXP;
            gameObject.transform.localScale = Vector3.one * 2f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.collider.CompareTag("Player"))
        //{
        //    PlayerCharacter player = collision.collider.GetComponent<PlayerCharacter>();
        //    player.TakeDamage(Random.Range(0, 9999), 1);
        //    _rb.AddForce((transform.position - collision.transform.position) * 2.5f, ForceMode.Impulse);
        //}
    }

    public int GetDamage()
    {
        int lowAtk = Mathf.CeilToInt(baseDamage - (baseDamage / 2));
        int highAtk = Mathf.CeilToInt(baseDamage + (baseDamage / 2));

        return Mathf.Max(0, Random.Range(lowAtk - 1, highAtk + 1));
    }

    public void TakeDamage(int attackID, int dmg, GameObject obj, float knockback)
    {
        if (!processedAttackIDs.Contains(attackID))
        {
            int calculatedDmg = Mathf.RoundToInt(dmg - (defence / 1.5f));
            if (calculatedDmg <= 0)
                calculatedDmg = 0;

            health.SubtractResource(dmg);
            SpawnDmgNumber(dmg, Color.red);
            TakeKnockback(obj, knockback);

            if (health.currentValue <= 0)
            {
                OnDeath();
            }
        }
    }

    private void OnDeath()
    {
        Vector3 deathPos = transform.position;

        EnemyGroupManager managerRef = groupManager;
        bool wasMiniboss = isMiniBoss;

        if (groupManager != null)
        {
            if (isMiniBoss)
            {
                managerRef?.MinibossDead(deathPos);
            }
            else
            {
                managerRef?.RemoveFromList(this, deathPos);
            }
        }

        ParticleSystem deathEffClone = Instantiate(deathEffect, transform.position, deathEffect.transform.rotation);
        deathEffClone.Play();
        GameManager.singleton.playerLevel.AddEXP(expToGive);
        lootPool.GetRandomDrop();
        lootPool.GetCurrencyDropAmount();

        Destroy(gameObject);
    }

    public void TakeHeal(int attackID, int amt)
    {
        if (!processedAttackIDs.Contains(attackID))
        {

            SpawnDmgNumber(amt, Color.green);
            health.AddResource(amt);
        }
    }

    private void TakeKnockback(GameObject obj, float amount)
    {
        _rb.AddForce((transform.position - obj.transform.position) * amount, ForceMode.Impulse);
    }

    public void SpawnHitbox()
    {
        spawnAttack = true;
    }
    public void DespawnHitbox()
    {
        spawnAttack = false;
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
        Destroy(cloneDashTrail, 0.15f);

        rb.linearVelocity = Vector3.zero;
    }
}
