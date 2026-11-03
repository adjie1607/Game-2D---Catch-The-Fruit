using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Gameplay Data")]
    public int score = 0;
    public int lives = 3;
    private int highScore = 0;
    private bool isGameOver = false;

    [Header("UI Text References")]
    public TMP_Text scoreText;
    public TMP_Text livesText;
    public TMP_Text gameOverScoreText;      // Teks skor akhir di panel kalah
    public TMP_Text gameOverHighScoreText;  // Teks rekor tertinggi di panel kalah
    public GameObject gameOverPanel;

    [Header("Audio SFX")]
    public AudioSource sfxSource;       // Speaker pemutar SFX
    public AudioClip catchAppleSound;  // Suara saat dapat apel
    public AudioClip hitBombSound;    // Suara saat kena bom / nyawa berkurang
    public AudioClip gameOverSound;   // Suara saat game over

    [Header("External References")]
    public Spawner spawner;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        Time.timeScale = 1f; // Menjamin game berjalan normal saat scene dimulai
        isGameOver = false;

        // Ambil High Score lama dari memori lokal (default 0 jika pemain baru)
        highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        UpdateInGameUI();
    }

    public void AddScore(int amount)
    {
        if (isGameOver) return;
        sfxSource.PlayOneShot(catchAppleSound);

        score += amount;

        // Periksa dan simpan rekor baru jika skor melampaui rekor lama
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save(); // Memastikan data langsung tersimpan ke disk
        }

        // Naikkan tingkat kesulitan di Spawner
        if (spawner != null)
        {
            spawner.UpdateDifficulty(score);
        }

        UpdateInGameUI();
    }

    public void LoseLife()
    {
        if (isGameOver) return;
        sfxSource.PlayOneShot(hitBombSound);

        lives--;
        UpdateInGameUI();

        if (lives <= 0)
        {
            TriggerGameOver();
        }
    }

    void UpdateInGameUI()
    {
        if (scoreText != null) scoreText.text = "Skor: " + score;
        if (livesText != null) livesText.text = "Nyawa: " + lives;
    }

    void TriggerGameOver()
    {
        isGameOver = true;
        sfxSource.PlayOneShot(gameOverSound);
        Time.timeScale = 0f; // Menghentikan pergerakan game

        // Perbarui teks di panel Game Over
        if (gameOverScoreText != null) gameOverScoreText.text = "Skor Akhir: " + score;
        if (gameOverHighScoreText != null) gameOverHighScoreText.text = "High Score: " + highScore;

        if (gameOverPanel != null) gameOverPanel.SetActive(true);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // untuk memastikan waktu kembali normal saat kembali ke menu
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // untuk memastikan waktu kembali normal saat restart
        SceneManager.LoadScene("GameplayScene");
    }

}