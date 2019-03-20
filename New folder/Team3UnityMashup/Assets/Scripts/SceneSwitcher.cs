using UnityEngine;
using UnityEngine.SceneManagement;  

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private string newLevel;

    void OnTriggerEnter2D(Collider2D other)
    {
<<<<<<< HEAD
        if (other.CompareTag("Player")) //&& manager.numberOfKeys == 3)
=======
        if (other.CompareTag("Player") && GameObject.FindObjectOfType<GameManager>().isAllKeysCollected())
>>>>>>> 67ef630549218f62f5a56b37244ec7bfe8160084
        {
            SceneManager.LoadScene(newLevel);
        }
   
    }


   public void GotoLevel_1Scene()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void GotoMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }
}
