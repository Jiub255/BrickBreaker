using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{

    public Rigidbody2D rb;
    public int ballSpeed = 400;
    public bool inPlay;

    public Transform paddle;
    public Transform explosionRed;
    public Transform explosionGreen;
    public Transform explosionPurple;
    public GameManager gm;
    public Transform powerup;
    public Transform coin;
    public Transform widener;
    public Transform gun;

    public float angleMultiplier = .5f;

    public Vector3 heightcorrection;
    public Vector3 corrected;
    public float height = -0.6f;

    public AudioSource paddleBounce;
    public AudioSource brickBounce;

    //public float angleTotal = 45f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //audio = GetComponent<AudioSource>();
        //audio2 = GetComponent<AudioSource>(); 
    }

    // Update is called once per frame
    void Update()
    {

        if (gm.gameOver)
        {
            rb.bodyType = RigidbodyType2D.Static;
            transform.Translate(Vector3.up * 80);
            //rb.velocity = Vector2.zero;
            return;
        }

        else if(gm.gameOver == false)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }

        if (!inPlay)
        {

            heightcorrection = new Vector3(0, height, 0);
            corrected = paddle.position + heightcorrection;
            transform.position = corrected;
        }

        if (Input.GetButtonDown("Jump") && !inPlay)
        {
            inPlay = true;
            rb.AddForce(Vector2.up * ballSpeed);
            paddleBounce.Play();
        }

        //maybe do like 3 or 5 colliders on the paddle and manually set the bounce angle based on ball velocity
        //rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y); like this kinda


        //trying to get a minimum angle for the ball's trajectory, not working fully.
        //maybe do if rb.velocity.normalized is something, then ...?



        //still need to deal with shallow angles while the ball comes down from above
        //and sometimes the ball gets stuck going up and down on the side
        if (transform.position.y < -3 && rb.velocity.y > 0)
        {

            if (rb.velocity.y < (rb.velocity.x * angleMultiplier))
            {

                float currentVelocity = rb.velocity.magnitude;
                Vector2 desiredDirection = new Vector2(1, 1);
                Vector2 newVelocity = desiredDirection.normalized * currentVelocity;
                rb.velocity = newVelocity;
            }

            else if (rb.velocity.y < -(rb.velocity.x * angleMultiplier))
            {

                float currentVelocity = rb.velocity.magnitude;
                Vector2 desiredDirection = new Vector2(-1, 1);
                Vector2 newVelocity = desiredDirection.normalized * currentVelocity;
                rb.velocity = newVelocity;
            }

        }





            //float xVelocity = rb.velocity.x;
            //Vector2 vec = new Vector2(xVelocity, xVelocity * angleMultiplier);
            //rb.velocity = vec;


            //Vector2 vec = new Vector2(rb.velocity.x,rb.velocity.x * angleMultiplier);
            //vec.Normalize();
            //rb.velocity = Vector2.zero; 
            //rb.AddForce(vec * ballSpeed);

            //Vector3 newRot = new Vector3(0f, angleTotal, 0f);
            //transform.rotation = Quaternion.
        //}

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Bottom"))
        {
            rb.velocity = Vector2.zero;
            inPlay = false;
            gm.UpdateLives(-1);
            //want to add a 'destroy all powerup items' function here
            gm.DestroyPowerups();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {


        if (other.transform.CompareTag("Brick"))
        {
            brickBounce.Play();
        }

        if (other.transform.CompareTag("Paddle"))
        {
            paddleBounce.Play();
        }

    }


        //probably a cleaner way to do this with just the "brick" tag, then using public variables for 
        //the stuff that makes the different colored bricks different. item chances, hitstobreak, etc.
        //also could make hitstobreak more adjustable

    /*
            BrickScript brickScript = other.gameObject.GetComponent<BrickScript>();
            if (brickScript.hitsToBreak == 3)
            {
                brickScript.BreakBrick();
            }

            else if (brickScript.hitsToBreak == 2)
            {
                brickScript.BreakBrick2();
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
            brickBounce.Play();

        }

        if (other.transform.CompareTag("Purple Brick"))
        {
            BrickScript brickScript = other.gameObject.GetComponent<BrickScript>();
            if (brickScript.hitsToBreak == 2)
            {
                brickScript.BreakBrick2();
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
            brickBounce.Play();

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
            brickBounce.Play();

        }

        if (other.transform.CompareTag("Paddle")) 
        {
            paddleBounce.Play();
        }
    }*/

}
