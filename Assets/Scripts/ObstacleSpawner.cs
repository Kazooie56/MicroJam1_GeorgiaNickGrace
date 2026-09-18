using System.Collections;
using UnityEngine;

public class MountainSpawner : MonoBehaviour
{
    [Header("References")]
    public GameObject mountainPrefab;
    public RectTransform canvasRect;   // drag your Canvas's RectTransform here
    public RectTransform witch;        // drag the witch's RectTransform here

    [Header("Spawn Timing")]
    public float spawnInterval = 20f;

    [Header("Spawn Position")]
    public float bottomSpawnY = -250f;

    private float spawnX;
    private float destroyX;

    void Start()
    {
        // Spawn just off the right edge, destroy just off the left edge
        float halfWidth = canvasRect.rect.width / 2f;
        spawnX = halfWidth + 100f;
        destroyX = -halfWidth - 100f;

        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            float wait = spawnInterval;
            yield return new WaitForSeconds(wait);
            SpawnMountain();
        }
    }

    void SpawnMountain()
    {
        GameObject obj = Instantiate(mountainPrefab, canvasRect);
        RectTransform rt = obj.GetComponent<RectTransform>();

        rt.anchoredPosition = new Vector2(spawnX, bottomSpawnY);

        Mountain mountain = obj.GetComponent<Mountain>();
        mountain.Init(witch, destroyX);
    }
}