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

    public UIPotionHUD hpPot;
    public UIPotionHUD mpPot;

    public GameObject statsDebug;
    //private bool isDebugView = false;

    [SerializeField] private GameObject tutorialWindow;
    [SerializeField] private GameObject pickupNotifContainer;
    [SerializeField] private UIPickupNotif pickupNotifPrefab;

    [SerializeField] private GameObject lvlupAlert;

    private void Start()
    {
        tutorialWindow.SetActive(false);
        lvlupAlert.SetActive(true);
        PlayerStats.hasStatPoints += ()=> SetLvlUpNotif(true);
        PlayerStats.noStatPoints += ()=> SetLvlUpNotif(false);
    }

    private void Update()
    {
        // Wait until the player and manager exist
        if (GameManager.singleton == null || GameManager.singleton.playerStats == null)
            return;

        UpdateStats();

    }

    public void EnableTutorial()
    {
        tutorialWindow.SetActive(true);
    }

    public void UpdateStats()
    {
        int currentHP = GameManager.singleton.playerStats.health.currentValue;
        int maxHP = GameManager.singleton.playerStats.maxHealth;

        int currentMana = GameManager.singleton.playerStats.mana.currentValue;
        int maxMana = GameManager.singleton.playerStats.maxMana;

        healthBarText.text = $"{currentHP} / {maxHP}";
        float healthCurrentPercent = (float)currentHP / maxHP;
        healthBarSlider.value = healthCurrentPercent;


        manaBarText.text = $"{currentMana} / {maxMana}";
        float manaCurrentPercent = (float)currentMana / maxMana;
        manaBarSlider.value = manaCurrentPercent;


    }

    public void UpdatePotionCount(int count, string type)
    {
        if (type == "health")
            hpPot.SetItemCount(count);

        if (type == "mana")
            mpPot.SetItemCount(count);
    }

    public void SetPickedupItemText(InventoryItem newItem)
    {
        UIPickupNotif newNotif = Instantiate(pickupNotifPrefab, pickupNotifContainer.transform, true);
        Destroy(newNotif.gameObject, 3f);
        newNotif.InitializeNotif(newItem);
    }

    public void SetLvlUpNotif(bool active)
    {
        lvlupAlert.SetActive(active);
    }

}
