using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyType : CharacterBase
{
    [SerializeField] private string enemyName;
    [SerializeField] private Resource health;
    [SerializeField] private int level;
    [SerializeField] private int damage;
    [SerializeField] private int defence;
    [SerializeField] private int expToGive;

    [SerializeField] private Slider hpBar;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private UIDamageNumber dmgNumberTest;
    [SerializeField] private TextMeshProUGUI lvlText;
    [SerializeField] private TextMeshProUGUI nameText;

    [SerializeField] private Rigidbody _rb;


    protected override void Awake()
    {
        base.Awake();
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        health.SetCurrentValue(health.baseValue);

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
            _rb.AddForce((transform.position - collision.transform.position) * 5, ForceMode.Impulse);
        }
    }

    public void TakeDamage(int attackID, int dmg, GameObject obj)
    {
        if (!processedAttackIDs.Contains(attackID))
        {
            if (health.currentValue - dmg <= 0)
            {
                GameManager.singleton.playerLevel.AddEXP(expToGive);
                Destroy(gameObject);
            }

            SpawnDmgNumber(dmg, Color.red);
            health.SubtractResource(dmg);
            _rb.AddForce((transform.position - obj.transform.position) * 5, ForceMode.Impulse);
        }
    }
}
