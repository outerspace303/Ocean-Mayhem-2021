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
    [SerializeField] private GameObject explosionVFX;
    [SerializeField] private GameObject playerShip;
    [SerializeField] private MeshRenderer[] meshRenderers;
    

    private void OnCollisionEnter(Collision other)
    {
        StartCoroutine(HandleDeath());
    }

    private IEnumerator HandleDeath()
    {
       _playerControls.enabled = false;
       foreach (var meshRenderer in meshRenderers)
       {
           meshRenderer.enabled = false;
       }
       Instantiate(explosionVFX, playerShip.transform.position, Quaternion.identity);
       yield return new WaitForSeconds(reloadDelay);
        SceneManager.LoadScene("Level 1");
    }
    
}
