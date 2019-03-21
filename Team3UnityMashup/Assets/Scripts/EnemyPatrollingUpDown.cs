using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrollingUpDown : Enemy
{
    bool movingUp = true;
    bool hasFlipped = false;
    public GameObject wayPointOne;
    public GameObject wayPointTwo;
    public float idleTime;
    bool isIdle = false;
    Vector2 direction = Vector2.up;

    GameManager gameManager;
    

    void Update ()
    {
        if (!isIdle)
            transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == wayPointOne || other.gameObject == wayPointTwo)
        {
            FlipOnDelay();
        }
    }

    void Flip()
    {
        direction *= -1;

        movingUp = !movingUp;
        hasFlipped = false;
        isIdle = false;
    }

    void FlipOnDelay()
    {
        if (!hasFlipped)
        {
            Invoke("Flip", idleTime);
            hasFlipped = true;
            isIdle = true;
        }
    }

}
