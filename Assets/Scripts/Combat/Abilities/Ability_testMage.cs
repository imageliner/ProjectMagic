using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Ability/test Mage Ability")]
public class Ability_testMage : CharacterAbility
{
    [SerializeField] public GameObject attackHitBox;

    public override void Use(int attackID, Transform t)
    {
        GameObject attackBox = Instantiate(attackHitBox, t);
        attackBox.transform.Translate(Vector3.forward * 5);
        Hitbox hitbox = attackBox.GetComponent<Hitbox>();
        hitbox.attackID = attackID;
        Destroy(attackBox, 0.3f);
        Debug.Log("ability used");
    }
}
