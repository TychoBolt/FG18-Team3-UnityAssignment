using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //public int health;
    public float speed;
    //float currentHealt;
    //Weapon playerWeapon;
    //Weapon weaponScript;

    void Start()
    {
        //GameObject weapon = GameObject.Find("Maincharacter");
        //weaponScript = weapon.GetComponent<Weapon>();
        //currentHealt = health;
        //playerWeapon = GameObject.Find("Maincharacter")?.GetComponent<Weapon>();
    }

    public virtual void TakeDamage()
    {
        //Debug.Log(playerWeapon.Damage); 
        //currentHealt -= playerWeapon.Damage;
        ////currentHealt -= weaponScript.Damage;
        //if (currentHealt <= 0)
        //{
        //    Destroy(gameObject);
        //}
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
}
