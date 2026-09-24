using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainSpawner : MonoBehaviour
{
    [Header("References")]
    public GameObject mountainPrefab;
    public RectTransform canvasRect;   // drag your Canvas's RectTransform here
    public RectTransform gameplayScreen; // drag gameplay here so the mountains are children here.
    public RectTransform witch;        // drag the witch's RectTransform here

    [Header("Randomness")]
    public float minimumSpawnInterval = 4f;
    public float maximumSpawnInterval = 6f; // maybe make the difference smaller like pokemon damage variance.
    public float minSize = 0.8f;
    public float maxSize = 1.2f;
    private readonly float upsideDownChance = 0.5f;

    [Header("Spawn Position")]
    public float bottomSpawnY = -250f;
    public float topSpawnY = 250f;

    private float spawnX;
    private float destroyX;

    private readonly List<GameObject> activeMountains = new List<GameObject>();

    private void OnEnable()
    {
        // need to do this otherwise it's broken on Start after swtiching screens
        StartCoroutine(SpawnLoop());
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    void Start()
    {
        // Spawn just off the right edge, destroy just off the left edge
        float halfWidth = canvasRect.rect.width / 2f;
        spawnX = halfWidth + 300f;
        destroyX = -halfWidth - 300f;
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            float spawnInterval = Random.Range(minimumSpawnInterval, maximumSpawnInterval);             // include a math formula that adapts the score later
            yield return new WaitForSeconds(spawnInterval);
            SpawnMountain();
        }
    }

    void SpawnMountain()
    {
        GameObject obj = Instantiate(mountainPrefab, gameplayScreen, false);
        activeMountains.Add(obj);

        RectTransform rt = obj.GetComponent<RectTransform>();

        bool spawnUpsideDown = Random.value < upsideDownChance;
        float randomSize = Random.Range(minSize, maxSize);

        if (spawnUpsideDown)
        {
            rt.anchoredPosition = new Vector2(spawnX, topSpawnY);
            rt.localScale = new Vector3(randomSize, -randomSize, 1f);
        }
        else
        {
            rt.anchoredPosition = new Vector2(spawnX, bottomSpawnY);
            rt.localScale = new Vector3(randomSize, randomSize, 1f);
        }

        Mountain mountain = obj.GetComponent<Mountain>();
        mountain.Init(witch, destroyX);
    }

    public void RemoveMountainFromList(GameObject mountain)
    {
        activeMountains.Remove(mountain);
    }

    public void ResetSpawner()
    {
        foreach (GameObject mountain in activeMountains)
        {
            if (mountain != null)
                Destroy(mountain);
        }
        activeMountains.Clear();
    }
}