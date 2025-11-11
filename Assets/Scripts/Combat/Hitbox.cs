using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public int attackID;

    public int Damage()
    {
        return GameManager.singleton.playerStats.statCalcs.CalculatePhysAtkDmg(GameManager.singleton.playerStats.finalPhysAtk);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyType enemy = other.GetComponent<EnemyType>();
            if (enemy != null)
            {
                enemy.TakeDamage(attackID, Damage());
            }
        }
    }
}
