using UnityEngine;
using UnityEngine.SceneManagement;  

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private string newLevel;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
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
