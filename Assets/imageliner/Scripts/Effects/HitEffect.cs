using TMPro;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    private HitEffectPool poolOwner;
    [SerializeField] private ParticleSystem _effect;


    public void SetEffectToUse(ParticleSystem newEffect)
    {
        _effect = newEffect;
    }

    public void UseEffect(ParticleSystem newEffect, Transform pos)
    {
        gameObject.SetActive(true);
        transform.position = pos.position;
        //transform.SetParent(null);
        if (newEffect != null)
        {
            Destroy(_effect.gameObject);
        }

        if (_effect != null)
        {
            _effect = Instantiate(newEffect, transform);
            _effect.Play();
            Invoke("ResetEffect", 1f);
        }
        
    }

    public void InitializePooledEffects(HitEffectPool owner)
    {
        poolOwner = owner;
    }

    private void ResetEffect()
    {
        transform.SetParent(poolOwner.transform);

        poolOwner.ReturnEffect(this);

        gameObject.SetActive(false);
    }
}
