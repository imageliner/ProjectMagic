using UnityEngine;

public class UIAbility : MonoBehaviour
{
    [SerializeField] UIAbilityIcon[] abilityIcons;
    private AbilityClass[] abilities;

    private PlayerCharacter player;

    public void Bind(PlayerCharacter newPlayer)
    {
        if (player != null)
            Unbind(player);

        player = newPlayer;

        if (player == null)
            return;

        abilities = player.abilities;

        player.useAbilityEvents[0] += abilityIcons[0].StartCooldown;
        player.useAbilityEvents[1] += abilityIcons[1].StartCooldown;
        player.useAbilityEvents[2] += abilityIcons[2].StartCooldown;
    }

    public void Unbind(PlayerCharacter oldPlayer)
    {
        oldPlayer.useAbilityEvents[0] -= abilityIcons[0].StartCooldown;
        oldPlayer.useAbilityEvents[1] -= abilityIcons[1].StartCooldown;
        oldPlayer.useAbilityEvents[2] -= abilityIcons[2].StartCooldown;
    }

    private void Update()
    {
        if (abilities == null) return;

        for (int i = 0; i < abilityIcons.Length; i++)
        {
            if (abilities[i] != null)
            {
                float maxCD = abilities[i].ability.GetCooldown();
                float currentCD = abilities[i].currentCooldown;
                abilityIcons[i].abilityIcon.sprite = abilities[i].ability.icon;
                float fill = 1f - (currentCD / maxCD);
                abilityIcons[i].CooldownVisual(fill);
            }
        }
    }

    private void OnDestroy()
    {
        if (player != null)
            Unbind(player);
    }
}
