using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassAnimate : MonoBehaviour
{

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //   animator.SetBool("ShouldAnimate?", false);
            animator.SetBool("ShouldAnimate?", true);

        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            animator.SetBool("ShouldAnimate?", false);
        }
    }
}