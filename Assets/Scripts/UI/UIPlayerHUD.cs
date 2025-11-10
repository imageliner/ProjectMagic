using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerHUD : MonoBehaviour
{
    public Slider healthBarSlider;
    public TextMeshProUGUI healthBarText;
    public Slider staminaBarSlider;
    public TextMeshProUGUI staminaBarText;
    public Slider manaBarSlider;
    public TextMeshProUGUI manaBarText;

    public GameObject statsDebug;
    private bool isDebugView = false;

    private void Update()
    {
        // Wait until the player and manager exist
        if (GameManager.singleton == null || GameManager.singleton.playerStats == null)
            return;

        UpdateStats();

        if (Input.GetKeyDown(KeyCode.I))
        {
            isDebugView = !isDebugView;
            statsDebug?.SetActive(isDebugView);
        }
    }

    public void UpdateStats()
    {
        int currentHP = GameManager.singleton.playerStats.health.currentValue;
        int maxHP = GameManager.singleton.playerStats.maxHealth;
        //int currentStam = GameManager.singleton.playerStats.currentStamina;
        //int maxStam = GameManager.singleton.playerStats.maxStamina;
        int currentMana = GameManager.singleton.playerStats.mana.currentValue;
        int maxMana = GameManager.singleton.playerStats.maxMana;

        healthBarText.text = $"{currentHP} / {maxHP}";
        float healthCurrentPercent = (float)currentHP / maxHP;
        healthBarSlider.value = healthCurrentPercent;

        //staminaBarText.text = $"{currentStam} / {maxStam}";
        //float staminaCurrentPercent = (float)currentStam / maxStam;
        //staminaBarSlider.value = staminaCurrentPercent;

        //stamina cooldown timer
        //if (currentStam < maxStam)
        //{
        //    staminaRegenTimer += Time.deltaTime;
        //    if (staminaRegenTimer >= staminaRegenInterval)
        //    {
        //        staminaCurrent += 1;
        //        staminaRegenTimer = 0f;
        //    }
        //}
        //else
        //{
        //    staminaRegenTimer = 0f;
        //}

        manaBarText.text = $"{currentMana} / {maxMana}";
        float manaCurrentPercent = (float)currentMana / maxMana;
        manaBarSlider.value = manaCurrentPercent;


    }

}
