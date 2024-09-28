using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BalloonSpawner : MonoBehaviour
{
    public List<Enemy> enemies = new List<Enemy>();
    public int curentWave;
    public int waveValue;
    public List<GameObject> enemiesToSpawn = new List<GameObject>();

    public Transform spawnLocation;
    public int waveDuration;
    private float waveTimer;
    private float spawnInterval;
    private float spawnTimer;

    public List<GameObject> spawnedEnemies = new List<GameObject>();

    public Transform[] waypoints;


    void Start()
    {
        GenerateWave();
    }

    void FixedUpdate()
    {
        if(spawnTimer < 0)
        {
            if (enemiesToSpawn.Count > 0)
            {
                GameObject enemy = Instantiate(enemiesToSpawn[0], spawnLocation.position, Quaternion.identity);
                BaloonController balloonController = enemy.GetComponent<BaloonController>();
                if (balloonController != null)
                {
                    balloonController.SetWaypoints(waypoints);  // Assign waypoints to the balloon
                }

                enemiesToSpawn.RemoveAt(0);
                spawnTimer = spawnInterval;
                //Instantiate(enemiesToSpawn[0], spawnLocation.position, Quaternion.identity);
                //enemiesToSpawn.RemoveAt(0);
                //spawnTimer = spawnInterval;
            }
            else
            {
                waveTimer = 0;
            }
        }
        else
        {
            spawnTimer -= Time.fixedDeltaTime;
            waveTimer -= Time.fixedDeltaTime;
        }

       if(waveTimer <= 0 && spawnedEnemies.Count <= 0)
        {
            curentWave++;
            GenerateWave();
        }
    }

    public void GenerateWave()
    {
        waveValue = curentWave * 10;
        GenerateEnemies();

        spawnInterval = waveDuration / enemiesToSpawn.Count;
        waveTimer = waveDuration;
    }

    public void GenerateEnemies()
    {
        List<GameObject> generatedEnemies = new List<GameObject>();
        while (waveValue > 0 || generatedEnemies.Count < 20)
        {
            int randEnemyId = Random.Range(0, enemies.Count);
            int ranEnemyCost = enemies[randEnemyId].cost;

            if (waveValue - ranEnemyCost >= 0)
            {
                generatedEnemies.Add(enemies[randEnemyId].enemyPrefab);
                waveValue -= ranEnemyCost;
            }
            else if (waveValue <= 0)
            {
                break;
            }
        }
        enemiesToSpawn.Clear();
        enemiesToSpawn = generatedEnemies;
    }

}

[System.Serializable]

public class Enemy
{
    public GameObject enemyPrefab;
    public int cost;


}



