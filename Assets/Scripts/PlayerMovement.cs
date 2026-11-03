using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    public float limitX = 8f; // Batas layar[cite: 1]
    private float moveInput = 0f;

    void Update()
    {
        // Gabungkan input Keyboard (PC)[cite: 1] dan Tombol Mobile
        float keyboardInput = Input.GetAxis("Horizontal"); //[cite: 1]
        float finalMove = (keyboardInput != 0) ? keyboardInput : moveInput;

        // Gerakkan Player
        transform.Translate(Vector3.right * finalMove * speed * Time.deltaTime);

        // Batasi posisi X agar tidak keluar layar[cite: 1]
        float clampedX = Mathf.Clamp(transform.position.x, -limitX, limitX);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }

    // Fungsi helper yang akan dipanggil oleh Tombol UI Mobile
    public void SetMoveDirection(float direction)
    {
        moveInput = direction; // Nilai: -1 (Kiri), 1 (Kanan), atau 0 (Dilepas/Stop)
    }
}