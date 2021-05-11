using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
    [SerializeField] private GameObject hitCrosshair;

    private void OnParticleCollision(GameObject other)
    {
        if (!other.transform.gameObject.CompareTag("Enemy")) return;
        StartCoroutine(SetNewCrosshair());
    }

    private IEnumerator SetNewCrosshair()
    {
        hitCrosshair.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        hitCrosshair.SetActive(false);
    }
}
