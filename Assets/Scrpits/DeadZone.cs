using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private ScoreManager scoreManager;
    public AudioClip beerFallSound; // Tham chiếu đến âm thanh bia rơi
    public AudioSource audioSource; // Tham chiếu đến AudioSource để phát âm thanh

    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>(); // Tìm ScoreManager
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Beer"))
        {
            Debug.Log("Bia đã rơi!");
            Destroy(collision.gameObject); // Hủy đối tượng bia khi chạm vào deadzone
            scoreManager.SubtractScore(100); // Trừ 50 điểm
            PlayBeerFallSound();
        }
    }
    
    private void PlayBeerFallSound()
    {
        // Phát âm thanh bia rơi
        audioSource.PlayOneShot(beerFallSound);
    }
}