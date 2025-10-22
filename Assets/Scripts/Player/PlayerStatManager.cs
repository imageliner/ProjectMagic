using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatManager : MonoBehaviour
{

    [SerializeField] public BaseStatData playerBaseStats;
    private PlayerController playerController;
    public Slider healthBarSlider;
    public TextMeshProUGUI healthBarText;
    public Slider staminaBarSlider;
    public TextMeshProUGUI staminaBarText;
    public Slider manaBarSlider;
    public TextMeshProUGUI manaBarText;

    [Header("Main Stats")]
    public int healthCurrent;
    private int healthMax;
    public int staminaCurrent;
    private int staminaMax;
    public int manaCurrent;
    private int manaMax;

    private float staminaRegenTimer = 0f;
    public float staminaRegenInterval = 0.25f;

    [Header("Defining Stats")]
    private int strengthCurrent;
    private int dexterityCurrent;
    private int intelligenceCurrent;
    private int luckCurrent;

    //[Header("Extra Stats")]


    void Start()
    {
        playerController = GetComponent<PlayerController>();
        InitiateStats();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStats(); 
    }

    public void InitiateStats()
    {
        //health
        healthMax = playerBaseStats.healthBase;
        healthCurrent = healthMax;
        healthBarSlider.value = 1.0f;

        //stamina
        staminaMax = playerBaseStats.staminaBase;
        staminaCurrent = staminaMax;
        staminaBarSlider.value = 1.0f;

        //mana
        manaMax = playerBaseStats.manaBase;
        manaCurrent = manaMax;
        manaBarSlider.value = 1.0f;
    }

    public void UpdateStats()
    {
        healthBarText.text = $"{healthCurrent} / {healthMax}"; //(healthCurrent, "/", healthMax).ToString();
        float healthCurrentPercent = (float)healthCurrent / healthMax;
        healthBarSlider.value = healthCurrentPercent;

        staminaBarText.text = $"{staminaCurrent} / {staminaMax}";
        float staminaCurrentPercent = (float)staminaCurrent / staminaMax;
        staminaBarSlider.value = staminaCurrentPercent;


        if (staminaCurrent < staminaMax)
        {
            staminaRegenTimer += Time.deltaTime;
            if (staminaRegenTimer >= staminaRegenInterval)
            {
                staminaCurrent += 1;
                staminaRegenTimer = 0f;
            }
        }
        else
        {
        staminaRegenTimer = 0f;
        }

        manaBarText.text = $"{manaCurrent} / {manaMax}";
        float manaCurrentPercent = (float)manaCurrent / manaMax;
        manaBarSlider.value = manaCurrentPercent;
    }

    public void TakeDamageCalculation(int dmgAmount)
    {
        healthCurrent -= dmgAmount;
    }

    public void TakeHealCalculation(int healAmount)
    {
        if (healthCurrent <= healthMax)
        {
            healthCurrent += healAmount;
        }
        if (healAmount + healthCurrent > healthMax)
        {
            healthCurrent = healthMax;
        }
    }

}
