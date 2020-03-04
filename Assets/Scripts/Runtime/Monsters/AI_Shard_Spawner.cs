using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Shard_Spawner : MonoBehaviour
{

    private PLAYER player;
    private float range;
    public GameObject[] enemy;
    public float spawnTime;
    public int maxMobs;
    public Transform[] spawnPoints;

    public GameObject spawnlingParrent;

    private List<AI_HealthManager> enemies = new List<AI_HealthManager>();

    void Awake()
    {
        player = FindObjectOfType<PLAYER>();
    }

    void Start()
    {
        if (GetComponent<AI_HealthManager>().mobName == "Sapphire Shard")
        {
            spawnTime = Random.Range(2, 7);
        }
        else if (GetComponent<AI_HealthManager>().mobName == "Ruby Shard")
        {
            spawnTime = Random.Range(1f, 5f);
        }
        if (GetComponent<AI_HealthManager>().mobName == "Evil Shard")
        {
            spawnTime = Random.Range(0.5f, 3f);
        }

        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }


    void Spawn()
    {
        if (player.health.CurrentVal <= 0)
        {
            return;
        }

        if (FindObjectOfType<PLAYER>() != null)
        {
            maxMobs = FindObjectOfType<PLAYER>().level;
        }
        else
        {
            maxMobs = 1;
        }

        range = Vector2.Distance(transform.position, player.transform.position);

        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        int enemyIndex = Random.Range(0, enemy.Length);

        if (enemies.Count < maxMobs && range <= 2.4f)
        {
            Instantiate(enemy[0].GetComponent<AI_HealthManager>().poof, spawnPoints[spawnPointIndex].position, Quaternion.identity);
            FindObjectOfType<SFX_Manager>().poof.Play();
            var clone = Instantiate(enemy[enemyIndex], spawnPoints[spawnPointIndex].position, Quaternion.identity) as GameObject;
            clone.GetComponent<AI_HealthManager>().mobName = "SHARDLING";
            clone.transform.SetParent(spawnlingParrent.transform);
            clone.GetComponent<AI_HealthManager>().scorePerKill = 0;
            clone.GetComponent<AI_HealthManager>().expPerKill = 0;
            enemies.Add(clone.GetComponent<AI_HealthManager>());
        }
    }

    public void EnemyKilled(AI_HealthManager enemy)
    {
        if (enemies.Contains(enemy))
        {
            enemies.Remove(enemy);
        }
    }

    void OnDestroy()
    {
        foreach (var enemy in enemies)
        {
            enemy.aiCurrentHealth = 0;
        }
    }
}
