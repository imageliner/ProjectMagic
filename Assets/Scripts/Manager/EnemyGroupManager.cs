using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyGroupManager : MonoBehaviour
{
    [SerializeField] private bool useTrigger = false;

    [SerializeField] private EnemyType[] enemyPrefab;

    [SerializeField] private GameObject rewardPrefab;

    public UnityEvent rewardEvent;

    [SerializeField] private GameObject bounds;
    [SerializeField] private GameObject roomVisuals;

    [SerializeField] public int amount = 5;

    [SerializeField] public int groupLevel = 1;

    [SerializeField] public bool hasMiniboss = false;

    [SerializeField] private Vector3 spawnZone;

    [SerializeField] private List<EnemyType> spawnedEnemies = new List<EnemyType>();

    [SerializeField] private Vector3 lastEnemyPos;

    public Action FinalEnemyDead;


    private void Start()
    {
        if (useTrigger)
            gameObject.SetActive(false);
        
        FinalEnemyDead += SpawnMiniboss;

        

    }

    public void ActivateRoom()
    {
        if (bounds != null)
            bounds.SetActive(false);

        if (roomVisuals != null)
            roomVisuals.SetActive(false);

        SpawnEnemies();
    }

    //private void OnEnable()
    //{
    //    SpawnEnemies();
    //}

    private void Update()
    {
        if (spawnedEnemies.Count <= 0)
        {
            if (!hasMiniboss)
            {
                if (rewardPrefab != null)
                    Instantiate(rewardPrefab, lastEnemyPos, Quaternion.identity, null);

                if (bounds != null)
                    Destroy(bounds);

                rewardEvent?.Invoke();
                Destroy(this.gameObject);
            }
        }
    }

    private EnemyType ChooseRandomEnemyFromArray()
    {
        if (enemyPrefab == null || enemyPrefab.Length == 0)
        {
            return null;
        }

        int randomIndex = UnityEngine.Random.Range(0, enemyPrefab.Length);
        return enemyPrefab[randomIndex];
    }

    private void SpawnMiniboss()
    {
        EnemyType miniBoss = Instantiate(ChooseRandomEnemyFromArray(), lastEnemyPos, Quaternion.identity, this.transform);
        miniBoss.SetValues(groupLevel, true);
        miniBoss.InitializeHealthAfterStats();
        miniBoss.SetGroupOwner(this);
    }

    private void SpawnEnemies()
    {
        Vector3 half = spawnZone * 0.5f;

        while (spawnedEnemies.Count < amount)
        {
            Vector3 randomPos = new Vector3(UnityEngine.Random.Range(-half.x, half.x), 0, UnityEngine.Random.Range(-half.z, half.z));

            Vector3 worldPos = transform.TransformPoint(randomPos);

            EnemyType clonedPrefab = Instantiate(ChooseRandomEnemyFromArray(), worldPos, Quaternion.identity, this.transform);
            spawnedEnemies.Add(clonedPrefab);
            clonedPrefab.SetGroupOwner(this);
            clonedPrefab.SetValues(groupLevel, false);
        }
    }

    public void RemoveFromList(EnemyType enemy, Vector3 deathPos)
    {
        lastEnemyPos = new Vector3(deathPos.x, 0 , deathPos.z);
        spawnedEnemies.Remove(enemy);

        if (spawnedEnemies.Count == 0 && hasMiniboss)
        {
            SpawnMiniboss();
        }
    }

    public void MinibossDead(Vector3 deathPos)
    {
        if (rewardPrefab != null)
            Instantiate(rewardPrefab, deathPos, Quaternion.identity);

        if (bounds != null)
            Destroy(bounds);

        rewardEvent?.Invoke();

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, spawnZone);

    }
}
