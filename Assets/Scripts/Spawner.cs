using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject applePrefab;
    public GameObject bombPrefab;

    [Header("Difficulty Settings")]
    public float baseSpawnInterval = 1.5f;   // Kecepatan spawn awal
    public float minSpawnInterval = 0.4f;    // Batas tercepat (agar tidak mustahil dimainkan)
    public float spawnRangeX = 7.5f;

    private float currentSpawnInterval;
    private float timer;

    void Start()
    {
        currentSpawnInterval = baseSpawnInterval;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= currentSpawnInterval)
        {
            SpawnObject();
            timer = 0f;
        }
    }

    void SpawnObject()
    {
        float randomX = Random.Range(-spawnRangeX, spawnRangeX);
        Vector3 spawnPos = new Vector3(randomX, transform.position.y, 0);

        // 25% peluang muncul BOM, 75% APEL
        GameObject objectToSpawn = (Random.value < 0.25f) ? bombPrefab : applePrefab;

        Instantiate(objectToSpawn, spawnPos, Quaternion.identity);
    }

    // Fungsi penyesuaian kesulitan yang dipanggil saat skor bertambah
    public void UpdateDifficulty(int currentScore)
    {
        // Setiap bertambah 5 poin, jeda spawn berkurang 0.1 detik
        float speedUpRate = (currentScore / 5) * 0.1f;
        currentSpawnInterval = Mathf.Max(minSpawnInterval, baseSpawnInterval - speedUpRate);
    }
}