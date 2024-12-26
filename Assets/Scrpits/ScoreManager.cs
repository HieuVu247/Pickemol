using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text currentScoreText; // Tham chiếu đến UI Text để hiển thị điểm hiện tại
    public TMP_Text highscoreText; // Tham chiếu đến UI Text để hiển thị Highscore
    public ParticleSystem scoreParticleSystem; // Tham chiếu đến Particle System
    public Animator scoreAnimator; // Tham chiếu đến Animator của Text Score
    public AudioClip collectBeerSound; // Tham chiếu đến âm thanh nhặt được bia
    public AudioSource audioSource; // Tham chiếu đến AudioSource để phát âm thanh
    
    private int currentScore = 0;
    private int highscore = 0;

    private void Start()
    {
        LoadHighscore();
        UpdateScoreUI();
    }

    public void AddScore(int points)
    {
        currentScore += points;
        if (currentScore > highscore)
        {
            highscore = currentScore;
            SaveHighscore();
        }
        TriggerScoreEffects();
        UpdateScoreUI();
        PlayCollectBeerSound();
    }

    public void SubtractScore(int points)
    {
        currentScore -= points;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        currentScoreText.text = "Score: " + currentScore;
        highscoreText.text = "Highscore: " + highscore;
    }

    private void SaveHighscore()
    {
        PlayerPrefs.SetInt("Highscore", highscore);
        PlayerPrefs.Save();
    }

    private void LoadHighscore()
    {
        highscore = PlayerPrefs.GetInt("Highscore", 0);
    }

    private void TriggerScoreEffects()
    {
        // Kích hoạt Particle System
        scoreParticleSystem.Play();

        // Kích hoạt hiệu ứng nảy lên của Text Score
        scoreAnimator.SetTrigger("ScoreBounce");
    }
    
    private void PlayCollectBeerSound()
    {
        // Phát âm thanh nhặt được bia
        audioSource.PlayOneShot(collectBeerSound);
    }
}