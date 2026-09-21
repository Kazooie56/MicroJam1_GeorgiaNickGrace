using UnityEngine;

public class Mountain : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 200f; // Eventually sync up with score at a later point, for now, public float

    [Header("Collision")]
    public RectTransform witch; // assigned by the spawner when this mountain is created

    private RectTransform rectTransform;
    private float offscreenDeletion = -1100f; // set by spawner based on screen bounds, may need updating with new assets

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void Init(RectTransform witchTarget, float offscreenX)
    {
        witch = witchTarget;
        offscreenDeletion = offscreenX;
    }

    void Update()
    {
        // Move left
        Vector2 pos = rectTransform.anchoredPosition;
        pos.x -= speed * Time.deltaTime;
        rectTransform.anchoredPosition = pos;

        // Clean up once off-screen
        if (pos.x < offscreenDeletion)
        {
            Destroy(gameObject);
            return;
        }

        // Check overlap with the witch
        if (witch != null && RectOverlaps(rectTransform, witch))
        {
            OnHitWitch();
        }
    }

    void OnHitWitch()
    {
        Destroy(witch.gameObject);
        Destroy(gameObject);
        // TODO: incorporate menu logic eventually
    }

    // Checks whether two RectTransforms' bounding boxes overlap in world space
    private bool RectOverlaps(RectTransform a, RectTransform b)
    {
        Rect rectA = GetWorldRect(a);
        Rect rectB = GetWorldRect(b);
        return rectA.Overlaps(rectB);
    }

    private Rect GetWorldRect(RectTransform rt)
    {
        Vector3[] corners = new Vector3[4];
        rt.GetWorldCorners(corners);
        // corners[0] = bottom-left, corners[2] = top-right
        return new Rect(corners[0].x, corners[0].y, corners[2].x - corners[0].x, corners[2].y - corners[0].y);
    }
}