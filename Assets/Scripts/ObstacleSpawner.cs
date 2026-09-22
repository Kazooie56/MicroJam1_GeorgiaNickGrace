using System.Collections;
using UnityEngine;

public class MountainSpawner : MonoBehaviour
{
    [Header("References")]
    public GameObject mountainPrefab;
    public RectTransform canvasRect;   // drag your Canvas's RectTransform here
    public RectTransform witch;        // drag the witch's RectTransform here

    [Header("Spawn Timing")]
    public float minimumSpawnInterval = 5f;
    public float maximumSpawnInterval = 5f;

    [Header("Spawn Position")]
    public float bottomSpawnY = -250f;
    public float topSpawnY = 250f;

    private float spawnX;
    private float destroyX;
    private readonly float upsideDownChance = 0.5f;

    void Start()
    {
        // Spawn just off the right edge, destroy just off the left edge
        float halfWidth = canvasRect.rect.width / 2f;
        spawnX = halfWidth + 300f;
        destroyX = -halfWidth - 300f;

        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            float wait = minimumSpawnInterval;             // include a math formula that adapts the score later
            yield return new WaitForSeconds(wait);
            SpawnMountain();
        }
    }

    void SpawnMountain()
    {
        GameObject obj = Instantiate(mountainPrefab, canvasRect);
        RectTransform rt = obj.GetComponent<RectTransform>();

        bool spawnUpsideDown = Random.value < upsideDownChance;

        if (spawnUpsideDown)
        {
            rt.anchoredPosition = new Vector2(spawnX, topSpawnY);
            rt.localRotation = Quaternion.Euler(0f, 0f, 180f);
        }
        else
        {
            rt.anchoredPosition = new Vector2(spawnX, bottomSpawnY);
            rt.localRotation = Quaternion.identity; // make sure it resets if the prefab isn't already upright
        }

        Mountain mountain = obj.GetComponent<Mountain>();
        mountain.Init(witch, destroyX);
    }
}