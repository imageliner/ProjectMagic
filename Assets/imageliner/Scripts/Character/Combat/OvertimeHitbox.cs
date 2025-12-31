using UnityEngine;
using UnityEngine.TextCore.Text;

public class OvertimeHitbox : Hitbox
{
    public ParticleSystem aoeEffect;

    private float timeForConsecutiveHits;
    [SerializeField] private float timeForConsecutiveHitsDebug;
    private float timer;
    [SerializeField] private float timerDebug;

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
        //if (fromEntity == "Player" || fromEntity == "Enemy" || fromEntity == "NPC")
        //{
        //    timer = timeForConsecutiveHits;
        //}
    }

    private void OnTriggerStay(Collider other)
    {
        if (fromEntity == "Enemy" || fromEntity == "NPC")
        {
            if (isHeal)
            {
                EnemyType enemyOwner = other.GetComponent<EnemyType>();
                if (enemyOwner != null)
                {
                    AOEHealTimer(enemyOwner);
                }
                return;
            }
            
            PlayerCharacter player = other.GetComponent<PlayerCharacter>();
            if (player != null)
            {
                if (debugHitbox == true)
                {
                    AOEDamageTimerDebug(player);
                }
                else
                {
                    AOEDamageTimer(player);
                }
            }
        }
        if (fromEntity == "Player")
        {
            if (isHeal)
            {
                PlayerCharacter playerOwner = other.GetComponent<PlayerCharacter>();
                if (playerOwner != null)
                {
                    AOEHealTimer(playerOwner);
                }
                return;
            }
            EnemyType enemy = other.GetComponent<EnemyType>();
            if (enemy != null)
            {
                AOEDamageTimer(enemy);
            }
        }
        
    }

    private void AOEDamageTimerDebug(CharacterBase character)
    {
        PlayerCharacter player = (PlayerCharacter)character;
        if (timerDebug != timeForConsecutiveHitsDebug)
        {
            timerDebug += 1 * Time.deltaTime;
        }

        if (timerDebug >= timeForConsecutiveHitsDebug)
        {
            player.TakeDamage(Random.Range(1, 9999), debugDmg, damageType);
            //HitEffectPool effPool = FindAnyObjectByType<HitEffectPool>();
            //HitEffect newEffect = effPool.GetAvailableEffect();
            //newEffect.UseEffect(impactEffect, character.transform);
            //SpawnAudio();
            timerDebug = 0;
        }
    }

    private void AOEDamageTimer(CharacterBase character)
    {
        if (character is PlayerCharacter)
        {
            PlayerCharacter player = (PlayerCharacter)character;
            if (timer != timeForConsecutiveHits)
            {
                timer += 1 * Time.deltaTime;
            }

            if (timer >= timeForConsecutiveHits)
            {
                player.TakeDamage(attackID, damage, damageType);
                HitEffectPool effPool = FindAnyObjectByType<HitEffectPool>();
                HitEffect newEffect = effPool.GetAvailableEffect();
                newEffect.UseEffect(impactEffect, character.transform);
                SpawnAudio();
                timer = 0;
            }
        }

        if (character is EnemyType)
        {
            EnemyType enemy = (EnemyType)character;
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
                SpawnAudio();
                timer = 0;
            }
        }
    }

    private void AOEHealTimer(CharacterBase character)
    {
        if (character is PlayerCharacter)
        {
            PlayerCharacter player = (PlayerCharacter)character;
            if (timer != timeForConsecutiveHits)
            {
                timer += 1 * Time.deltaTime;
            }

            if (timer >= timeForConsecutiveHits)
            {
                player.TakeHeal(attackID, damage);
                HitEffectPool effPool = FindAnyObjectByType<HitEffectPool>();
                HitEffect newEffect = effPool.GetAvailableEffect();
                newEffect.UseEffect(impactEffect, character.transform);
                SpawnAudio();
                timer = 0;
            }
        }

        if (character is EnemyType)
        {
            EnemyType enemy = (EnemyType)character;
            if (timer != timeForConsecutiveHits)
            {
                timer += 1 * Time.deltaTime;
            }

            if (timer >= timeForConsecutiveHits)
            {
                enemy.TakeHeal(attackID, damage);
                HitEffectPool effPool = FindAnyObjectByType<HitEffectPool>();
                HitEffect newEffect = effPool.GetAvailableEffect();
                newEffect.UseEffect(impactEffect, enemy.transform);
                SpawnAudio();
                timer = 0;
            }
        }
    }
}
