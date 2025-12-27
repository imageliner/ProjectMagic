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

    [SerializeField] private int maxLevel = 10;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        OnLevelChanged.Invoke(0, currentLevel);
    }

    public void ResetLevel()
    {
        OnLevelChanged.Invoke(currentLevel, 1);
        currentLevel = 1;
        currentEXP = 0;
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
        Resource health = GameManager.singleton.playerStats.health;
        Resource mana = GameManager.singleton.playerStats.mana;
        health.SetCurrentValue(GameManager.singleton.playerStats.maxHealth);
        FindAnyObjectByType<PlayerCharacter>().CheckLowHP();
        mana.SetCurrentValue(GameManager.singleton.playerStats.maxMana);
        currentEXP = 0;

        SoundManager.singleton.PlayAudio(SoundManager.singleton.sfx_UseItem);
    }

    private int GetEXPRequiredForNextLevel()
    {
        return Mathf.CeilToInt(100 + (currentLevel * 75));
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
