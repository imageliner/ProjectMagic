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

    [SerializeField] private GameObject testAttackBox;



    public void StandardAttack(Vector3 attackDir)
    {
        if (!isAttacking && attackCount < attackCountMax)
        {
            //Vector3 attackDir = new Vector3(mousePosition.x, 0f, mousePosition.z).normalized;
            StartCoroutine(Attack(attackDir.normalized));
        }
    }

    private IEnumerator Attack(Vector3 direction)
    {
        isAttacking = true;

        //GameManager.singleton.playerStats.SubtractStamina(dashStaminaNeeded);
        float elapsed = 0f;
        while (elapsed < attackDashTime)
        {
            transform.position += direction * attackDashPower * Time.deltaTime;
            elapsed += Time.deltaTime;
            attackCount++;

            yield return null;
        }
        Destroy(Instantiate(testAttackBox, transform), 0.5f);
        yield return new WaitForSeconds(attackCoolDown);
        isAttacking = false;
        attackCount = 0;

    }
}
