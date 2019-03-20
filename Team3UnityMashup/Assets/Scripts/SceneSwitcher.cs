using UnityEngine;
using UnityEngine.SceneManagement;  

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private string newLevel;

    GameManager manager;

    void Start()
    {
        manager = GameObject.FindObjectOfType<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) //&& manager.numberOfKeys == 3)
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
