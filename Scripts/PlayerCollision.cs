using UnityEngine;

public class PlayerCollision : MonoBehaviour    // Xử lý va chạm
{
    private GameManager gameManager;
    private AudioManager audioManager;
    private void Awake() 
    {
        gameManager = FindAnyObjectByType<GameManager>();     // Tham chiếu tới các đối tượng có scripts GameManager
        audioManager = FindAnyObjectByType<AudioManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)     // Ktra va chạm khi player chạm vào collider coin (có tích isTrigger)
    {
        if(collision.CompareTag("Coin")){   // Coin: tag đc đặt tên gắn vào obeject Coin
            Destroy(collision.gameObject);  // Xóa khi chạm vào coin
            audioManager.PlayCoinSound();
            gameManager.AddScore(1);        // Số lượng điểm (points) tăng lên khi chạm 1 đồng coin
            Debug.Log("Hit Coin");
        }
        else if(collision.CompareTag("Trap"))   // Kiểm tra xem có va chạm với Trap(bẫy) hay k
        {
            audioManager.PlayGameOverSound();   // Mở tiếng game over
            audioManager.backgroundAudioSource.Stop();  // Tắt âm thanh nền
            gameManager.GameOver();
            Debug.Log("Die");
        }
        else if(collision.CompareTag("Enemy"))   // Kiểm tra xem có va chạm với Trap(bẫy) hay k
        {
            audioManager.PlayGameOverSound();
            audioManager.backgroundAudioSource.Stop();
            gameManager.GameOver();
            Debug.Log("Die");
        }
        else if(collision.CompareTag("Key"))   // Kiểm tra xem có va chạm với Trap(bẫy) hay k
        {
            audioManager.backgroundAudioSource.Stop();
            audioManager.PlayKeySound();
            Destroy(collision.gameObject);
            gameManager.GameWin();
            Debug.Log("You Win");
        }
    }
}
