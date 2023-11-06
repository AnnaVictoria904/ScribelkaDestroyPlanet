using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayStart : MonoBehaviour
{
    public TextMeshProUGUI points;
    public TextMeshProUGUI lifeRemaining;
    public TextMeshProUGUI time;
    public GameObject erase;
    public GameObject scorePanel;
    public GameObject startPanel;
    public GameObject restart;
    public GameObject playButton;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("GameFinish", 0);
        PlayerPrefs.SetInt("Camera", 0);
        PlayerPrefs.SetInt("maxScore", 0);
        PlayerPrefs.SetInt("newPlay", 0);
        startPanel.SetActive(true);
        Time.timeScale = 0.0f;
        points.enabled = false; lifeRemaining.enabled = false; time.enabled = false; scorePanel.SetActive(false);
        erase.SetActive(false); restart.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void GameStart()
    {
        startPanel.SetActive(false);
        Time.timeScale = 1.0f;
        points.enabled = true; lifeRemaining.enabled = true; time.enabled = true;
        PlayerPrefs.SetInt("Camera", 1);
    }
}
