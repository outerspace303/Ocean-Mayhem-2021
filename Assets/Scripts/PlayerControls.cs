using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("Ship")]
    [SerializeField] private float controlSpeed = 10f;
    [SerializeField] private float xRange = 5f;
    [SerializeField] private float zRange = 5f;
    [SerializeField] private float controlRollFactor = 2f;
    [SerializeField] private float positionYawFactor = -5f;

    [Header("Tower")] 
    [SerializeField] private Transform tower;
    [SerializeField] private float towerSpeed;
    private float towerAngle;
    
    

    float xThrow, zThrow;
    
    // Because of Blender: Z - right, left; Y - up, down; X - forward
    // Rotation is affected as well.
    void Update()
    {
        ProcessTranslation();
        ProcessShipRotation();
        ProcessTowerRotation();

    }

    void ProcessTowerRotation()
    {
       // tower.localPosition = new Vector3(transform.localPosition.x,
      //      transform.localPosition.y , transform.localPosition.z);
        towerAngle += Input.GetAxis("Mouse X") * towerSpeed * -Time.deltaTime;
        towerAngle = Mathf.Clamp(towerAngle, -100, 140);
        tower.localRotation = Quaternion.AngleAxis(towerAngle, -Vector3.up);
    }

    void ProcessShipRotation()
    {
        //X axis
        float roll = zThrow * controlRollFactor;
        float yaw = transform.localPosition.z * positionYawFactor;
        
        transform.localRotation = Quaternion.Euler(roll, yaw,
            transform.localRotation.z);
        
    }

    void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Vertical");
        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        zThrow = Input.GetAxis("Horizontal");
        float zOffset = zThrow * Time.deltaTime * controlSpeed;
        float rawZPos = transform.localPosition.z + zOffset;
        float clampedZPos = Mathf.Clamp(rawZPos, -zRange, zRange);

        transform.localPosition =
            new Vector3(
                clampedXPos, transform.localPosition.y, clampedZPos);
    }
}
