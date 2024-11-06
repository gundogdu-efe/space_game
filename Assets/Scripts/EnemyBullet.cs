using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    float speed; //the bullet speed
    Vector2 _direction; //the direction of the bullet
    bool isReady; //to know when the bullet direction is set

    //set default values in Awake function
   void Awake()
    {
        speed = 5f;
        isReady = false;
    }

    void Start()
    {
        
    }
    //Function to set the bullet's direction
    public void SetDirection(Vector2 direction) 
    {
        //set the direction normalized, to get an unit vector
        _direction = direction.normalized;

        isReady = true; // set flag to true
    }

    
    void Update()
    {
        if (isReady)
        {
            //get the bullet's current position
            Vector2 position = transform.position;

            //compute the bullet's new position
            position += _direction * speed * Time.deltaTime;

            //update the bullet's position
            transform.position = position;


            //To remove the bullets from outside of the screen
            Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
            Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

            if((transform.position.x < min.x) || (transform.position.x > max.x) || (transform.position.y < min.y) || (transform.position.y > max.y))
            {
                Destroy(gameObject);
            }

        }

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        //Detect collison of the player ship with an enemy ship, or with an enemy bullet
        if (col.tag == "PlayerShipTag")
        {
            Destroy(gameObject); //Destroy the player's ship
        }
    }

}
