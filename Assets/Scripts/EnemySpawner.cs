using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyShip01;
    float maxSpawnRateInSeconds = 2f;
    void Start()
    {
       
    }

    
    void Update()
    {
        
    }

    // Function to spawn an enemy
    void SpawnEnemy()
    {
        
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2 (0,0));

        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2 (1,1));

        // Instantiate an enemy
        GameObject anEnemy = (GameObject)Instantiate(EnemyShip01);
        anEnemy.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);


        //Schedule when to spawn next enemy
        ScheduleNextEnemySpawn();
    }

    void ScheduleNextEnemySpawn()
    {

        float spawnInSeconds;

        if(maxSpawnRateInSeconds > 1f) 
        {
            // Pick a number between 1 and maxSpawnRateInSeconds
            spawnInSeconds = Random.Range(1f, maxSpawnRateInSeconds);

        }
        else
        {
            spawnInSeconds = 1f;

            Invoke("SpawnEnemy", spawnInSeconds);
        }

      
    }

    // Function to increase the difficulty of the game
    void IncreaseSpawnRate()
    {
        if (maxSpawnRateInSeconds > 1f)
            maxSpawnRateInSeconds--;
        if(maxSpawnRateInSeconds == 1f)
            CancelInvoke("IncreaseSpawnRate");
    }

    public void ScheduleEnemySpawner()
    {
        Invoke("SpawnEnemy", maxSpawnRateInSeconds);

        // Increase spawn rate every 30 seconds
        InvokeRepeating("IncreaseSpawnRate", 0f, 30f);
    }

    public void UnscheduleEnemySpawner()
    {
        CancelInvoke("SpawnEnemy");
        CancelInvoke("IncreaseSpawnRate");

    }

}
