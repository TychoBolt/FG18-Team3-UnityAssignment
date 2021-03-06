﻿using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 10f;
    public Rigidbody2D Rb2D;

    private void Start()
    {
        Rb2D.velocity = new Vector2(transform.right.x, transform.right.y) * Speed;
    }

    void OnTriggerEnter2D(Collider2D HitInfo)
    {
        Enemy enemy = HitInfo.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage();
            Destroy(gameObject, 0.01f);
        }
        else if (HitInfo.name.Equals("Tilemap"))
        {
            Destroy(gameObject, 0.01f);
        }

    }

}
