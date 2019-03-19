using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    int totalNumberOfPickups;
    GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindObjectOfType<GameManager>();
        totalNumberOfPickups = GameObject.FindObjectsOfType(typeof(PointPickup)).Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player") && manager.currentScore == (totalNumberOfPickups * 50))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Open gate");
            }
        }
    }
}
