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

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        lifeText.text = "LIFE: " + playerLives;
        scoreText.text = "SCORE: " + currentScore;
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

            lifeText.text = "LIFE: " + playerLives;
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

    public bool isAllKeysCollected() => GameObject.FindObjectsOfType(typeof(OnTriggeredKey)).Length == 0;
}

