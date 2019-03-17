using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Transform playerSpawnPoint;
    public int playerLives;
    public int currentScore;

    public Text lifeText;
    public Text scoreText;

    public GameObject thePlayer;

    // Start is called before the first frame update
    void Start()
    {
        lifeText.text = "LIFE: " + playerLives;
        scoreText.text = "SCORE: " + currentScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void KillPlayer()
    {
        playerLives -= 1;

        if (playerLives >= 0)
        {
            thePlayer.gameObject.SetActive(false);
            thePlayer.transform.position = playerSpawnPoint.position;
            thePlayer.gameObject.SetActive(true);

            lifeText.text = "LIFE: " + playerLives;
        }
        else
        {
            thePlayer.gameObject.SetActive(false);

            lifeText.text = "NO LIVES";

        }
    }

    public void AddScore(int scoreToAdd)
    {
        currentScore += scoreToAdd;
        scoreText.text = "SCORE: " + currentScore;
    }
}

