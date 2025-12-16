using System.Collections.Generic;
using UnityEngine;

public class HitEffectPool : MonoBehaviour
{
    [SerializeField] private HitEffect effectRef;
    [SerializeField] private List<HitEffect> availableEffects = new List<HitEffect>();
    [SerializeField] private List<HitEffect> unavailableEffects = new List<HitEffect>();

    private void Awake()
    {
        for (int index = 0; index < 20; index++)
        {
            CreatePooledEffect();
        }
    }

    private void CreatePooledEffect()
    {
        HitEffect effectClone = Instantiate(effectRef, transform);
        effectClone.InitializePooledEffects(this);

        effectClone.gameObject.name = availableEffects.Count.ToString();
        availableEffects.Add(effectClone);
        effectClone.gameObject.SetActive(false);
    }

    public HitEffect GetAvailableEffect()
    {
        if (availableEffects.Count == 0)
        {
            //return null;
            CreatePooledEffect();
        }

        HitEffect firstAvailableEffect = availableEffects[0];

        availableEffects.RemoveAt(0);
        unavailableEffects.Add(firstAvailableEffect);

        return firstAvailableEffect;
    }

    public void ReturnEffect(HitEffect usedEffect)
    {
        unavailableEffects.Remove(usedEffect);
        availableEffects.Add(usedEffect);
    }
}
