using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UI;
using TMPro;

public class PlayerControl : MonoBehaviour
{
    public GameObject GameManagerGO;


    public GameObject PlayerBullet;
    public GameObject BulletPosition01;
    public GameObject ExplosionGO;

    public TextMeshProUGUI LivesUIText;
    const int MaxLives = 3;
    int lives;
    public float speed;

    public void Init()
    {
        lives = MaxLives;

        LivesUIText.text = lives.ToString();

        transform.position = new Vector2(0, 0);


        gameObject.SetActive(true);
    }

    void Start()
    {
        
    }

   
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            

            GameObject bullet01 = (GameObject)Instantiate(PlayerBullet);
            bullet01.transform.position = BulletPosition01.transform.position;



        }


        float x = Input.GetAxisRaw("Horizontal"); //the value will be -1,0, or 1 (for left, no input, and right)    
        float y = Input.GetAxisRaw("Vertical"); // the value will be -1,0, or 1 (for down, no input and up)

        // based on the input we compute a direction vector, and we normalize it to get a unit vector
        Vector2 direction= new Vector2(x, y).normalized;

        //we call the function that computes and sets the player's position
        Move(direction);
    }

    void Move (Vector2 direction)
    {
        //Find the screen limits to the player's movement (left, right, top, and bottom edges of the screen)
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        max.x = max.x - 0.225f;
        min.x = min.x + 0.225f;

        max.y = max.y - 0.285f;
        min.y = min.y + 0.285f;

        Vector2 pos = transform.position;

        pos += direction * speed * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y=Mathf.Clamp(pos.y, min.y, max.y);

        transform.position = pos;   
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Detect collison of the player ship with an enemy ship, or with an enemy bullet
        if((col.tag =="EnemyShipTag") || (col.tag == "EnemyBulletTag"))
        {
            PlayExplosion();

            lives--;
            LivesUIText.text = lives.ToString();

            if (lives == 0)
            {
                //Destroy(gameObject); //Destroy the player's ship

                GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.GameOver);

                gameObject.SetActive(false);
            }
            
        }
    }

    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(ExplosionGO);

        explosion.transform.position = transform.position;
    }

}
