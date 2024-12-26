using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public TMP_Text timerText; // Tham chiếu đến UI Text để hiển thị thời gian
    public float totalTime = 300f; // Tổng thời gian đếm ngược (300 giây = 5 phút)

    private float remainingTime;

    private void Start()
    {
        remainingTime = totalTime;
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            UpdateTimerText();
            yield return null;
        }

        // Khi hết thời gian, bạn có thể thực hiện hành động cần thiết (ví dụ: kết thúc trò chơi)
        remainingTime = 0;
        UpdateTimerText();
        Debug.Log("Time's up!");
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}