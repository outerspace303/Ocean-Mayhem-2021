using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private void Awake()
    {
       var numOfMusicPlayer =  FindObjectsOfType<MusicPlayer>().Length;
       if (numOfMusicPlayer > 1)
       {
           Destroy(gameObject);
       }
       else
       {
           DontDestroyOnLoad(gameObject);
       }
    }
}
