using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{

    [SerializeField] private float timeUntilDestruction = 3f;
    
    private void Start()
    {
        Destroy(gameObject, timeUntilDestruction);
    }
    
}
