using System.Collections.Generic;
using TMPro;
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


    protected override void Awake()
    {
        base.Awake();
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


   

    public void TakeDamage(int attackID, int dmg)
    {
        if (!processedAttackIDs.Contains(attackID))
        {
            if (health.currentValue - dmg <= 0)
            {
                GameManager.singleton.playerLevel.AddEXP(expToGive);
                Destroy(gameObject);
            }

            SpawnDmgNumber(dmg);
            health.SubtractResource(dmg);
        }
    }
}
