using UnityEngine;

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
        Debug.Log(HitInfo.name);

        //if (HitInfo.GetComponent<EnemyPatrolling>() != null)
        //{
        //    HitInfo.GetComponent<EnemyPatrolling>().TakeDamage();
        //}
        //if (HitInfo.GetComponent<Enemy>() != null)
        //{
        //    HitInfo.GetComponent<Enemy>().TakeDamage();
        //}

        Enemy enemy = HitInfo.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage();
        }

        Destroy(gameObject, 0.25f);
    }
}
