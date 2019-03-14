using UnityEngine;
using UnityEngine.SceneManagement;  

public class SceneSwitcher : MonoBehaviour
{
   public void GotoLevel_1Scene()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void GotoMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }
}
