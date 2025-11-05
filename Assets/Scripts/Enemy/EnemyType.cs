using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyType : MonoBehaviour
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHitbox"))
        {
            int tempDmgVar = GameManager.singleton.playerStats2.statCalcs.CalculatePhysAtkDmg(GameManager.singleton.playerStats2.finalPhysAtk);
            if(health.currentValue - tempDmgVar <= 0)
            {
                GameManager.singleton.playerLevel.AddEXP(expToGive);
                Destroy(gameObject);
            }
            else
            {
                health.SubtractResource(tempDmgVar);
                Destroy(Instantiate(dmgNumberTest.gameObject, transform), 0.3f);
                dmgNumberTest.SetDamageNumber(tempDmgVar);
                Debug.Log("took damage");
            }
        }
    }
}
