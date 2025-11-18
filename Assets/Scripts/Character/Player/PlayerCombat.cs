using System;
using System.Collections;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public bool isAttacking;
    public bool spawnAttack;
    public bool inCombat;
    [SerializeField] private float combatToIdleTimer = 6.0f;
    [SerializeField] private float combatTimer;
    private float attackDashPower = 5f;
    private float attackDashTime = 0.15f;
    private float attackCoolDown = 0.5f;
    private int attackCountMax = 2;
    private int attackCount = 0;

    private int currentAttackID = 0;

    [SerializeField] private GameObject testAttackBox;

    public Action SheatheWeapon;
    public Action UnsheatheWeapon;

    private void Update()
    {
        if (combatTimer > 0)
        {
            combatTimer -= Time.deltaTime;
        }
        if (combatTimer <= 0)
        {
            combatTimer = 0;
            SheatheWeapon?.Invoke();
            inCombat = false;
        }
    }


    public void StandardAttack(WeaponObject weapon, Vector3 attackDir, string playerAtk)
    {
        if (!isAttacking)
        {
            inCombat = true;
            UnsheatheWeapon?.Invoke();

            combatTimer = combatToIdleTimer;

            currentAttackID++;
            StartCoroutine(Attack(weapon, attackDir.normalized, currentAttackID, playerAtk));
        }
    }

    private IEnumerator Attack(WeaponObject weapon, Vector3 direction, int attackID, string playerAtk)
    {
        isAttacking = true;
        spawnAttack = false;

        yield return new WaitUntil(() => spawnAttack == true);

        weapon.attackAbility.Use(attackID, transform, playerAtk);
        spawnAttack = false;

        float elapsed = 0f;
        while (elapsed < attackDashTime)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            lookRotation.x = 0;
            lookRotation.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 50);
            transform.position += direction * attackDashPower * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }
        attackCount++;
        
        yield return new WaitForSeconds(attackCoolDown);

        isAttacking = false;
        attackCount = 0;
    }

    public void SpawnHitbox()
    {

    }
    public void DespawnHitbox()
    {

    }
}
