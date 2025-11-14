
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Ability/test Mage Ability")]
public class Ability_testMage : CharacterAbility
{
    [SerializeField] public GameObject abilityPrefab;
    [SerializeField] private float lifeTime = 0.5f;

    public override void Use(int attackID, Transform t, string fromEntity)
    {
        Vector3 spawnPos = t.position + t.forward * 2;
        Quaternion spawnRot = t.rotation;

        GameObject attackBox = Instantiate(abilityPrefab, spawnPos, spawnRot);
        Hitbox hitbox = attackBox.GetComponent<Hitbox>();
        hitbox.damage = GameManager.singleton.playerStats.finalMAtk;
        hitbox.attackID = attackID;
        hitbox.fromEntity = fromEntity;

        Destroy(attackBox, lifeTime);
    }

    private void DestroyInstantiate()
    {

    }
}
