using UnityEngine;
using UnityEngine.SceneManagement;

public class Gate : MonoBehaviour
{
    public string nextLevel;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player") && GameObject.FindObjectOfType<GameManager>().isAllKeysCollected())
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                FindObjectOfType<GameManager>().ResetKeys();
                SceneManager.LoadScene(nextLevel);
                Destroy(gameObject);
            }
        }
    }
}
