using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public int attackID;
    public string fromEntity = "NPC";

    public bool debugHitbox = false;
    public int debugDmg = 0;


    public int damage;

    [SerializeField] protected ParticleSystem impactEffect;

    private void OnTriggerEnter(Collider other)
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
                GameManager.singleton.hitstopManager.HitStop?.Invoke();
                enemy.TakeDamage(attackID, damage, this.gameObject);
                HitEffectPool effPool = FindAnyObjectByType<HitEffectPool>();
                HitEffect newEffect = effPool.GetAvailableEffect();
                newEffect.UseEffect(impactEffect, enemy.transform);
            }
        }
        
    }
}
