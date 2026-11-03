using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadingManager : MonoBehaviour
{
    [Header("Loading UI")]
    public GameObject loadingPanel;
    public Slider loadingSlider;
    public TMP_Text loadingText;

    // Fungsi yang dipanggil oleh tombol PLAY
    public void PlayGameWithLoading(string sceneName)
    {
        // Jalankan proses loading di background menggunakan Coroutine
        StartCoroutine(LoadSceneAsyncProcess(sceneName));
    }

    IEnumerator LoadSceneAsyncProcess(string sceneName)
    {
        // 1. Munculkan UI Loading
        loadingPanel.SetActive(true);

        // 2. Mulai load scene di background
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        // Mencegah scene langsung berganti sebelum loading selesai/terlihat
        operation.allowSceneActivation = false;

        float progress = 0f;

        while (!operation.isDone)
        {
            // Unity membaca progress loading dari 0.0 sampai 0.9
            // 0.9 sampai 1.0 adalah tahap inisialisasi akhir
            float targetProgress = Mathf.Clamp01(operation.progress / 0.9f);

            // Trik "Fake Smooth Loading" agar barnya tidak loncat terlalu cepat
            while (progress < targetProgress)
            {
                progress += Time.deltaTime * 2f; // Kecepatan gerak bar
                loadingSlider.value = progress;
                loadingText.text = "Loading... " + (progress * 100f).ToString("F0") + "%";
                yield return null;
            }

            // Jika sudah mencapai 100% (1.0), izinkan pindah scene
            if (operation.progress >= 0.9f && progress >= 0.99f)
            {
                loadingText.text = "Memulai Game...";
                yield return new WaitForSeconds(0.5f); // Jeda sejenak agar terasa mulus
                operation.allowSceneActivation = true; // Resmi pindah scene
            }

            yield return null;
        }
    }
}