using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public GameObject EnemyBullet01;
    
    void Start()
    {
        Invoke("FireEnemyBullet", 1f);
    }

    
    void Update()
    {
        
    }

    void FireEnemyBullet() 
    {
        GameObject playerShip = GameObject.Find("MainShip");

        if (playerShip != null)
        { 
            GameObject bullet = (GameObject)Instantiate(EnemyBullet01);

            bullet.transform.position = transform.position;

            Vector2 direction = playerShip.transform.position - bullet.transform.position;

            bullet.GetComponent<EnemyBullet>().SetDirection(direction);
        }
    }
}
