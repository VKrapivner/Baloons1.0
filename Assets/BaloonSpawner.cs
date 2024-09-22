using UnityEngine;
using System.Collections;

public class BalloonSpawner : MonoBehaviour
{
    public GameObject balloonPrefab;
    //public GameObject[] balloonPrefab;
    public Transform spawnPoint;
    public float spawnInterval = 2f;
    public float timer = 0.0f;
    public float spawnMax = 10.0f;
    public float spawnMin = 1.0f;
    public float waveLength;

    void Start()
    {
        timer = Random.Range(spawnMin, spawnMax);
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Instantiate(balloonPrefab, spawnPoint.position, Quaternion.identity);
            //timer = 0.0f;
            //timer = Random.Range(spawnMin, spawnMax);

        }
    }

}



