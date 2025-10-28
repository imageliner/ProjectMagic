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

    private void Update()
    {
        UpdateStats();
    }

    public void UpdateStats()
    {
        int currentHP = GameManager.singleton.playerStats2.health.currentValue;
        int maxHP = GameManager.singleton.playerStats2.maxHealth;
        int currentStam = GameManager.singleton.playerStats.currentStamina;
        int maxStam = GameManager.singleton.playerStats.maxStamina;
        int currentMana = GameManager.singleton.playerStats.currentMana;
        int maxMana = GameManager.singleton.playerStats.maxMana;

        healthBarText.text = $"{currentHP} / {maxHP}";
        float healthCurrentPercent = (float)currentHP / maxHP;
        healthBarSlider.value = healthCurrentPercent;

        staminaBarText.text = $"{currentStam} / {maxStam}";
        float staminaCurrentPercent = (float)currentStam / maxStam;
        staminaBarSlider.value = staminaCurrentPercent;

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
