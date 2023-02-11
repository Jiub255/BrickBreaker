using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int lives;
    public int coins;
    public int score;
    public int level = 1;
    public Text livesText;
    public Text coinsText;
    public Text scoreText;
    public Text levelText;
    private float timeToDisappear = 2.0f;
    public Text highScoreText;
    public InputField highScoreInput;
    public Button playButton;
    public Button quitButton;
    public Text pressEnterText;

    public bool gameOver;
    public GameObject gameOverPanel;

    public int numberOfBricks;

    public Transform[] levels;
    public int currentLevelIndex = 0;

    public GameObject ball;
    private BallScript ballScript;
    public GameObject paddle;
    private PaddleScript paddleScript;
    private SpriteRenderer spriteRenderer;
    public Sprite paddleNoGun;

    public GameObject[] extraLivesToDestroy;
    public GameObject[] widenersToDestroy;
    public GameObject[] gunsToDestroy;
    public GameObject[] coinsToDestroy;



    //private static bool s_permanentInstance = true;
    // public static GameManager instance = null;

    //  private void Awake()
    // {
    //     if(instance = null)
    //    {
    //         instance = this;
    //     }
    //     else if(instance != this)
    //    {
    //         Destroy(gameObject);
    //    }
    //
    //     DontDestroyOnLoad(gameObject);
    //   }


    // Start is called before the first frame update
    public void Start()
    {
        livesText.text = "Lives: " + lives;
        coinsText.text = "Coins: " + coins;
        scoreText.text = "Score: " + score;

        levelText.text = "Level " + level;
        levelText.enabled = true;
        timeToDisappear = 2.0f;

        numberOfBricks = GameObject.FindGameObjectsWithTag("Brick").Length;
        ballScript = ball.GetComponent<BallScript>();
        paddleScript = paddle.GetComponent<PaddleScript>();
        spriteRenderer = paddle.GetComponent<SpriteRenderer>();


    }

    // Update is called once per frame
    void Update()
    {
        timeToDisappear -= Time.deltaTime;

        if (levelText.enabled && (timeToDisappear <= 0.0f))
        {
            levelText.enabled = false;
        }


        if (Input.GetKeyDown("return") && gameOver == true)
        {
            //Debug.Log("Enter");
            playButton.gameObject.SetActive(true);
            quitButton.gameObject.SetActive(true);
            pressEnterText.gameObject.SetActive(false);
        }
    }

    public void UpdateLives(int changeInLives)
    {
        lives += changeInLives;

        //check for no lives left and trigger the end of the game
        if(lives <= 0)
        {
            lives = 0;
            GameOver();
        }

        livesText.text = "Lives " + lives;
    }

    public void UpdateCoins(int coinsGained)
    {
        coins += coinsGained;

        coinsText.text = "Coins: " + coins;
    }

    public void UpdateScore(int points)
    {
        score += points;

        scoreText.text = "Score: " + score;
    }

    public void UpdateLevel()
    {
        level++;

        levelText.text = "Level " + level;
    }

    public void UpdateNumberOfBricks()
    {
        numberOfBricks--;
        if(numberOfBricks <= 0)
        {
            if(currentLevelIndex >= levels.Length - 1)
            {
                GameOver();
            }
            else
            {
                gameOver = true;
                Invoke("LoadLevel", 2f);
                //want to go to a menu between levels where you can spend money and upgrade your paddle or whatever, instead of load level right away
            }
        }
    }

    public void DestroyPowerups()
    {
        extraLivesToDestroy = GameObject.FindGameObjectsWithTag("Extra Life");
        for (var i = 0; i < extraLivesToDestroy.Length; i++)
            Destroy(extraLivesToDestroy[i]);

        widenersToDestroy = GameObject.FindGameObjectsWithTag("Widener");
        for (var j = 0; j < widenersToDestroy.Length; j++)
            Destroy(widenersToDestroy[j]);

        gunsToDestroy = GameObject.FindGameObjectsWithTag("Gun");
        for (var k = 0; k < gunsToDestroy.Length; k++)
            Destroy(gunsToDestroy[k]);

        coinsToDestroy = GameObject.FindGameObjectsWithTag("Coin");
        for (var l = 0; l < coinsToDestroy.Length; l++)
            Destroy(coinsToDestroy[l]);

    }

    void LoadLevel()
    {
        currentLevelIndex++;
        Instantiate(levels[currentLevelIndex], Vector2.zero, Quaternion.identity);
        numberOfBricks = GameObject.FindGameObjectsWithTag("Brick").Length;
        
        gameOver = false;
        ballScript.inPlay = false;
        
        UpdateLevel();
        levelText.enabled = true;
        timeToDisappear = 2.0f;

        paddle.transform.localScale = new Vector3(1, 1, 1);
        paddleScript.width = 1;
        paddleScript.gun = false;
        spriteRenderer.sprite = paddleNoGun;
        
    }


    void GameOver()
    {
        gameOver = true;
        gameOverPanel.SetActive(true);

        DestroyPowerups();

        int highScore = PlayerPrefs.GetInt("HIGHSCORE");
        if (score > highScore)
        {
            PlayerPrefs.SetInt("OLDHIGHSCORE", highScore);
            PlayerPrefs.SetInt("HIGHSCORE", score);
            string oldHighScoreName = PlayerPrefs.GetString("HIGHSCORENAME");
            PlayerPrefs.SetString("OLDHIGHSCORENAME", oldHighScoreName);

            if (highScore != 0)
            {
                highScoreText.text = "New High Score: " + score + "\n" + "Old High Score: " + highScore;
                highScoreInput.gameObject.SetActive(true);
                playButton.gameObject.SetActive(false);
                quitButton.gameObject.SetActive(false);
                pressEnterText.gameObject.SetActive(true);
            }
            
            else
            {
                highScoreText.text = "High Score: " + score;
                highScoreInput.gameObject.SetActive(true);
                playButton.gameObject.SetActive(false);
                quitButton.gameObject.SetActive(false);
                pressEnterText.gameObject.SetActive(true);
            }

        }

        else
        {
            highScoreText.text = PlayerPrefs.GetString("HIGHSCORENAME") + "'s High Score: " + highScore + "\n" 
                 + "Your Score: " + score + "\n" + "Be Better.";
        }

    }

    public void NewHighScore()
    {

        string highScoreName = highScoreInput.text;
        PlayerPrefs.SetString("HIGHSCORENAME", highScoreName);
        highScoreInput.gameObject.SetActive(false);
        if (PlayerPrefs.GetInt("OLDHIGHSCORE") != 0) 
        {
            highScoreText.text = "Congratulations " + highScoreName + "!" + "\n"
                + "New High Score: " + score + "\n" + "Old High Score: " + PlayerPrefs.GetInt("OLDHIGHSCORE")
                + " By " + PlayerPrefs.GetString("OLDHIGHSCORENAME");
        }

        else
        {
            highScoreText.text = "Congratulations " + highScoreName + "!" + "\n"
                + "High Score: " + score;
        }
    
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Main");
    }

    public void Quit()
    {
        SceneManager.LoadScene("StartMenu");
        //Debug.Log("Game Quit");
    }

}
