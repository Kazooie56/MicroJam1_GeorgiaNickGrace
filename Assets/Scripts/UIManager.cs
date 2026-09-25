using UnityEngine;
using TMPro;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject mainMenuUI;
    public GameObject instructionsScreenUI;
    public GameObject gamePlayUI;
    public GameObject pauseScreenUI;
    public GameObject gameOverScreenUI;
    public GameObject gameplayScreen;

    public int score;
    public TextMeshProUGUI scoreText;
    private Coroutine scoreRoutine;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ShowMainMenu();
        ResetScore();
    }

    public void StopScoring()
    {
        if (scoreRoutine != null)
        {
            StopCoroutine(scoreRoutine);
            scoreRoutine = null;
        }
    }

    private IEnumerator UpdateScoreLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            score += 100;
            scoreText.text = "Score: " + score;
        }
    }

    public void StartScoring()
    {
        StopScoring();
        scoreRoutine = StartCoroutine(UpdateScoreLoop());
    }

    public void ResetScore()
    {
        score = 0;
        scoreText.text = "Score: " + score;
    }

    public void HideAllUI()
    {
        mainMenuUI.SetActive(false);
        instructionsScreenUI.SetActive(false);
        gamePlayUI.SetActive(false);
        pauseScreenUI.SetActive(false);
        gameOverScreenUI.SetActive(false);
        gameplayScreen.SetActive(false);
    }

    public void ShowMainMenu()
    {
        HideAllUI();
        mainMenuUI.SetActive(true);
        GameManager.Instance.ResetGame();

        Time.timeScale = 0f;
    }

    public void ShowInstructions()
    {
        HideAllUI();
        instructionsScreenUI.SetActive(true);

        Time.timeScale = 0f;
    }

    public void ShowPauseScreenUI()
    {
        gamePlayUI.SetActive(false);
        pauseScreenUI.SetActive(true);

        Time.timeScale = 0f;
    }

    public void ShowGameOverScreenUI()
    {
        HideAllUI();
        gameOverScreenUI.SetActive(true);

        ScoreManager.Instance.StopScoring();
        ScoreManager.Instance.UpdateFinalScoreText();
        ScoreManager.Instance.UpdateHighScore();
        Time.timeScale = 0f;
    }
    public void ShowGameplay()
    {
        HideAllUI();
        gamePlayUI.SetActive(true);
        gameplayScreen.SetActive(true);

        Time.timeScale = 1f;
        ScoreManager.Instance.StartScoring();
    }
}
