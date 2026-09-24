using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public MountainSpawner mountainSpawner;
    public WitchyMovement witch;
    //public ScoreManager scoreManager;   // pray

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void ResetGame()
    {
        // Reset score
        mountainSpawner.ResetSpawner();
        witch.ResetPosition();
    }
}