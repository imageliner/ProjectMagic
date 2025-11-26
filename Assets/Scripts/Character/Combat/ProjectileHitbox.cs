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
        if (fromEntity == "Enemy" || fromEntity == "NPC")
        {
            PlayerCharacter player = other.GetComponent<PlayerCharacter>();
            if (player != null)
            {
                if (debugHitbox == true)
                    player.TakeDamage(Random.Range(1, 9999), debugDmg);
                else
                    player.TakeDamage(attackID, damage);

                HitEffectPool effPool = FindAnyObjectByType<HitEffectPool>();
                HitEffect newEffect = effPool.GetAvailableEffect();
                newEffect.UseEffect(impactEffect, player.transform);
                Destroy(this.gameObject);
            }
        }
        if (fromEntity == "Player")
        {
            EnemyType enemy = other.GetComponent<EnemyType>();
            if (enemy != null)
            {
                enemy.TakeDamage(attackID, damage, this.gameObject);
                HitEffectPool effPool = FindAnyObjectByType<HitEffectPool>();
                HitEffect newEffect = effPool.GetAvailableEffect();
                newEffect.UseEffect(impactEffect, enemy.transform);
                Destroy(this.gameObject);
            }
        }

    }
}
