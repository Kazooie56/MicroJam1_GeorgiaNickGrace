using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool IsGameOver;

    public MountainSpawner mountainSpawner;
    public WitchyMovement witch;

    void Awake()
    {
        Instance = this;
    }

    public void OnWitchDied()
    {
        if (IsGameOver)
        {
            return;
        }

        IsGameOver = true;

        mountainSpawner.enabled = false;
        AudioManager.Instance.PlayDieSound();
        AudioManager.Instance.StopMusic();
        AudioManager.Instance.PlayGameOverMusic();
        UIManager.Instance.ShowGameOverScreenUI();
    }

    public void OnRetryPressed()
    {
        AudioManager.Instance.RestartMusic();
        AudioManager.Instance.PlayBackgroundMusic();
        ResetGame();
        UIManager.Instance.ShowGameplay();
    }

    public void OnGameOverMainMenuPressed()
    {
        AudioManager.Instance.PlayBackgroundMusic();
        UIManager.Instance.ShowMainMenu();
    }

    public void ResetGame()
    {
        IsGameOver = false;
        mountainSpawner.ClearMountainList();
        witch.ResetPosition();
        witch.gameObject.SetActive(true);
        mountainSpawner.enabled = true;
        ScoreManager.Instance.ResetScore();
    }
}