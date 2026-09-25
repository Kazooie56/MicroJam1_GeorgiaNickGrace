using UnityEngine;
using TMPro;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; // Manages HighScore too

    [Header("Settings")]
    public int scorePerTick = 100;
    public float updateTime = 1f;

    [Header("References")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI highScoreText;
    public GameObject newRecordText;

    private string highScoreKey = "HighScore";

    private Coroutine scoreRoutine;
    public int score;               // Only public for access level.

    void Awake()
    {
        Instance = this;
    }
    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }
    public void UpdateFinalScoreText()
    {
        finalScoreText.text = "Final Score: " + score;
    }
    private IEnumerator UpdateScoreLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(updateTime);
            score += scorePerTick;
            UpdateScoreText();
        }
    }
    public void StartScoring()
    {
        StopScoring();
        scoreRoutine = StartCoroutine(UpdateScoreLoop());
    }

    public void StopScoring()
    {
        if (scoreRoutine != null)
        {
            StopCoroutine(scoreRoutine);
            scoreRoutine = null;
        }
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
    }

    public void UpdateHighScore()
    {
        // checks if we have a highscore
        bool hasHighScore = PlayerPrefs.HasKey(highScoreKey);
        // sets it, if there's none, then it's 0
        int highScore = PlayerPrefs.GetInt(highScoreKey, 0);

        // score becomes highScore if higher, save it and make it true for next play session
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt(highScoreKey, highScore);
            PlayerPrefs.Save();
            hasHighScore = true;

            // shows the new record text?
            if (newRecordText != null)
            {
                newRecordText.SetActive(true);
            }
        }
        else
        {
            if (newRecordText != null)
            {
                newRecordText.SetActive(false);
            }
        }

        highScoreText.text = hasHighScore ? "High Score: " + highScore : "";
    }
}