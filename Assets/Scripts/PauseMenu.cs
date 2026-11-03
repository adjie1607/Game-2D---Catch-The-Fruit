using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject settingsPanel;

    private bool isPaused = false;

    void Start()
    {
        // Memastikan panel tersembunyi dan waktu berjalan normal saat game dimulai
        if (pausePanel != null) pausePanel.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(false);
        Time.timeScale = 1f; 
    }

    void Update()
    {
        // Dukungan Tombol Keyboard (ESC) untuk Pause / Resume otomatis
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    // 1. Fungsi Buka Pause Menu
    public void PauseGame()
    {
        isPaused = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0f; // Membekukan pergerakan & fisika game
    }

    // 2. Fungsi Tombol "Lanjut" (Resume)
    public void ResumeGame()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(false);
        Time.timeScale = 1f; // Mengembalikan waktu game ke kondisi normal
    }

    // 3. Fungsi Tombol "Setting"
    public void OpenSettings()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(true);
        }
    }

    public void CloseSettings()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }
    }

    // 4. Fungsi Tombol "Exit"
    public void ExitToMainMenu()
    {
        Time.timeScale = 1f; // PENTING: Kembalikan waktu ke normal sebelum pindah scene!
        SceneManager.LoadScene("MainMenu"); // Ganti nama scene sesuai Main Menu Anda
    }
}