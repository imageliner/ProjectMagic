using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour
{
    public TextMeshProUGUI currentLevelText;
    public TextMeshProUGUI currentEXPText;
    public TextMeshProUGUI EXPToLevelText;
    public Slider expBarSlider;

    public int currentEXP { get; protected set; } = 0;
    public int currentLevel { get; protected set; } = 1;
    public bool AtLevelCap { get; protected set; } = false;
    public int EXPRequiredForNextLevel => GetEXPRequiredForNextLevel();

    [SerializeField] UnityEvent<int, int> OnLevelChanged = new UnityEvent<int, int>();

    [SerializeField] private int maxLevel = 20;

    private void Start()
    {
        OnLevelChanged.Invoke(0, currentLevel);
    }

    public void AddEXP(int amount)
    {
        currentEXP += amount;
        if (currentEXP >= EXPRequiredForNextLevel)
        {
            currentEXP = 0;
            SetLevel();
        }
    }

    public void SetLevel()
    {
        int newLevel = currentLevel + 1;
        if (newLevel >= maxLevel)
        {
            newLevel = maxLevel;
            AtLevelCap = true;
        }
        OnLevelChanged.Invoke(currentLevel, newLevel);
        currentLevel = newLevel;
        currentEXP = 0;
    }

    private int GetEXPRequiredForNextLevel()
    {
        return Mathf.CeilToInt(100 + (currentLevel * 250));
    }

    private void Update()
    {
        RefreshDisplays();
    }

    private void RefreshDisplays()
    {
        float expCurrentPercent = (float)currentEXP / EXPRequiredForNextLevel;
        expBarSlider.value = expCurrentPercent;
        currentLevelText.text = $"Lvl: {currentLevel}";
        currentEXPText.text = $"Current EXP: {currentEXP}";
        if (!AtLevelCap)
            EXPToLevelText.text = $"EXP to next Level: {EXPRequiredForNextLevel}";
        else
            EXPToLevelText.text = $"At max";
    }
}
