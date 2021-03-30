using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private float controlSpeed = 10f;
    [SerializeField] private float xRange = 5f;
    [SerializeField] private float zRange = 5f;
    
    // Because of Blender: Z - right, left; Y - up, down; X - forward
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
    }

    private void ProcessRotation()
    {
        //transform.localRotation = Quaternion.Euler();
    }

    void ProcessTranslation()
    {
        float xThrow = Input.GetAxis("Vertical");
        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float zThrow = Input.GetAxis("Horizontal");
        float zOffset = zThrow * Time.deltaTime * controlSpeed;
        float rawZPos = transform.localPosition.z + zOffset;
        float clampedZPos = Mathf.Clamp(rawZPos, -zRange, zRange);

        transform.localPosition =
            new Vector3(
                clampedXPos, transform.localPosition.y, clampedZPos);
    }
}
