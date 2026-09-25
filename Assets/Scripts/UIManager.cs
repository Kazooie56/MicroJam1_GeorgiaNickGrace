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

    private int score;
    public TextMeshProUGUI scoreText;

    void Awake()
    {
        Instance = this;
    }

    //// I'm toggling this off for testing, should be fine to keep off if we remember to uncomment this method after testing or the game begins on the MainMenu
    void Start()
    {
        ShowMainMenu();
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
        GameManager.Instance.ResetGame();

        Time.timeScale = 0f;
    }
    public void ShowGameplay()
    {
        HideAllUI();
        gamePlayUI.SetActive(true);
        gameplayScreen.SetActive(true);

        Time.timeScale = 1f;


        StartCoroutine(updateScore());
    }

    private IEnumerator updateScore()
    {
        yield return new WaitForSeconds(1);

        score += 100;
        scoreText.text = "Score: " + score;
        StartCoroutine(updateScore());
        
    }
}
