using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    GameManager manager;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindObjectOfType<GameManager>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (manager.numberOfKeys == 1)
        {
            animator.SetBool("OneGem?", true);
        }

        if (manager.numberOfKeys == 2)
        {
            animator.SetBool("TwoGem?", true);
        }

        if (manager.numberOfKeys == 3)
        {
            animator.SetBool("ThreeGem?", true);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player") && GameObject.FindObjectsOfType(typeof(OnTriggeredKey)).Length == 0)
        {
            FindObjectOfType<GameManager>().ShowGateText();

            if (Input.GetButtonDown("Activate"))
            {
                Debug.Log("Open gate");
                FindObjectOfType<GameManager>().ResetKeys();
                FindObjectOfType<GameManager>().HideGateText();
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        FindObjectOfType<GameManager>().HideGateText();
    }
}
