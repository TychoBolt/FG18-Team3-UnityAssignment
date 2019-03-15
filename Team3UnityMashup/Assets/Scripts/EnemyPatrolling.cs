using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolling : MonoBehaviour
{
    public float speed;
    bool movingRight = false;
    bool hasFlipped = false;
    public GameObject wayPointOne;
    public GameObject wayPointTwo;
    public Transform groundCheckPos;
    public float idleTime;
    bool isIdle = false;


    void Update ()
    {
        if (!isIdle)
            transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D groundCheck = Physics2D.Raycast(groundCheckPos.position, Vector2.down, 2f);

        if (groundCheck.collider == null || groundCheck.collider.gameObject.layer == 10)
            FlipOnDelay();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == wayPointOne || other.gameObject == wayPointTwo)
            FlipOnDelay();
    }

    void Flip()
    {
        if (movingRight)
            transform.eulerAngles = new Vector3(0, -180, 0);
        else
            transform.eulerAngles = new Vector3(0, 0, 0);

        movingRight = !movingRight;
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
