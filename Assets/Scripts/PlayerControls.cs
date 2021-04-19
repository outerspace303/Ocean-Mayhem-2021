using System;
using System.Collections;
using System.Collections.Generic;
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

    [Header("Tower")]
    [SerializeField] private GameObject crosshair;
    [SerializeField] private Transform tower;
    [SerializeField]  private float towerAngle;
    [SerializeField] private float towerSpeed;
    [SerializeField] private float clampTowerMin;
    [SerializeField] private float clampTowerMax;

    [Header("Cannon")]
    [SerializeField] private float cannonSpeed;
    [SerializeField] private Transform cannon;
    [SerializeField] private GameObject cannonRound;
    [SerializeField] private float cannonAngle;
    [SerializeField] private float clampCannonMin;
    [SerializeField] private float clampCannonMax;
    
    private Vector3 mousePos;
    private Vector3 targetPos;
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    // Because of Blender: Z - right, left; Y - up, down; X - forward
    // Rotation is affected as well.
    // Nvm learned how to fix that
    
   private void Update()
    {
        ProcessTranslation();
        ProcessShipRotation();
        ProcessFiring();
        HandleCrosshair();
        
        mousePos = Input.mousePosition;
        var mouseCast = cam.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(mouseCast, out hit, Mathf.Infinity))
        {
            targetPos = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            Debug.DrawLine(mousePos, targetPos, Color.blue);
            cannon.LookAt(targetPos);
        }
        else
        {
            var target= cam.ScreenPointToRay(Input.mousePosition).GetPoint(1000);
            cannon.LookAt(target);
        }
    }
   
   private void Start()
     {
         Cursor.visible = false;
     }

   private void HandleCrosshair()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
        }
        crosshair.transform.position = mousePos;
    }

    void ProcessFiring()
    {
        if (Input.GetButton("Fire1"))
        {
            Fire();
        }
        else
        {
            StopFiring();
        }
    }

    void StopFiring()
    {
       cannonRound.SetActive(false);
    }

    void Fire()
    {
        cannonRound.SetActive(true);
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
