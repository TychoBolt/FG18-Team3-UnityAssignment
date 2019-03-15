using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolling : MonoBehaviour
{
    public float speed;
    bool movingRight = true;
    bool hasFlipped = false;
    public GameObject wayPointOne;
    public GameObject wayPointTwo;
    //public Transform groundCheckPos;
    public float idleTime;
    bool isIdle = false;
    public int totalHealth = 10;
    float currentHealt;
    Weapon playerWeapon;
    GameManager gameManager;
    
    void Start()
    {
        currentHealt = totalHealth;
        playerWeapon = GameObject.Find("Maincharacter")?.GetComponent<Weapon>();
        gameManager = GetComponent<GameManager>();
    }

    void Update ()
    {
        if (!isIdle)
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        
        //RaycastHit2D groundCheck = Physics2D.Raycast(groundCheckPos.position, Vector2.down, 2f);
            
        //if (groundCheck.collider == null || groundCheck.collider.gameObject.layer == 10)
        //    FlipOnDelay();
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
        if (movingRight)
            transform.eulerAngles = new Vector3(0, 180, 0);
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

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            //FindObjectOfType<GameManager>().KillPlayer();
            //gameManager.KillPlayer();
            Debug.Log("Hurt enemy");
        }
    }

    public void TakeDamage()
    {
        currentHealt -= playerWeapon.Damage;
        if (currentHealt <= 0)
        {
            Destroy(gameObject);
        }
    }
}
