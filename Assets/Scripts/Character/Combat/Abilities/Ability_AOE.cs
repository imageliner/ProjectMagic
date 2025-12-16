
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Ability/test AOE Ability")]
public class Ability_testAOE : CharacterAbility
{
    [SerializeField] public GameObject abilityPrefab;
    [SerializeField] private float lifeTime = 0.5f;
    [SerializeField] private float timeForConsecutiveHits = 0.5f;

    public override void Use(int attackID, Transform t, string fromEntity, int damage, Rigidbody ownerRB)
    {
        Vector3 spawnPos = t.position;
        Quaternion spawnRot = t.rotation;

        GameObject attackBox = Instantiate(abilityPrefab, spawnPos, spawnRot);
        attackBox.transform.forward = ownerRB.transform.forward;
        OvertimeHitbox hitbox = attackBox.GetComponent<OvertimeHitbox>();
        hitbox.setTimeForConsecutiveHits(timeForConsecutiveHits);
        lifeTime = hitbox.GetAOELifetime() - 0.5f;

        hitbox.SetDamageType(damageType);
        hitbox.damage = Mathf.RoundToInt(abilityDamage + (damage * damageScaling));

        hitbox.attackID = attackID;
        hitbox.fromEntity = fromEntity;

        Destroy(attackBox, lifeTime);
    }
}
