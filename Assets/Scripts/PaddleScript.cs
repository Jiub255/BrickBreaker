using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleScript : MonoBehaviour
{

    public float speed;

    public float rightScreenEdge;
    public float leftScreenEdge;

    public GameManager gm;

    public int width = 1;

    public Sprite paddleGun;
    public SpriteRenderer spriteRenderer;
    public bool gun = false;
    public GameObject bullet;
    public Transform trnsfrm;
    private Vector3 pos;
    private Vector3 pos2;
    private float posx1;
    private float posx2;
    private float posy;
    private float posz;

    public AudioSource shot;
    public AudioSource powerup;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        //pos.Set(trnsfrm.position.x, trnsfrm.position.y, trnsfrm.position.z);

        //shot = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (gm.gameOver)
        {
            return;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (gun)
            {
                //still have bullets following paddle in x direction
                //maybe do instantiate here and add force in bullet script? nope didnt work
                posx1 = transform.position.x - 0.7f - ((width - 1)* 0.7f);
                posx2 = transform.position.x + 0.7f + ((width - 1)* 0.7f);
                posy = transform.position.y - 0.7f;
                posz = transform.position.z;
                pos.Set(posx1, posy, posz);
                pos2.Set(posx2, posy, posz);

                GameObject blt = Instantiate(bullet, pos, transform.rotation);
                GameObject blt2 = Instantiate(bullet, pos2, transform.rotation);

                //blt.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 1000);
                //blt2.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 1000);

                shot.Play();

                //maybe do someething like blt.GetComponent<BulletScript>().shooter = gameObject; 
                //to try and get it to reference the game manager script?
            }
        }

        float horizontal = Input.GetAxis("Horizontal");

        transform.Translate(Vector2.right * horizontal * Time.deltaTime * speed);
        if(transform.position.x < leftScreenEdge + 0.7f * (width - 1)) //tried to correct for wider paddles here 
        {
            transform.position = new Vector2(leftScreenEdge + 0.7f * (width - 1), transform.position.y);
        }
        if (transform.position.x > rightScreenEdge - 0.7f * (width - 1))
        {
            transform.position = new Vector2(rightScreenEdge - 0.7f * (width - 1), transform.position.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Hit " + other.name);
        if(other.CompareTag("Extra Life"))
        {
            gm.UpdateLives(1);
            Destroy(other.gameObject);
            powerup.Play();
        }
        else if (other.CompareTag("Coin"))
        {
            gm.UpdateCoins(1);
            Destroy(other.gameObject);
            powerup.Play();
        }
        else if (other.CompareTag("Widener"))
        {
            //widen paddle using scale
            width++;
            transform.localScale = new Vector3(width,1,1);
            //or could do a separate sprite so it looks better/can look different
            Destroy(other.gameObject);
            powerup.Play();
        }
        else if (other.CompareTag("Gun"))
        {
            spriteRenderer.sprite = paddleGun;
            gun = true;
            Destroy(other.gameObject);
            powerup.Play();
        }
    }

}
