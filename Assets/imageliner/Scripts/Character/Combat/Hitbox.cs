using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField] protected bool isHeal = false;
    public int attackID;
    public string fromEntity = "NPC";

    public bool debugHitbox = false;
    public int debugDmg = 0;

    [SerializeField] protected float knockback;

    public int damage;

    protected DamageType damageType;

    [SerializeField] protected ParticleSystem impactEffect;

    [SerializeField] protected AudioSource impactEff;

    private void Awake()
    {
        impactEff = GetComponentInChildren<AudioSource>();
    }

    protected void SpawnAudio()
    {
        AudioSource clonedAudio = Instantiate(impactEff, transform.position, transform.rotation, null);
        clonedAudio.Play();
        float audioTimer = clonedAudio.clip.length;
        Destroy(clonedAudio.gameObject, audioTimer);
    }


    public void SetDamageType(DamageType type)
    {
        damageType = type;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (fromEntity == "Enemy" || fromEntity == "NPC")
        {
            PlayerCharacter player = other.GetComponent<PlayerCharacter>();
            if (player != null)
            {
                if (debugHitbox == true)
                {
                    player.TakeDamage(Random.Range(1, 9999), debugDmg, damageType);
                }
                else
                {
                    player.TakeDamage(attackID, damage, damageType);
                    HitEffectPool effPool = FindAnyObjectByType<HitEffectPool>();
                    HitEffect newEffect = effPool.GetAvailableEffect();
                    newEffect.UseEffect(impactEffect, player.transform);

                    SpawnAudio();
                }
            }
        }
        if (fromEntity == "Player")
        {
            EnemyType enemy = other.GetComponent<EnemyType>();
            if (enemy != null)
            {
                GameManager.singleton.hitstopManager.HitStop?.Invoke();
                enemy.TakeDamage(attackID, damage, this.gameObject, knockback);
                HitEffectPool effPool = FindAnyObjectByType<HitEffectPool>();
                HitEffect newEffect = effPool.GetAvailableEffect();
                newEffect.UseEffect(impactEffect, enemy.transform);

                SpawnAudio();
            }
        }
        
    }
}

