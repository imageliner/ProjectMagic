using UnityEngine;
using UnityEngine.UI;

public class UIAbilityIcon : MonoBehaviour
{
    public Image abilityIcon;

    public void CooldownVisual(float fill)
    {
        abilityIcon.fillAmount = fill;
    }

    public void StartCooldown()
    {
        //anim? effect?
    }
}
