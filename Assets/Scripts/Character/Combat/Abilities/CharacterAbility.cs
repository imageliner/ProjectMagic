using UnityEngine;

public abstract class CharacterAbility : ScriptableObject
{
    public bool mousePosAim;
    [SerializeField] protected float cooldown;

    public float GetCooldown()
    {
        return cooldown;
    }

    public abstract void Use(int attackID, Transform transform, string fromEntity, int damage);

}
