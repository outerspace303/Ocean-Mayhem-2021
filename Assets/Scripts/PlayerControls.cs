using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro.EditorUtilities;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("Ship")]
    [SerializeField] private float controlSpeed = 10f;
    [SerializeField] private float xRange = 5f;
    [SerializeField] private float zRange = 5f;
    [SerializeField] private float controlRollFactor = 2f;
    [SerializeField] private float positionYawFactor = -5f;
    float xThrow, zThrow;

    [Header("Cannon")]
    [SerializeField] private GameObject crosshair;
    [SerializeField] private GameObject hitCrosshair;
    [SerializeField] private Transform cannon;
    [SerializeField] private GameObject cannonRound;
    private ParticleSystem cannonParticleSystem;

    private Vector3 mousePos;
    private Vector3 targetPos;
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }
    
    private void Update()
    {
        ProcessTranslation();
        ProcessShipRotation();
        ProcessFiring();
        HandleCrosshair();
        ProcessCannonRotation();
    }

    private void ProcessCannonRotation()
    {
        mousePos = Input.mousePosition;
        
        var target = cam.ScreenPointToRay(Input.mousePosition).GetPoint(500);
        target.y = Mathf.Clamp(target.y, 0f, 150f);
        cannon.LookAt(target);
        
        
       /*//mousePos.y = Mathf.Clamp(mousePos.y, 0, 150);
        var mouseCast = cam.ScreenPointToRay(mousePos);

        if (Physics.Raycast(mouseCast, out var hit, Mathf.Infinity))
        {
          // Debug.Log("Physic target spotted");
            targetPos = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            Debug.DrawLine(mousePos, targetPos, Color.blue);
            
            cannon.LookAt(targetPos);
        }
        else
        {*/
          // Debug.Log("Skybox spotted");
           


     //  }
    }

    private void Start()
   {
       cannonParticleSystem = cannonRound.GetComponent<ParticleSystem>();
       Cursor.visible = false;
   }

   private void HandleCrosshair()
   {
       var transformPosition = crosshair.transform.position;
       transformPosition.y = Mathf.Clamp(transformPosition.y, 0, 150);
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
        }
        crosshair.transform.position = mousePos;
        hitCrosshair.transform.position = mousePos;
   }

    void ProcessFiring()
    {
        ActivateCannon(Input.GetButton("Fire1"));
    }

    void ActivateCannon(bool isActive)
    {
        var emissionModule = cannonParticleSystem.emission;
        emissionModule.enabled = isActive;
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
