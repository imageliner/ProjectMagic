using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Ability/Base Ability")]
public class Ability_testMelee : CharacterAbility
{
    [SerializeField] public GameObject attackHitBox;

    public override void Use(int attackID, Transform t, string fromEntity, int damage)
    {
        GameObject attackBox = Instantiate(attackHitBox, t);
        Hitbox hitbox = attackBox.GetComponent<Hitbox>();
        hitbox.damage = damage;
            //GameManager.singleton.playerStats.statCalcs.CalculatePhysAtkDmg(GameManager.singleton.playerStats.finalPhysAtk);
        hitbox.attackID = attackID;
        hitbox.fromEntity = fromEntity;
        Destroy(attackBox, 0.3f);
    }
}
