using UnityEngine;
using UnityEngine.InputSystem;

public class WitchyMovement : MonoBehaviour
{
    [Header("Movement")]
    public float maxSpeed = 1000f;
    public float acceleration = 5000f;
    public float deceleration = 2000f;   // how fast it comes to a stop when no movement is done.


    [Header("Bounds")]
    // incase we need to adjust if we change the screen size.
    public float minY = -250f;
    public float maxY = 250f;

    [Header("Tilt")]
    public float maxTiltAngle = 20f;    // this is in degrees
    public float tiltSpeed = 90f;

    [Header("Hitbox (manual adjustment)")]
    public Vector2 hitboxSize = new Vector2(80f, 120f);
    public Vector2 hitboxOffset = Vector2.zero;

    private RectTransform rectTransform; // Mountain's Rect Transform
    private float currentVelocity = 0f;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        // Determine input direction
        float inputDirection = 0f;
        var keyboard = Keyboard.current;

        if (keyboard != null)
        {
            if (keyboard.wKey.isPressed)
                inputDirection = 1f;
            else if (keyboard.sKey.isPressed)
                inputDirection = -1f;
        }

        // Accelerate toward target speed, or decelerate toward 0 if no input
        float targetVelocity = inputDirection * maxSpeed;

        // when any movement is made
        if (inputDirection != 0f)
        {
            currentVelocity = Mathf.MoveTowards(currentVelocity, targetVelocity, acceleration * Time.deltaTime);
        }
        else
        {
            currentVelocity = Mathf.MoveTowards(currentVelocity, 0f, deceleration * Time.deltaTime);
        }

        Vector2 currentPosition = rectTransform.anchoredPosition;
        currentPosition.y += currentVelocity * Time.deltaTime;
        currentPosition.y = Mathf.Clamp(currentPosition.y, minY, maxY);             // The Mathf.Clamp prevents it from going out of bounds
        rectTransform.anchoredPosition = currentPosition;

        if (currentPosition.y == minY || currentPosition.y == maxY)
            currentVelocity = 0f;

        // Tilt based on current speed ratio
        float speedRatio = currentVelocity / maxSpeed; // -1 to 1
        float targetTiltAngle = speedRatio * maxTiltAngle;
        float currentAngle = rectTransform.eulerAngles.z;   // rotating on the z axis makes it properly tilt

        // we need to do this because of how unity handles rotations
        if (currentAngle > 180f) currentAngle -= 360f;

        float newAngle = Mathf.MoveTowards(currentAngle, targetTiltAngle, tiltSpeed * Time.deltaTime);
        rectTransform.eulerAngles = new Vector3(0f, 0f, newAngle);
    }

    public Rect GetHitboxRect()
    {
        Vector2 center = rectTransform.anchoredPosition + hitboxOffset;
        // x and y start from the bottom left corner
        // hitboxSize is a 2d Vector using updated hitboxes because box colliders are a pain for UI images.
        // since center.x and center.y start from the middle, subtracting the hitbox size divided by 2 gives you the left edge
        // same for y, then width and height are the hitbox size x and y
        return new Rect(center.x - hitboxSize.x / 2f, center.y - hitboxSize.y / 2f, hitboxSize.x, hitboxSize.y);
    }

    public void ResetPosition()
    {
        RectTransform rt = GetComponent<RectTransform>();
        Vector2 position = rt.anchoredPosition;
        position.y = 0f;
        rt.anchoredPosition = position;
    }

    void OnDrawGizmosSelected()
    {
        if (rectTransform == null) rectTransform = GetComponent<RectTransform>();
        Gizmos.color = Color.green;
        Vector3 worldCenter = rectTransform.TransformPoint(hitboxOffset);
        Vector2 worldSize = Vector2.Scale(hitboxSize, rectTransform.lossyScale);
        Gizmos.DrawWireCube(worldCenter, new Vector3(worldSize.x, worldSize.y, 0.1f));
    }
}