using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CarController : MonoBehaviour
{
    private float horizontalInput, verticalInput;
    private float currentSteerAngle, currentbreakForce;
    private bool isBreaking;

    private Material GetMaterialByIndex(int index)
    {

        var roomController = FindObjectOfType<PUN2_RoomController>();
        switch (index)
        {
            case 0: return roomController.material_player_one;
            case 1: return roomController.material_player_two;
            case 2: return roomController.material_player_three;
            case 3: return roomController.material_player_four;
            case 4: return roomController.material_player_five;
            default: return null;
        }
    }

    [PunRPC]
    public void ChangeMaterial(int materialIndex)
    {
        Material material = GetMaterialByIndex(materialIndex);
        Renderer bodyRenderer = transform.Find("Body").GetComponent<Renderer>();
        if (bodyRenderer != null)
        {
            bodyRenderer.material = material;
        }
        else
        {
            Debug.LogError("'Body' child not found or Renderer component missing.");
        }

    }

    // Settings
    [SerializeField] private float motorForce, breakForce, maxSteerAngle;

    private Rigidbody rb;
    // Wheel Colliders
    [SerializeField] private WheelCollider frontLeftWheelCollider, frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider, rearRightWheelCollider;

    // Wheels
    [SerializeField] private Transform frontLeftWheelTransform, frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform, rearRightWheelTransform;

    public Camera carFontViewCamera;
    public Camera carBackViewCamera;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            carFontViewCamera.enabled = false;
            carBackViewCamera.enabled = true;
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            carFontViewCamera.enabled = true;
            carBackViewCamera.enabled = false;
        }

        UpdateSpeedDisplay();
    }

    private void UpdateSpeedDisplay()
    {
        GameObject speedTextObject = GameObject.Find("SpeedText");
        Text speedText = speedTextObject.GetComponent<Text>();
        if (speedTextObject != null)
        {
            if (speedText != null)
            {
                float speed = rb.velocity.magnitude * 3.6f;
                speedText.text = "Speed: " + speed.ToString("F2") + " km/h";
            }
        }
    }

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();

    }

    private void GetInput()
    {
        // Steering Input
        horizontalInput = Input.GetAxis("Horizontal");

        // Acceleration Input
        verticalInput = Input.GetAxis("Vertical");

        // Breaking Input
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        currentbreakForce = isBreaking ? breakForce : 0f;
        ApplyBreaking();
    }

    private void ApplyBreaking()
    {
        frontRightWheelCollider.brakeTorque = currentbreakForce;
        frontLeftWheelCollider.brakeTorque = currentbreakForce;
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}