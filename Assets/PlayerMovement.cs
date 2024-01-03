using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb; // Rigidbody component reference

    public float maxSpeed = 400.0f; // Maximum speed
    public float decelerationRate = 1f; // Rate of deceleration
    public float baseRotationSpeed = 10.0f; // Base rotation speed

    private float currentSpeed = 0.0f; // Current speed, starts at 0
    private float t = 5.0f; // Time variable for exponential acceleration

    public float minSpeedForRotation = 5.0f; // Minimum speed to start rotating
    public float rotationSpeedFactor = 0.5f; // Factor to adjust rotation based on speed

    private float horizontalInput; // Horizontal input
    private float verticalInput; // Vertical input

    public Text speedDisplay;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component

        if (speedDisplay != null)
        {
            speedDisplay.text = "Speed: 0";
        }
    }

    void Update()
    {
        // Read inputs
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        bool isAccelerating = verticalInput > 0;

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


        if (speedDisplay != null)
        {
            speedDisplay.text = "Speed: " + currentSpeed.ToString("F2");
        }

    }

    void FixedUpdate()
    {
        // Apply rotation
        if (horizontalInput != 0 && currentSpeed > minSpeedForRotation)
        {
            float rotationSpeed = baseRotationSpeed +
                                  (Mathf.Clamp(currentSpeed, minSpeedForRotation, maxSpeed) / maxSpeed)
                                   * rotationSpeedFactor
                                   * baseRotationSpeed;

            float turnAngle = horizontalInput * rotationSpeed * Time.fixedDeltaTime;

            // Rotate around the player's local y-axis (up axis)
            Quaternion deltaRotation = Quaternion.Euler(0, turnAngle, 0);
            rb.MoveRotation(rb.rotation * deltaRotation);
        }

        // Apply movement
        Vector3 move = transform.forward * currentSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);
    }

}
