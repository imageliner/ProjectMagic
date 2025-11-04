using TMPro;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    [SerializeField] private int statPoints_perLevel = 2;
    [SerializeField] private int statPoints_Offset = 0;
    [SerializeField] private int statPoints_Spent = 0;

    [SerializeField] TextMeshProUGUI statPointText;
    [SerializeField] TextMeshProUGUI vitalityText;
    [SerializeField] TextMeshProUGUI wisdomText;
    [SerializeField] TextMeshProUGUI strengthText;
    [SerializeField] TextMeshProUGUI dexText;
    [SerializeField] TextMeshProUGUI intText;
    [SerializeField] TextMeshProUGUI pAtkText;
    [SerializeField] TextMeshProUGUI pDefText;
    [SerializeField] TextMeshProUGUI mAtkText;
    [SerializeField] TextMeshProUGUI mDefText;
    [SerializeField] TextMeshProUGUI atkSpeedText;

    public int statPoints { get; protected set; } = 0;

    protected override void Awake()
    {
        base.Awake();
    }

    public enum StatType
    {
        Vitality,
        Wisdom,
        Strength,
        Dexterity,
        Intelligence
    }

    protected override void InitializeStats()
    {
        base.InitializeStats();
    }

    public void SpendStatPoint(StatType statType)
    {
        if (statPoints <= 0)
        {
            Debug.Log("not enough stat points");
            return;
        }

        statPoints--;
        statPoints_Spent++;

        switch (statType)
        {
            case StatType.Vitality:
                vitality.AddBaseValue(1);
                break;

            case StatType.Wisdom:
                wisdom.AddBaseValue(1);
                break;

            case StatType.Strength:
                strength.AddBaseValue(1);
                break;

            case StatType.Dexterity:
                dexterity.AddBaseValue(1);
                break;

            case StatType.Intelligence:
                intelligence.AddBaseValue(1);
                break;

            default:
                Debug.LogWarning("Unknown stat type in SpendStatPoint");
                break;
        }

        UpdateStatTexts();
    }

    private void UpdateStatTexts()
    {
        statPointText.text = $"Stat Points: {statPoints}";
        vitalityText.text = $"Vitality: {vitality.GetTotalValue()}";
        wisdomText.text = $"Wisdom: {wisdom.GetTotalValue()}";
        strengthText.text = $"Strength: {strength.GetTotalValue()}";
        dexText.text = $"Dexterity: {dexterity.GetTotalValue()}";
        intText.text = $"Intelligence: {intelligence.GetTotalValue()}";
        pAtkText.text = $"Phys Atk: {finalPhysAtk}";
        pDefText.text = $"Phys Def: {finalPhysDef}";
        mAtkText.text = $"Magic Atk: {finalMAtk}";
        mDefText.text = $"Magic Def: {finalMDef}";
        atkSpeedText.text = $"Atk Speed: {finalAtkSpeed}";
    }

    public void OnUpdateLevel(int previousLevel, int currentLevel)
    {

        statPoints += statPoints_perLevel;

        statPointText.text = $"Stat Points: {statPoints}";
    }

    

    public void OnVitalityButtonClicked() => SpendStatPoint(StatType.Vitality);
    public void OnWisdomButtonClicked() => SpendStatPoint(StatType.Wisdom);
    public void OnStrengthButtonClicked() => SpendStatPoint(StatType.Strength);
    public void OnDexButtonClicked() => SpendStatPoint(StatType.Dexterity);
    public void OnIntButtonClicked() => SpendStatPoint(StatType.Intelligence);
}
