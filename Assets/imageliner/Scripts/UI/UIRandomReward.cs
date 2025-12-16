using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class UIRandomReward : MonoBehaviour
{
    [SerializeField] private bool isAbility;

    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI rewardName;
    [SerializeField] private TextMeshProUGUI rewardDesc;
    [SerializeField] private TextMeshProUGUI rewardStats;

    [SerializeField] private GameObject itemButtons;
    [SerializeField] private GameObject abilityButtons;

    [SerializeField] private CharacterAbility currentAbility;
    [SerializeField] private InventoryItem currentItem;

    private void OnEnable()
    {
        SoundManager.singleton.PlayAudio(SoundManager.singleton.sfx_Confirm); //play music?
        RewardEntry reward = RandomRewardManager.singleton.GetRandomReward();
        RefreshUI(reward);
    }


    public void RefreshUI(RewardEntry reward)
    {
        ClearUI();
        if (reward.isAbility)
        {
            ToggleButtonTypes(true);

            icon.sprite = reward.ability.icon;
            rewardName.text = reward.ability.abilityName;
            rewardDesc.text = reward.ability.abilityDescription;
            rewardStats.text = "DMG  " + reward.ability.GetBaseDmg() + " " + reward.ability.GetCooldown() + "s CD " + reward.ability.GetManaCost() + " Mana Cost";

            currentAbility = reward.ability;
        }
        else
        {
            ToggleButtonTypes(false);

            icon.sprite = reward.item.itemIcon;
            rewardName.text = reward.item.itemName;
            rewardDesc.text = reward.item.itemDescription;
            if (reward.item is EquipInventoryItem)
            {
                EquipInventoryItem equipReward = (EquipInventoryItem)reward.item;
                rewardStats.text = "DMG " + equipReward.GetDamage() + " " + equipReward.GetStats();
            }
            else
            {
                rewardStats.text = "";
            }

            currentItem = reward.item;
        }
    }

    public void ClearUI()
    {
        icon.sprite = null;
        rewardName.text = null;
        rewardDesc.text = null;

        currentAbility = null;
        currentItem = null;
    }


    public void ToggleButtonTypes(bool ability)
    {
        itemButtons.SetActive(!ability);
        abilityButtons.SetActive(ability);
    }

    public void OnClickItemSelect()
    {
        SoundManager.singleton.PlayAudio(SoundManager.singleton.sfx_Confirm);
        Inventory playerInv = FindAnyObjectByType<Inventory>();
        playerInv.AddItem(currentItem);
        RandomRewardManager.singleton.DisableUI();
    }

    public void OnClickAbility1()
    {
        SoundManager.singleton.PlayAudio(SoundManager.singleton.sfx_Confirm);
        GameManager.singleton.player.SwapAbility(0, currentAbility);


        RandomRewardManager.singleton.DisableUI();
    }

    public void OnClickAbility2()
    {
        SoundManager.singleton.PlayAudio(SoundManager.singleton.sfx_Confirm);
        GameManager.singleton.player.SwapAbility(1, currentAbility);


        RandomRewardManager.singleton.DisableUI();
    }

    public void OnClickAbility3()
    {
        SoundManager.singleton.PlayAudio(SoundManager.singleton.sfx_Confirm);
        GameManager.singleton.player.SwapAbility(2, currentAbility);


        RandomRewardManager.singleton.DisableUI();
    }
}
