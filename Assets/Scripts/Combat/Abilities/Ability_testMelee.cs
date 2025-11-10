using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Ability/Base Ability")]
public class Ability_testMelee : CharacterAbility
{
    [SerializeField] public GameObject attackHitBox;

    public override void Use(int attackID, Transform t)
    {
        GameObject attackBox = Instantiate(attackHitBox, t);
        Hitbox hitbox = attackBox.GetComponent<Hitbox>();
        hitbox.attackID = attackID;
        Destroy(attackBox, 0.3f);
        Debug.Log("ability used");
    }
}
