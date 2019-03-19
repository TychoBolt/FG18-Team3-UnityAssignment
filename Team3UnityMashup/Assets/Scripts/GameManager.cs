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

    public GameObject thePlayer;

    // Start is called before the first frame update
    void Start()
    {
        lifeText.text = "LIFE: " + playerLives;
        scoreText.text = "SCORE: " + currentScore;
    }

    void Awake()
    {
       DontDestroyOnLoad(gameObject);
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

            lifeText.text = "Game Over";

        }
    }

    public void AddScore(int scoreToAdd)
    {
        currentScore += scoreToAdd;
        scoreText.text = "SCORE: " + currentScore;
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
}

