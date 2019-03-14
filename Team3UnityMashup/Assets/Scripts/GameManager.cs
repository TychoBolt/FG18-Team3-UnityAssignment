using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform playerSpawnPoint;
    public int playerLives;

    public GameObject thePlayer;

    // Start is called before the first frame update
    void Start()
    {
        
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
        }
        else
        {
            thePlayer.gameObject.SetActive(false);

        }
    }
}

