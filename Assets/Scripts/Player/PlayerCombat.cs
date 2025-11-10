using System.Collections;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public bool isAttacking;
    private int attackStaminaNeeded = 0;
    private float attackDashPower = 5f;
    private float attackDashTime = 0.15f;
    private float attackCoolDown = 0.15f;
    private int attackCountMax = 2;
    private int attackCount = 0;

    private int currentAttackID = 0;

    [SerializeField] private GameObject testAttackBox;



    public void StandardAttack(WeaponObject weapon, Vector3 attackDir)
    {
        if (!isAttacking && attackCount < attackCountMax)
        {
            currentAttackID++;
            StartCoroutine(NewAttack(weapon, attackDir.normalized, currentAttackID));
        }
    }

    private IEnumerator NewAttack(WeaponObject weapon, Vector3 direction, int attackID)
    {
        isAttacking = true;

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
        weapon.attackAbility.Use(attackID, transform);
        yield return new WaitForSeconds(attackCoolDown);
        isAttacking = false;
        attackCount = 0;

    }

    private IEnumerator Attack(Vector3 direction)
    {
        isAttacking = true;

        //GameManager.singleton.playerStats.SubtractStamina(dashStaminaNeeded);
        float elapsed = 0f;
        while (elapsed < attackDashTime)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            lookRotation.x = 0;
            lookRotation.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 50);
            transform.position += direction * attackDashPower * Time.deltaTime;
            elapsed += Time.deltaTime;
            attackCount++;

            yield return null;
        }
        GameObject attackBox = Instantiate(testAttackBox, transform);
        Hitbox hitbox = attackBox.GetComponent<Hitbox>();
        hitbox.attackID = currentAttackID;
        Destroy(attackBox, 0.3f);

        yield return new WaitForSeconds(attackCoolDown);
        isAttacking = false;
        attackCount = 0;

    }
}
