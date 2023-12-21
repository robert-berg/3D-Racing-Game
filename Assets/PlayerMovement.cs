using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb; // Rigidbody

    public float maxSpeed = 400.0f;
    public float decelerationRate = 1f; // Rate of deceleration
    public float baseRotationSpeed = 10.0f; // Base rotation speed

    private float currentSpeed = 0.0f; // Current speed starts at 0
    private float t = 5.0f; // Time variable for exponential acceleration

    public float minSpeedForRotation = 5.0f; // Minimum speed to start rotating
    public float rotationSpeedFactor = 0.5f; // Factor to adjust rotation based on speed

    private float logTimer = 0.0f; // Timer for log frequency
    public float logInterval = 0.2f; // Interval for logging

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        bool isAccelerating = moveVertical > 0;

        // Accelerate or decelerate
        if (isAccelerating)
        {
            t += Time.deltaTime; // Increment time
            float newSpeed = Mathf.Pow(2, t); // Calculate new speed using exponential function
            currentSpeed = Mathf.Min(newSpeed, maxSpeed); // Cap the speed at maxSpeed
        }
        else
        {
            if (currentSpeed > 0)
            {
                float targetSpeed = 0.0f; // Target speed is 0 when not accelerating
                currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, decelerationRate * Time.deltaTime);
            }
            t = 5.0f; // Reset time when not accelerating
        }

        // Logging with controlled frequency
        logTimer += Time.deltaTime;
        if (logTimer >= logInterval)
        {
            Debug.Log("Current Speed: " + currentSpeed);
            logTimer = 0.0f; // Reset the timer
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        // Apply rotation
        if (moveHorizontal != 0 && currentSpeed > minSpeedForRotation)
        {
            float rotationSpeed = baseRotationSpeed + (Mathf.Clamp(currentSpeed, minSpeedForRotation, maxSpeed) / maxSpeed) * rotationSpeedFactor * baseRotationSpeed;
            float turnAngle = moveHorizontal * rotationSpeed * Time.fixedDeltaTime;
            Quaternion deltaRotation = Quaternion.Euler(0, turnAngle, 0);
            rb.MoveRotation(rb.rotation * deltaRotation);
        }

        // Apply movement
        Vector3 move = transform.forward * currentSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);
    }
}
