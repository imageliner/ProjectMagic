using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public int attackID;

    public int Damage()
    {
        return GameManager.singleton.playerStats2.statCalcs.CalculatePhysAtkDmg(GameManager.singleton.playerStats2.finalPhysAtk);
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
