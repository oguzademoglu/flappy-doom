using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene"); // Oyun sahnesinin adı
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Çıkış yapıldı.");
    }
}
