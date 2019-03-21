using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public float speed;
    float currentHealt;
    Weapon playerWeapon;

    void OnEnable()
    {
        playerWeapon = GameObject.Find("Maincharacter")?.GetComponent<Weapon>();
    }

    public virtual void TakeDamage()
    {
        currentHealt -= playerWeapon.Damage;
        if (currentHealt <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            FindObjectOfType<GameManager>().KillPlayer();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            FindObjectOfType<GameManager>().KillPlayer();
        }
    }
}
