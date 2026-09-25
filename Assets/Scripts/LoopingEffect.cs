using UnityEngine;

public class LoopingEffect : MonoBehaviour
{
    [Header("Backgrounds")]
    public RectTransform background1;
    public RectTransform background2;

    [Header("Settings")]
    public float scrollSpeed = 125f;
    public float backgroundScoreScaling = 0.0001f;
    public float imageWidth = 2732.446f; // specific size of the canvas

    // This is how far to the left the object is. 
    // even though left would make it negative, we 
    private float offset;

    void Update()
    {
        // offset is a number we increase equally to the 
        // speed we move the background images
        float backgroundSpeed = scrollSpeed * (ScoreManager.Instance.score * backgroundScoreScaling);
        offset += backgroundSpeed * Time.deltaTime;

        // When it reaches the very edge of the screen, restart the process
        if (offset >= imageWidth)
        {
            offset = 0;
        }

        // This is way easier than having it run individually twice
        background1.anchoredPosition = new Vector2(-offset, background1.anchoredPosition.y);
        background2.anchoredPosition = new Vector2(-offset + imageWidth, background2.anchoredPosition.y);
    }
}