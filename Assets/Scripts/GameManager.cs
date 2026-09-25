using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool IsGameOver;

    public MountainSpawner mountainSpawner;
    public WitchyMovement witch;
    public UIManager UIManager;
    //public ScoreManager scoreManager;   // pray

    void Awake()
    {
        Instance = this;
    }

    public void OnWitchDied()
    {
        if (IsGameOver) return;
        IsGameOver = true;

        mountainSpawner.enabled = false;
        AudioManager.Instance.PlayThud();
        AudioManager.Instance.StopMusic();
        UIManager.Instance.ShowGameOverScreenUI();
    }

    public void OnRetryPressed()
    {
        AudioManager.Instance.RestartMusic();
        GameManager.Instance.ResetGame();
        UIManager.Instance.ShowGameplay();
    }

    public void OnGameOverMainMenuPressed()
    {
        AudioManager.Instance.RestartMusic();
        UIManager.Instance.ShowMainMenu(); // already calls GameManager.Instance.ResetGame() internally
    }

    public void ResetGame()
    {
        IsGameOver = false;
        mountainSpawner.ResetSpawner();
        witch.ResetPosition();
        witch.gameObject.SetActive(true); // re-enable after death
        mountainSpawner.enabled = true;

        // scoreManager.ResetScore();
    }
}