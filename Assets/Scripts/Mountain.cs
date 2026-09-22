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

    // 

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
        Destroy(witch.gameObject);
        Destroy(gameObject);
        // TODO: incorporate menu logic eventually
    }

    // Compares this mountain's hitbox against the witch's hitbox.
    private bool HitboxesOverlap()
    {
        Rect mountainRect = GetHitboxRect(rectTransform.anchoredPosition, hitboxSize, hitboxOffset);

        WitchyMovement witchMovement = witch.GetComponent<WitchyMovement>();
        Rect witchRect = witchMovement.GetHitboxRect();

        return mountainRect.Overlaps(witchRect);
    }

    // Turns a position + size + offset into a Rect centered on that position.
    // This works directly in anchoredPosition space - no world-space conversion needed,
    // since both the mountain and witch live under the same canvas.
    private Rect GetHitboxRect(Vector2 position, Vector2 size, Vector2 offset)
    {
        Vector2 center = position + offset;
        return new Rect(center.x - size.x / 2f, center.y - size.y / 2f, size.x, size.y);
    }

    // Draws the hitbox as a red box in the Scene view whenever this object is selected.
    // NOTE: this uses real world-space coordinates (TransformPoint/lossyScale), NOT
    // anchoredPosition. Gizmos.DrawWireCube always draws in world space, so plugging in
    // raw anchoredPosition numbers only lines up if the Canvas sits at world origin with
    // scale 1 - otherwise the box ends up offset and the wrong size. The actual collision
    // check above (GetHitboxRect/HitboxesOverlap) is unaffected - it still uses the simple
    // anchoredPosition math, which is correct since both objects share the same Canvas.
    void OnDrawGizmosSelected()
    {
        if (rectTransform == null) rectTransform = GetComponent<RectTransform>();
        Gizmos.color = Color.red;
        Vector3 worldCenter = rectTransform.TransformPoint(hitboxOffset);
        Vector2 worldSize = Vector2.Scale(hitboxSize, rectTransform.lossyScale);
        Gizmos.DrawWireCube(worldCenter, new Vector3(worldSize.x, worldSize.y, 0.1f));
    }
}