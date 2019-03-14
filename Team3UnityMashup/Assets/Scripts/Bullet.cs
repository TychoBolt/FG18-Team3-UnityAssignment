using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 10f;
    public Rigidbody2D Rb2D;
    //public int Damage = 40;



    void Start()
    {
        Rb2D.velocity = new Vector2(transform.right.x, transform.right.y) * Speed;
    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D HitInfo)
    {
        Debug.Log(HitInfo.name);
        // Enemy enemy = HitInfo.GetComponent<Enemy>();
        //if (enemy != null)
        //{
        //    enemy.TakeDamage();
        //}
        Destroy(gameObject);
    }
}
