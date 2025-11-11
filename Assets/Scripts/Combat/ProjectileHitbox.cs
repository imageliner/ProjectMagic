using System.Collections;
using UnityEngine;

public class ProjectileHitbox : Hitbox
{

    private void Update()
    {
        this.gameObject.transform.position += transform.forward * 10f * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyType enemy = other.GetComponent<EnemyType>();
            if (enemy != null)
            {
                enemy.TakeDamage(attackID, Damage());
                Destroy(gameObject);
            }
        }
    }
}
