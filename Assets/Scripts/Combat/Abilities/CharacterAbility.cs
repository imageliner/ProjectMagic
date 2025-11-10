using UnityEngine;

public abstract class CharacterAbility : ScriptableObject
{

    public abstract void Use(int attackID, Transform transform);

}
