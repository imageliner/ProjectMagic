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
    [SerializeField] protected ParticleSystem effect;


    public float GetCooldown()
    {
        return cooldown;
    }

    public int GetManaCost()
    {
        return manaCost;
    }

    public abstract void Use(int attackID, Transform transform, string fromEntity, int damage, Rigidbody ownerRB);

}
