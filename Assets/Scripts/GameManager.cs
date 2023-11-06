using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int lives = 3;
    private int score = 0;
    public GameObject planet;
    public GameObject erase;
    public GameObject restart;
    public GameObject panel;
    public TextMeshProUGUI points;
    public TextMeshProUGUI lifeRemaining;
    public TextMeshProUGUI time;
    public TextMeshProUGUI gameOver;
    public TextMeshProUGUI bestScore;
    public AudioClip defused;
    public AudioClip exploted;
    public AudioClip won;
    public AudioClip lost;
    public AudioClip tie;
    private GameObject sm;
    private float sumTime;
    private float respawn;
    private float respawnInterval;
    private bool soundPlayed;
    // Start is called before the first frame update
    void Start()
    {
        bestScore.enabled = false;
        gameOver.enabled = false;
        soundPlayed = false;
        restart.SetActive(false);
        erase.SetActive(false);
        panel.SetActive(false);
        time.color = Color.white;
        points.color = Color.white;
        lifeRemaining.color = Color.white;
        sumTime = 0.0f;
        lives = 3;
        score = 0;
        respawnInterval = 10.0f;
        points.text = "Score: " + score;
        lifeRemaining.text = "Lives: " + lives;
        time.text = "Time: " + sumTime.ToString("0.00");
        sm = GameObject.FindGameObjectWithTag("SpawnManager");
        respawn = sm.GetComponent<SpawnManager>().respawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (lives <= 0)
        {
            PlayerPrefs.SetInt("GameFinish", 1);
        }
        if (PlayerPrefs.GetInt("GameFinish") == 0)
        {
            if (sumTime >= respawnInterval && respawn != 1.0f)
            {
                respawn -= 0.5f;
                respawnInterval += 10.0f;
                sm.GetComponent<SpawnManager>().SetRespawnTime(respawn);
            }
            sumTime += Time.deltaTime;
            time.text = "Time: " + sumTime.ToString("0.00");
            time.color = Color.Lerp(Color.white, Color.red, sumTime / 60.0f);
        }
        else
        {
            panel.SetActive(true);
            sm.GetComponent<SpawnManager>().SetRespawnTime(3.0f);
            restart.SetActive(true);
            erase.SetActive(true);
            bestScore.enabled = true;
            if (!soundPlayed)
            {
                if (score > PlayerPrefs.GetInt("oldScore") || PlayerPrefs.GetInt("newPlay") == 0 && score > 0)
                {
                    GetComponent<AudioSource>().PlayOneShot(won);
                    bestScore.text = "New Best Score: " + PlayerPrefs.GetInt("maxScore");
                    gameOver.text = "You've bested your best score!";
                    gameOver.color = Color.green;
                    PlayerPrefs.SetInt("oldScore", score);
                }
                else if (score < PlayerPrefs.GetInt("maxScore") || PlayerPrefs.GetInt("newPlay") == 0 && score == 0)
                {
                    bestScore.text = "Best Score: " + PlayerPrefs.GetInt("maxScore");
                    GetComponent<AudioSource>().PlayOneShot(lost);
                    gameOver.text = "You didn't improve your best score...";
                    gameOver.color = Color.red;
                }
                else if (score == PlayerPrefs.GetInt("oldScore") && PlayerPrefs.GetInt("newPlay") == 1)
                {
                    bestScore.text = "Best Score: " + PlayerPrefs.GetInt("maxScore");
                    GetComponent<AudioSource>().PlayOneShot(tie);
                    gameOver.text = "It's a tie!";
                    gameOver.color = Color.yellow;
                }
                gameOver.enabled = true;
                soundPlayed = true;
            }
        }
    }

    public void AddScore()
    {
        PlayerPrefs.SetInt("Explote", 1);
        GetComponent<AudioSource>().PlayOneShot(defused);
        score++;
        points.text = "Score: " + score;
        if (score > PlayerPrefs.GetInt("maxScore"))
        {
            PlayerPrefs.SetInt("maxScore", score);
            points.color = Color.cyan;
        }
    }
    public void TakeDamage()
    {
        PlayerPrefs.SetInt("Explote", 1);
        GetComponent<AudioSource>().PlayOneShot(exploted);
        lives--;
        lifeRemaining.text = "Lives: " + lives;
        lifeRemaining.color = new Color(lifeRemaining.color.r - 0.2f, 0, 0);
    }
    public void Erase()
    {
        PlayerPrefs.SetInt("maxScore", 0);
        bestScore.text = "New Score: " + PlayerPrefs.GetInt("maxScore");
    }
    public void Restart()
    {
        Start();
        PlayerPrefs.SetInt("GameFinish", 0);
        if (PlayerPrefs.GetInt("maxScore") == 0)
        {
            PlayerPrefs.SetInt("newPlay", 0);
        }
        else
        {
            PlayerPrefs.SetInt("newPlay", 1);
        }
    }
}
