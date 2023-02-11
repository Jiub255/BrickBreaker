using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    //public float speed = 10.0f;
    //private Rigidbody2D rb;
    //public Transform explosionRed;
    //public Transform explosionGreen;
    //public Transform explosionPurple;
    //public Transform powerup;
    //public Transform coin;
    //public Transform widener;
    //public Transform gun;
    //public Transform ball;
    //private GameManager gm;
    //private GameObject brickScript;//cant reference in prefab

    AudioSource hitBrick;


    private void Awake()
    {
        //rb = (Rigidbody2D)GetComponent(typeof(Rigidbody2D));
        //gm = FindObjectOfType<GameManager>();
    }


    // Start is called before the first frame update
    private void Start()
    {
        //rb = GetComponent<Rigidbody2D>;
        //gm = GameObject.Find("Game Manager");
        //brickScript = GameObject.Find("BrickScript");

        //gm = FindObjectOfType<GameManager>();

        //var bal = ball.GetComponent<Collider2D>();
        //Physics2D.IgnoreCollision(GetComponent<Collider2D>(), bal);

        hitBrick = GetComponent<AudioSource>();
        gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 1000);


    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(transform.up * speed * Time.deltaTime);
        //check for collisions with bricks or the top edge and destroy the bullet
        //still gonna need to reference the game manager though
    }



    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Brick"))
        {
            hitBrick.Play();
            Destroy(gameObject);
        }

        if (other.transform.CompareTag("Top"))
        {
            Destroy(gameObject);
        }
    }



    /*
            BrickScript brickScript = other.gameObject.GetComponent<BrickScript>();
            if (brickScript.hitsToBreak == 3)
            {
                brickScript.BreakBrick();
                hitBrick.Play();
                //Debug.Log("Red 3 left");
            }

            else if (brickScript.hitsToBreak == 2)
            {
                brickScript.BreakBrick2();
                hitBrick.Play();
                //Debug.Log("Red 2 left");

            }

            else
            {
                int randChance = Random.Range(1, 101);

                if (randChance < 10)
                {
                    Instantiate(powerup, other.transform.position, other.transform.rotation);
                }
                else if (randChance > 10 && randChance < 20)
                {
                    Instantiate(gun, other.transform.position, other.transform.rotation);
                }
                else if (randChance > 20 && randChance < 40)
                {
                    Instantiate(coin, other.transform.position, other.transform.rotation);
                }
                else if (randChance > 40 && randChance < 50)
                {
                    Instantiate(widener, other.transform.position, other.transform.rotation);
                }

                Transform newExplosion = Instantiate(explosionRed, other.transform.position, other.transform.rotation);
                Destroy(newExplosion.gameObject, 2.5f);


                gm.UpdateScore(brickScript.points);
                gm.UpdateNumberOfBricks();
                Destroy(other.gameObject);
            }
            Destroy(gameObject);

        }

        if (other.transform.CompareTag("Purple Brick"))
        {
            BrickScript brickScript = other.gameObject.GetComponent<BrickScript>();
            if (brickScript.hitsToBreak == 2)
            {
                brickScript.BreakBrick2();
                hitBrick.Play();
                //Debug.Log("Purple 2 left");

            }

            else
            {
                int randChance = Random.Range(1, 101);

                if (randChance < 8)
                {
                    Instantiate(powerup, other.transform.position, other.transform.rotation);
                }
                else if (randChance > 8 && randChance < 15)
                {
                    Instantiate(gun, other.transform.position, other.transform.rotation);
                }
                else if (randChance > 15 && randChance < 30)
                {
                    Instantiate(coin, other.transform.position, other.transform.rotation);
                }
                else if (randChance > 30 && randChance < 37)
                {
                    Instantiate(widener, other.transform.position, other.transform.rotation);
                }

                Transform newExplosion = Instantiate(explosionPurple, other.transform.position, other.transform.rotation);
                Destroy(newExplosion.gameObject, 2.5f);

                gm.UpdateScore(brickScript.points);
                gm.UpdateNumberOfBricks();
                Destroy(other.gameObject);
            }

            Destroy(gameObject);

        }

        if (other.transform.CompareTag("Green Brick"))
        {
            BrickScript brickScript = other.gameObject.GetComponent<BrickScript>();
            int randChance = Random.Range(1, 101);

            if (randChance < 5)
            {
                Instantiate(powerup, other.transform.position, other.transform.rotation);
            }
            else if (randChance > 5 && randChance < 10)
            {
                Instantiate(gun, other.transform.position, other.transform.rotation);
            }
            else if (randChance > 10 && randChance < 20)
            {
                Instantiate(coin, other.transform.position, other.transform.rotation);
            }
            else if (randChance > 20 && randChance < 25)
            {
                Instantiate(widener, other.transform.position, other.transform.rotation);
            }

            Transform newExplosion = Instantiate(explosionGreen, other.transform.position, other.transform.rotation);
            Destroy(newExplosion.gameObject, 2.5f);

            gm.UpdateScore(brickScript.points);
            gm.UpdateNumberOfBricks();
            Destroy(other.gameObject);

            Destroy(gameObject);

        }

        if (other.transform.CompareTag("Top"))
        {
            Destroy(gameObject);
        }

        //if (other.transform.CompareTag("Ball"))
        {

           // Debug.Log("Collision avoided");
        }
    }*/

}
