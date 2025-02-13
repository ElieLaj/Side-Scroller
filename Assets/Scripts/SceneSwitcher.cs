using UnityEngine;

public class SceneSwitcher : MonoBehaviour
{
    public void SwapToGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
    }

    public void ReloadScene()
    {
       UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void SwapToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("StartMenu");
    }
}
