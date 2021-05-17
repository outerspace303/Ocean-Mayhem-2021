using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private PlayerControls _playerControls;
    [SerializeField] private float reloadDelay = 1f;
    [SerializeField] private ParticleSystem explosionVFX;
    [SerializeField] private GameObject playerShip;
    [SerializeField] private MeshRenderer[] meshRenderers;

    private MeshCollider meshCollider;

    private void Start()
    {
        meshCollider = GetComponent<MeshCollider>();
    }


    private void OnCollisionEnter(Collision other)
    {
       HandleDeath();
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene("Level 1");
    }
    
    

    private void HandleDeath()
    {
        explosionVFX.Play();
        _playerControls.enabled = false;
        meshCollider.enabled = false;
        foreach (var meshRenderer in meshRenderers)
        {
            meshRenderer.enabled = false;
        }

        Invoke(nameof(ReloadLevel), reloadDelay);

    }
    
}
