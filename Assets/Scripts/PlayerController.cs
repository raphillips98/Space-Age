using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {
    [Header("General")]
    [Tooltip("In meters/second")][SerializeField] float xSpeed = 25f;
    [Tooltip("In meters/second")] [SerializeField] float ySpeed = 25f;

    [Header("control / position")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float controlPitchFactor = -20f;
    [SerializeField] float positionYawFactor = 2f;
    [SerializeField] float controlRollFactor = -20f;

    float xThrow, yThrow;
    bool isControlEnabled = true;

    
    // Update is called once per frame
    void Update ()
    {
        if (isControlEnabled)
        {
            ProcessMovement();
            ProcessRotation();
        }
    }
    

    //Called by string reference
    void OnPlayerDeath()         
    {
        isControlEnabled = false;
    }

    // processes the roll, pitch, and yaw of the ship according to ships position on screen
    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow =  yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlThrow;                      // x-axis

        float yaw = transform.localPosition.x * positionYawFactor;                      // y-axis

        float roll = xThrow * controlRollFactor; ;                                      // z-axis

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessMovement()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = -(CrossPlatformInputManager.GetAxis("Vertical"));
        float xOffset = xThrow * xSpeed * Time.deltaTime;
        float yOffset = yThrow * ySpeed * Time.deltaTime;

        float rawNewXPos = transform.localPosition.x + xOffset;
        float rawNewYPos = transform.localPosition.y + yOffset;

        float clampedXPos = Mathf.Clamp(rawNewXPos, -16, 16);
        float clampedYPos = Mathf.Clamp(rawNewYPos, -11, 11);

        transform.localPosition = new Vector3(clampedXPos, transform.localPosition.y, transform.localPosition.z);
        transform.localPosition = new Vector3(transform.localPosition.x, clampedYPos, transform.localPosition.z);
    }




}
