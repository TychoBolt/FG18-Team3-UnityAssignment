using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGhost : Enemy
{
    Transform player;
    public float spawnDelay = 5;
    bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Maincharacter")?.GetComponent<Transform>();
        SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        spawnDelay -= Time.deltaTime;
        if (spawnDelay < 0)
            SetActive(true);

        if (isActive)
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    void SetActive(bool active)
    {
        GetComponent<Renderer>().enabled = active;
        GetComponent<CircleCollider2D>().enabled = active;
        isActive = active;
    }
}
