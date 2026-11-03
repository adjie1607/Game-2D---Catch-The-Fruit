using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Update()
    {
        // Dukungan Tombol Keyboard (ESC) untuk keluar dari game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitGame();
        }
    }
    
    // tombol play
    public void PlayGame()
   {
    SceneManager.LoadScene("GameplayScene");
   }

    // tombol exit
    
   public void ExitGame()
   {
    Application.Quit();
    Debug.Log("Keluar Game");
   }


}
