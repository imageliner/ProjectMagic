using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Ability/Dash Ability")]
public class Ability_testDash : CharacterAbility
{
    [SerializeField] public GameObject attackHitBox;

    [SerializeField] private float dashForce = 5;
    [SerializeField] private float dashDuration = 1;

    public override void Use(int attackID, Transform t, string fromEntity, int damage, Rigidbody ownerRB)
    {
        if (attackHitBox != null)
        {
            GameObject attackBox = Instantiate(attackHitBox, t);
            Hitbox hitbox = attackBox.GetComponent<Hitbox>();
            hitbox.damage = damage;
            hitbox.attackID = attackID;
            hitbox.fromEntity = fromEntity;
            Destroy(attackBox, 0.3f);
        }
        Vector3 aimDir = (t.position - ownerRB.transform.position).normalized;

        ownerRB.GetComponent<PlayerMovement>().StartDash(aimDir, dashForce, dashDuration, ownerRB);
    }
}
