using UnityEngine;
using TMPro;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] int baseStatPoints_perLevel = 2;
    [SerializeField] int baseStatPoints_Offset = 0;
    [SerializeField] int baseVitality_perLevel = 1;
    [SerializeField] int baseVitality_Offset = 0;
    [SerializeField] int vitalityToHealthConversion = 10;

    [SerializeField] TextMeshProUGUI statPointText;
    [SerializeField] TextMeshProUGUI vitalityText;
    [SerializeField] TextMeshProUGUI healthText;

    public int baseStatPoints { get; protected set; } = 0;
    public int baseVitality { get; protected set; } = 0;

    public int statPoint
    {
        get
        {
            return baseStatPoints;
        }
    }

    public int vitality
    {
        get
        {
            return baseVitality;
        }
    }

    public int maxHealth
    {
        get
        {
            return vitality * vitalityToHealthConversion;
        }
    }

    public void OnUpdateLevel(int previousLevel, int currentLevel)
    {
        baseStatPoints = baseStatPoints_perLevel * currentLevel + baseStatPoints_Offset;
        baseVitality = baseVitality_perLevel * currentLevel + baseVitality_Offset;

        vitalityText.text = $"Vitality: {vitality}";
        healthText.text = $"Health: {maxHealth}";
        statPointText.text = $"Stat Points: {statPoint}";
    }
}
