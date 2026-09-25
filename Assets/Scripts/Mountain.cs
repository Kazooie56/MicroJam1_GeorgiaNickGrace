using UnityEngine;

public class Mountain : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 250f; // Eventually sync up with score at a later point, for now, public float
    private float score = 1f; // placeholder
    private float scorescaling = 1f; //placeholder

    [Header("Hitbox (manual adjustment)")]
    public Vector2 hitboxSize = new Vector2(150f, 150f);
    public Vector2 hitboxOffset = Vector2.zero;

    private RectTransform rectTransform;
    private float offscreenDeletion = -1500f; // set by spawner based on screen bounds, may need updating with new assets
    private RectTransform witch;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Prefab can't know about the witch
    public void Init(RectTransform witchTarget, float offscreenX)
    {
        witch = witchTarget;
        offscreenDeletion = offscreenX;
    }

    void Update()
    {
        // Move left
        Vector2 position = rectTransform.anchoredPosition;
        position.x -= speed * (score / scorescaling) * Time.deltaTime;
        rectTransform.anchoredPosition = position;

        // Clean up once off-screen
        if (position.x < offscreenDeletion)
        {
            Destroy(gameObject);
            return;
        }

        // Check overlap with the witch
        if (witch != null && HitboxesOverlap())
        {
            OnHitWitch();
        }
    }

    void OnHitWitch()
    {
        //if (GameManager.Instance.IsGameOver)
        //{
        //  return;
        //}

        // we need to set it to false because if we destroy it and retry, the game crashes
        witch.gameObject.SetActive(false);
        GameManager.Instance.OnWitchDied();
    }

    // Compares this mountain's hitbox against the witch's hitbox.
    private bool HitboxesOverlap()
    {
        Rect mountainRect = GetHitboxRect(rectTransform.anchoredPosition, hitboxSize, hitboxOffset);

        WitchyMovement witchMovement = witch.GetComponent<WitchyMovement>();
        Rect witchRect = witchMovement.GetHitboxRect();

        return mountainRect.Overlaps(witchRect);
    }

    private Rect GetHitboxRect(Vector2 position, Vector2 size, Vector2 offset)
    {
        // because we have random mountain size now, these STUPID MOUNTAINS NEED THEIR SIZE INVOLVED IN THE CALCULATIONS
        // I HAVE TO USE ABSOLUTE BECAUSE THIS IS ABSOLUTELY STUPID (and we're multiplying it by negatives for upside down mountains)
        // also making rects with negatives doesn't work right

        Vector2 scale = rectTransform.localScale;
        Vector2 scaledSize = new Vector2(size.x * Mathf.Abs(scale.x), size.y * Mathf.Abs(scale.y));
        Vector2 scaledOffset = new Vector2(offset.x * scale.x, offset.y * scale.y);
        Vector2 center = position + scaledOffset;
        return new Rect(center.x - scaledSize.x / 2f, center.y - scaledSize.y / 2f, scaledSize.x, scaledSize.y);
    }


    // We don't need this below method, the hitboxes are fine and we are done changing it.

    void OnDrawGizmosSelected()
    {
        if (rectTransform == null) rectTransform = GetComponent<RectTransform>();
        Gizmos.color = Color.red;
        Vector3 worldCenter = rectTransform.TransformPoint(hitboxOffset);
        Vector2 worldSize = Vector2.Scale(hitboxSize, rectTransform.lossyScale); // lossyScale gives you the most accurate space, it checks out the parent object's scales too.
        Gizmos.DrawWireCube(worldCenter, new Vector3(worldSize.x, worldSize.y, 0.1f));
    }
}