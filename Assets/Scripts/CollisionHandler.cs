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
    [SerializeField] AudioClip deathSFX;

    private AudioSource audioSource;

    private MeshCollider meshCollider;

    private void Start()
    {
        meshCollider = GetComponent<MeshCollider>();
        audioSource = GetComponent<AudioSource>();
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
        audioSource.Play();
        _playerControls.enabled = false;
        meshCollider.enabled = false;
        foreach (var meshRenderer in meshRenderers)
        {
            meshRenderer.enabled = false;
        }

        Invoke(nameof(ReloadLevel), reloadDelay);

    }
    
}
