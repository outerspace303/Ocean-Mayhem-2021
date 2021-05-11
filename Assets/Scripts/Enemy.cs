using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Effects")]
    [SerializeField] private GameObject deathVFX;
    [SerializeField] private GameObject hitVFX;
    
    [Header("Stats")]
    [SerializeField] private int howMuchScorePerHit= 50;
    [SerializeField] private int health = 200;
    [SerializeField] private int damagePerHit = 50;
    
    [SerializeField] private GameObject parentGameObject;
    

    private ScoreBoard _scoreBoard;

    private void Start()
    {
        _scoreBoard = FindObjectOfType<ScoreBoard>();
        parentGameObject = GameObject.FindWithTag("Spawn At Runtime");
    }

    private void OnParticleCollision(GameObject other)
    {
        var hitVfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        hitVfx.transform.parent = parentGameObject.transform;
        HitEnemy();
    }

   

    private void HitEnemy()
    {
        health -= damagePerHit;
        _scoreBoard.IncreaseScore(howMuchScorePerHit);
        if (health <= 0)
        {
            KillEnemy();
        }
    }

    private void KillEnemy()
    {
        var deathVfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
        deathVfx.transform.parent = parentGameObject.transform;
        Destroy(gameObject);
    }
}
