using UnityEngine;
using TMPro; 

public class Apple : MonoBehaviour
{
    public TMP_Text scoreText;
    private int score = 0;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Jika apel jatuh melewati batas bawah layar tanpa tertangkap
        if (transform.position.y < -5.5f)
        {
            ResetApple();
        }
    }

    // Fungsi deteksi tabrakan (karena 'Is Trigger' dicentang)
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Cek apakah yang ditabrak adalah Player
        if (other.CompareTag("Player"))
        {
            score += 10;
            if (scoreText != null)
            {
                scoreText.text = "Skor: " + score;
            }
            
            ResetApple();
        }
    }

    void ResetApple()
    {
        // Reset posisi apel ke atas secara acak di sumbu X
        float randomX = Random.Range(-7f, 7f);
        transform.position = new Vector2(randomX, 5f);

        // Netralkan kecepatan jatuh agar tidak semakin cepat/rusak
        rb.linearVelocity = Vector2.zero; 
    }
}