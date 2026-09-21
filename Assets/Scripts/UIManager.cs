using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject instructionsScreenUI;
    public GameObject gamePlayUI;
    public GameObject pauseScreenUI;
    public GameObject gameOverScreenUI;
   
    void Start()
    {
        ShowMainMenu();
    }

    public void HideAllUI()
    {
        mainMenuUI.SetActive(false);
        instructionsScreenUI.SetActive(false);
        gamePlayUI.SetActive(false);
        pauseScreenUI.SetActive(false);
        gameOverScreenUI.SetActive(false);
    }

    public void ShowMainMenu()
    {
        HideAllUI();
        mainMenuUI.SetActive(true);
    }

    public void ShowInstructions()
    {
        HideAllUI();
        instructionsScreenUI.SetActive(true);
    }

    public void ShowGamePlayUI()
    {
        HideAllUI();
        gamePlayUI.SetActive(true);
    }

    public void ShowPauseScreenUI()
    {
        HideAllUI();
        pauseScreenUI.SetActive(true);
    }

    public void ShowGameOverScreenUI()
    {
        HideAllUI();
        gameOverScreenUI.SetActive(true);
    }
}
