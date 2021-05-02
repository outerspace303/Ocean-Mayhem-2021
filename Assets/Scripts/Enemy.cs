using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject deathVFX;
    [SerializeField] private GameObject hitVFX;
    [SerializeField] private Transform parent;
    [SerializeField] private int howMuchScorePerHit= 50;
    [SerializeField] private int health = 200;
    [SerializeField] private int healthPerHit = 50;

    public bool isHit = false;
    [SerializeField] private GameObject hitCrosshair;

    private ScoreBoard _scoreBoard;

    private void Start()
    {
        _scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    private void OnParticleCollision(GameObject other)
    {
        GameObject hitVfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        hitVfx.transform.parent = parent;
        HitEnemy();
    }

    private IEnumerator SetNewCrosshair()
    {
        hitCrosshair.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        hitCrosshair.SetActive(false);
    }

    private void HitEnemy()
    {
        StartCoroutine(SetNewCrosshair());
        isHit = true;
        health -= healthPerHit;
        _scoreBoard.IncreaseScore(howMuchScorePerHit);
        if (health <= 0)
        {
            hitCrosshair.SetActive(false);
            KillEnemy();
        }
    }

    private void KillEnemy()
    {
        GameObject deathVfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
        deathVfx.transform.parent = parent;
        Destroy(gameObject);
    }
}
