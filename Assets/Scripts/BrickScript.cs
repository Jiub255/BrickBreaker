using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickScript : MonoBehaviour
{

    public int points;
    public int hitsToBreak;
    public Sprite hitSprite;
    public Sprite hitSprite2; 
    //maybe make more hitSprite slots, add an if line below for each one though

    //could do odds variables here for how likely to drop each powerup, then just need one brick tag and way less code i think
    //keep total of all odds less than 100 obviously
    public int oddsExtraLife;
    public int oddsWidener;
    public int oddsGun;
    public int oddsCoin;

    public Transform extraLife;
    public Transform widener;
    public Transform gun;
    public Transform coin;

    public Transform explosion;
    //public Transform explosionRed;
    //public Transform explosionPurple;
    //public Transform explosionGreen;

    private GameManager gm;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
    }
    public void BreakBrick()
    {
        hitsToBreak--;
        GetComponent<SpriteRenderer>().sprite = hitSprite;
        //GetComponent<SpriteRenderer>().color = Color.blue;
    }

    public void BreakBrick2()
    {
        hitsToBreak--;
        GetComponent<SpriteRenderer>().sprite = hitSprite2;
        //GetComponent<SpriteRenderer>().color = Color.blue;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Bullet") || other.transform.CompareTag("Ball"))
        {
            if (hitsToBreak == 3)
            {
                BreakBrick();
            }
            else if (hitsToBreak == 2)
            {
                BreakBrick2();
            }
            else
            {
                int randChance = Random.Range(1, 101);

                if (randChance < oddsExtraLife)
                {
                    Instantiate(extraLife, transform.position, transform.rotation);
                }
                else if (randChance > oddsExtraLife && randChance < (oddsExtraLife + oddsWidener))
                {
                    Instantiate(widener, transform.position, transform.rotation);
                }
                else if (randChance > (oddsExtraLife + oddsWidener) && randChance < (oddsExtraLife + oddsWidener + oddsGun))
                {
                    Instantiate(gun, transform.position, transform.rotation);
                }
                else if (randChance > (oddsExtraLife + oddsWidener + oddsGun) && randChance < (oddsExtraLife + oddsWidener + oddsGun + oddsCoin))
                {
                    Instantiate(coin, transform.position, transform.rotation);
                }

                Transform newExplosion = Instantiate(explosion, transform.position, transform.rotation);
                Destroy(newExplosion.gameObject, 2.5f);

                gm.UpdateScore(points);
                gm.UpdateNumberOfBricks();
                Destroy(gameObject);
            }

        }

    }

    /*

            Debug.Log("Bullet hit brick");
            if (CompareTag("Red Brick"))
            {
                if (hitsToBreak == 3)
                {
                    BreakBrick();
                }
                else if (hitsToBreak == 2)
                {
                    BreakBrick2();
                }
                else
                {
                    int randChance = Random.Range(1, 101);
  
                    if (randChance < 25)
                    {
                        Instantiate(powerup, transform.position, transform.rotation);
                    }
                    else if (randChance > 25 && randChance < 75)
                    {
                        Instantiate(gun, transform.position, transform.rotation);
                    }
                    else if (randChance > 75)
                    {
                        Instantiate(widener, transform.position, transform.rotation);
                    }
          
                    Transform newExplosion = Instantiate(explosionRed, transform.position, transform.rotation);
                    Destroy(newExplosion.gameObject, 2.5f);
              
                    gm.UpdateScore(points);
                    gm.UpdateNumberOfBricks();
                    Destroy(gameObject);
                }
            }
   
            else if (CompareTag("Purple Brick"))
            {
                if (hitsToBreak == 2)
                {
                    BreakBrick2();
                }
      
                else
                {
                    int randChance = Random.Range(1, 101);
            
                    if (randChance < 25)
                    {
                        Instantiate(gun, transform.position, transform.rotation);
                    }
                    else if (randChance > 25 && randChance < 50)
                    {
                        Instantiate(coin, transform.position, transform.rotation);
                    }
       
                    Transform newExplosion = Instantiate(explosionPurple, transform.position, transform.rotation);
                    Destroy(newExplosion.gameObject, 2.5f);
           
                    gm.UpdateScore(points);
                    gm.UpdateNumberOfBricks();
                    Destroy(gameObject);
                }
            }
   
            else if (CompareTag("Green Brick"))
            {
                int randChance = Random.Range(1, 101);
         
                if (randChance < 15)
                {
                    Instantiate(powerup, transform.position, transform.rotation);
                }
                else if (randChance > 15 && randChance < 30)
                {
                    _ = Instantiate(gun, transform.position, transform.rotation);
                }
         
                Transform newExplosion = Instantiate(explosionGreen, transform.position, transform.rotation);
                Destroy(newExplosion.gameObject, 2.5f);
             
                gm.UpdateScore(points); //cant reference the game manager since working with prefabs only here?
                gm.UpdateNumberOfBricks();
                Destroy(gameObject);
            }
      
            Destroy(other.gameObject);

        }

     
    */
}
