using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Transform playerSpawnPoint;
    public int playerLives;
    public int currentScore;

    public int numberOfKeys;
   
    public Text lifeText;
    public Text scoreText;

    public int scoreForLife1;
    public int scoreForLife2;
    public int scoreForLife3;
    public int scoreForLife4;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        lifeText.text = " " + playerLives;
        scoreText.text = " " + currentScore;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Awake()
    {
       DontDestroyOnLoad(gameObject);
    }

    public void KillPlayer()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");

        playerLives -= 1;

        if (playerLives >= 0)
        {
            player.gameObject.SetActive(false);
            player.transform.position = playerSpawnPoint.position;
            player.gameObject.SetActive(true);

            lifeText.text = " " + playerLives;
        }
        else
        {
            player.gameObject.SetActive(false);

            lifeText.text = "Game Over";
        }
    }

    public void AddScore(int scoreToAdd)
    {
        currentScore += scoreToAdd;
        scoreText.text = " " + currentScore;

        if (currentScore == scoreForLife1)
        {
            playerLives += 1;
            lifeText.text = " " + playerLives;
        }

        if (currentScore == scoreForLife2)
        {
            playerLives += 1;
            lifeText.text = " " + playerLives;
        }

        if (currentScore == scoreForLife3)
        {
            playerLives += 1;
            lifeText.text = " " + playerLives;
        }

        if (currentScore == scoreForLife4)
        {
            playerLives += 1;
            lifeText.text = " " + playerLives;
        }
    }

        public void AddKey()
    {
        numberOfKeys += 1;
        Debug.Log(numberOfKeys);
    }

    public void ResetKeys()
    {
        numberOfKeys -= 3;
        Debug.Log(numberOfKeys);
    }

    public bool isAllKeysCollected() => GameObject.FindObjectsOfType(typeof(OnTriggeredKey)).Length == 0;
}

