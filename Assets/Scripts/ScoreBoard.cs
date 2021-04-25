using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    public int score;
    public void IncreaseScore(int amountToIncrease)
    {
        score += amountToIncrease;
    }
    
}
