using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static PlayerStats;

public class UIStatsPanel : MonoBehaviour
{
    private PlayerStats stats;

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

    [SerializeField] Button vitalityButton;
    [SerializeField] Button wisdomButton;
    [SerializeField] Button strengthButton;
    [SerializeField] Button dexButton;
    [SerializeField] Button intButton;

    private Coroutine initRoutine;

    private void OnEnable()
    {
        if (initRoutine == null)
            initRoutine = StartCoroutine(InitWhenReady());
    }

    IEnumerator InitWhenReady()
    {
        while (GameManager.singleton == null)
            yield return null;

        while (GameManager.singleton.playerStats == null)
            yield return null;

        stats = GameManager.singleton.playerStats;

        //debug double binding
        stats.StatsUpdated -= UpdateStatTexts;
        stats.StatsUpdated += UpdateStatTexts;

        //debug multi listener for multi stat spending
        vitalityButton.onClick.RemoveAllListeners();
        wisdomButton.onClick.RemoveAllListeners();
        strengthButton.onClick.RemoveAllListeners();
        dexButton.onClick.RemoveAllListeners();
        intButton.onClick.RemoveAllListeners();

        vitalityButton.onClick.AddListener(OnVitalityButtonClicked);
        wisdomButton.onClick.AddListener(OnWisdomButtonClicked);
        strengthButton.onClick.AddListener(OnStrengthButtonClicked);
        dexButton.onClick.AddListener(OnDexButtonClicked);
        intButton.onClick.AddListener(OnIntButtonClicked);

        UpdateStatTexts();
    }

    private void OnDisable()
    {
        if (initRoutine != null)
        {
            StopCoroutine(initRoutine);
            initRoutine = null;
        }

        if (stats != null)
            stats.StatsUpdated -= UpdateStatTexts;
    }

    public void RefreshReferences()
    {

        stats = GameManager.singleton.playerStats;
    }

    public void UpdateStatTexts()
    {

        statPointText.text = $"Stat Points: {stats.statPoints}";
        vitalityText.text = $"Vitality: {stats.vitality.GetTotalValue()}";
        wisdomText.text = $"Wisdom: {stats.wisdom.GetTotalValue()}";
        strengthText.text = $"Strength: {stats.strength.GetTotalValue()}";
        dexText.text = $"Dexterity: {stats.dexterity.GetTotalValue()}";
        intText.text = $"Intelligence: {stats.intelligence.GetTotalValue()}";
        pAtkText.text = $"Phys Atk: {stats.finalPhysAtk}";
        pDefText.text = $"Phys Def: {stats.finalPhysDef}";
        mAtkText.text = $"Magic Atk: {stats.finalMAtk}";
        mDefText.text = $"Magic Def: {stats.finalMDef}";
        //atkSpeedText.text = $"Atk Speed: {finalAtkSpeed}";

        statPointText.text = $"Stat Points: {stats.statPoints}";
    }

    public void OnVitalityButtonClicked() => stats.SpendStatPoint(StatType.Vitality);
    public void OnWisdomButtonClicked() => stats.SpendStatPoint(StatType.Wisdom);
    public void OnStrengthButtonClicked() => stats.SpendStatPoint(StatType.Strength);
    public void OnDexButtonClicked() => stats.SpendStatPoint(StatType.Dexterity);
    public void OnIntButtonClicked() => stats.SpendStatPoint(StatType.Intelligence);
}
