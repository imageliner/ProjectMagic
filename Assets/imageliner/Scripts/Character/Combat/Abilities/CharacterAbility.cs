using UnityEngine;
using UnityEngine.UI;

public abstract class CharacterAbility : ScriptableObject
{
    public Sprite icon;
    public string abilityName;
    public string abilityDescription;
    public bool mousePosAim;
    [SerializeField] protected float cooldown;
    [SerializeField] protected int manaCost;
    [SerializeField] protected int abilityDamage = 0;
    [SerializeField] protected float damageScaling = 1f;
    [SerializeField] protected ParticleSystem effect;

    [SerializeField] protected DamageType damageType;


    public float GetCooldown()
    {
        return cooldown;
    }

    public int GetManaCost()
    {
        return manaCost;
    }

    public int GetBaseDmg()
    {
        return abilityDamage;
    }

    public DamageType GetDamageType()
    {
        return damageType;
    }

    public abstract void Use(int attackID, Transform transform, string fromEntity, int damage, Rigidbody ownerRB);

}

public enum DamageType
{
    Physical,
    Magic
}
