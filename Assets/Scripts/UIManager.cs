using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject instructionsScreenUI;
    public GameObject gamePlayUI;
    public GameObject pauseScreenUI;
    public GameObject gameOverScreenUI;
    public GameObject gameplayScreen;


   
    //// I'm toggling this off for testing, should be fine to keep off if we remember to uncomment this method after testing or the game begins on the MainMenu
    //void Start()
    //{
    //    ShowMainMenu();
    //}

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

        Time.timeScale = 0f;
    }

    public void ShowInstructions()
    {
        HideAllUI();
        instructionsScreenUI.SetActive(true);

        Time.timeScale = 0f;
    }

    public void ShowGamePlayUI()
    {
        HideAllUI();
        gamePlayUI.SetActive(true);

        Time.timeScale = 1f;
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

        Time.timeScale = 0f;
    }
    public void ShowGameplay()
    {
        HideAllUI();
        gamePlayUI.SetActive(true);
        gameplayScreen.SetActive(true);

        Time.timeScale = 1f;
    }
}
