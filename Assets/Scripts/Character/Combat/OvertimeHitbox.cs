using UnityEngine;

public class OvertimeHitbox : Hitbox
{
    public ParticleSystem aoeEffect;

    private float timeForConsecutiveHits;
    private float timer;

    public void setTimeForConsecutiveHits(float time)
    {
        timeForConsecutiveHits = time;
    }

    public float GetAOELifetime()
    {
        return aoeEffect.main.duration;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (fromEntity == "Player" || fromEntity == "Enemy" || fromEntity == "NPC")
        {
            timer = timeForConsecutiveHits;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (fromEntity == "Enemy" || fromEntity == "NPC")
        {
            PlayerCharacter player = other.GetComponent<PlayerCharacter>();
            if (player != null)
            {
                if (debugHitbox == true)
                {
                    player.TakeDamage(Random.Range(1, 9999), debugDmg);
                }
                else
                {
                    player.TakeDamage(attackID, damage);
                    HitEffectPool effPool = FindAnyObjectByType<HitEffectPool>();
                    HitEffect newEffect = effPool.GetAvailableEffect();
                    newEffect.UseEffect(impactEffect, player.transform);
                }
            }
        }
        if (fromEntity == "Player")
        {
            EnemyType enemy = other.GetComponent<EnemyType>();
            if (enemy != null)
            {
                //GameManager.singleton.hitstopManager.HitStop?.Invoke();
                if (timer != timeForConsecutiveHits)
                {
                    timer += 1 * Time.deltaTime;
                }
                
                 if (timer >= timeForConsecutiveHits)
                 {
                     enemy.TakeDamage(attackID, damage, this.gameObject, knockback);
                     HitEffectPool effPool = FindAnyObjectByType<HitEffectPool>();
                     HitEffect newEffect = effPool.GetAvailableEffect();
                     newEffect.UseEffect(impactEffect, enemy.transform);
                     timer = 0;
                 }
                
            }
        }
        
    }
}
