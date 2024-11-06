using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    GameObject scoreUITextGO;

    public GameObject ExplosionGO;
    float speed;
   
    void Start()
    {
        speed = 2f;

        scoreUITextGO = GameObject.FindGameObjectWithTag("ScoreTextTag");
    }

    
    void Update()
    {
        //Get the enemy current position
        Vector2 position = transform.position;

        //Compute the enemy new position
        position = new Vector2 (position.x, position.y - speed * Time.deltaTime);

        //Update the enemy position
        transform.position = position;


        //This is the bottom-left point of the screen
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0,0));

        // If the enemy went outside the screen on the bottomi then destroy the enemy
        if (transform.position.y < min.y)
        {
            
            Destroy(gameObject);
        
        }


    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Detect collison of the player ship with an enemy ship, or with an enemy bullet
        if ((col.tag == "PlayerShipTag") || (col.tag == "PlayerBulletTag"))
        {
            PlayExplosion();

            scoreUITextGO.GetComponent<GameScore>().Score += 100;

            Destroy(gameObject); //Destroy the player's ship
        }
    }

    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(ExplosionGO);

        explosion.transform.position = transform.position;
    }

}
