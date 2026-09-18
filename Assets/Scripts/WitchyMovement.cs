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

    private RectTransform rectTransform;
    private float currentVelocity = 0f;

    void Start()
    {
        // We need this to have something to move
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

        // rectTransform is needed for objects with Rect Transform like my placeholder Witch.
        // anchoredPosition is the literal anchored position the object has.
        // The Mathf.Clamp prevents it from going out of bounds
        Vector2 currentPosition = rectTransform.anchoredPosition;   
        currentPosition.y += currentVelocity * Time.deltaTime;      
        currentPosition.y = Mathf.Clamp(currentPosition.y, minY, maxY);
        rectTransform.anchoredPosition = currentPosition;

        //// I'm commenting this out but if it's enabled, the witch won't tilt if holding up at the top or down at the bottom.
        //if (currentPosition.y == minY || currentPosition.y == maxY)
        //    currentVelocity = 0f;

        // Tilt based on current speed ratio
        float speedRatio = currentVelocity / maxSpeed; // -1 to 1
        float targetTiltAngle = speedRatio * maxTiltAngle;
        float currentAngle = rectTransform.eulerAngles.z;   // rotating on the z axis makes it properly tilt

        // we need to do this because of how unity handles rotations
        if (currentAngle > 180f) currentAngle -= 360f;

        float newAngle = Mathf.MoveTowards(currentAngle, targetTiltAngle, tiltSpeed * Time.deltaTime);
        rectTransform.eulerAngles = new Vector3(0f, 0f, newAngle);
    }
}