using UnityEngine;

public class Fruit : MonoBehaviour
{
    public bool isBatu = false; // Centang di Inspector jika ini adalah Batu
    public GameObject catchEffectPrefab;


    void Update()
    {
        // Jika jatuh melewati batas bawah layar tanpa ditangkap
        if (transform.position.y < -5.5f)
        {
            // Apel jatuh ke tanah = Nyawa berkurang. (Batu jatuh = Abaikan)
            if (!isBatu)
            {
                GameManager.instance.LoseLife();
            }

            Destroy(gameObject); // Hapus dari scene
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (catchEffectPrefab != null)
            {
                Instantiate(catchEffectPrefab, transform.position, Quaternion.identity);
            }
            
            if (isBatu)
            {
                // Menangkap batu = Nyawa berkurang
                GameManager.instance.LoseLife();
                
            }
            else
            {
                // Menangkap apel = Tambah skor
                GameManager.instance.AddScore(10);
                
            }

            Destroy(gameObject); // Hapus objek setelah ditangkap
        }
    }
}