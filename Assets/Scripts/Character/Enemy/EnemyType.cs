using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyType : CharacterBase
{
    public WeaponItem enemyWeapon;
    private WeaponObject enemyWeaponEquipped;
    private Transform weaponHandSocket;
    private GameObject weaponInstance;

    public bool spawnAttack;

    [SerializeField] private CharacterAnimator_FlagHandler flagHandler;

    public CharacterAbility[] enemyAbilities;

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

    protected override void Awake()
    {
        base.Awake();
        _rb = GetComponent<Rigidbody>();
        lootPool = GetComponent<LootPool>();
        flagHandler = GetComponentInChildren<CharacterAnimator_FlagHandler>();
        flagHandler.OnSpawnAttack += SpawnHitbox;
        flagHandler.OnDespawnAttack += DespawnHitbox;

        foreach (var t in GetComponentsInChildren<Transform>(true))
        {
            if (t.name == "DEF-hand.socket.R")
                weaponHandSocket = t;
        }

        
    }

    private void Start()
    {
        health.SetCurrentValue(health.baseValue);

        if (enemyWeapon == null)
        {
            enemyWeapon = Resources.Load<WeaponItem>("Default_WeaponItem");
            enemyWeaponEquipped = enemyWeapon.GetWeaponObject();
            weaponInstance = Instantiate(enemyWeapon.gameObject, weaponHandSocket);
        }
        else
        {
            enemyWeaponEquipped = enemyWeapon.GetWeaponObject();
            weaponInstance = Instantiate(enemyWeapon.gameObject, weaponHandSocket);
        }
    }

    private void Update()
    {
        int currentHP = health.currentValue;
        int maxHP = health.baseValue;
        hpText.text = $"{currentHP} / {maxHP}";
        lvlText.text = $"Lv. {level}";
        nameText.text = $"{enemyName}";
        float healthCurrentPercent = (float)currentHP / maxHP;
        hpBar.value = healthCurrentPercent;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerCharacter player = collision.collider.GetComponent<PlayerCharacter>();
            player.TakeDamage(Random.Range(0, 9999), 1);
            _rb.AddForce((transform.position - collision.transform.position) * 2.5f, ForceMode.Impulse);
        }
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
            if (health.currentValue - dmg <= 0)
            {
                //Quaternion effRot = new Vector3 (0, 0, 0);
                ParticleSystem deathEffClone = Instantiate(deathEffect, transform.position, deathEffect.transform.rotation);
                deathEffClone.Play();
                GameManager.singleton.playerLevel.AddEXP(expToGive);
                lootPool.GetRandomDrop();
                lootPool.GetCurrencyDropAmount();
                Destroy(gameObject);
            }

            SpawnDmgNumber(dmg, Color.red);
            health.SubtractResource(dmg);
            TakeKnockback(obj, knockback);
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
}
