using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    //create static variable to store score
    [SerializeField]private TextMeshPro score;
    //singleton
    public static ScoreManager Instance { get; private set; }

    // Use this for initialization
    void Awake()
    {
        Instance = this;
        score.text = "0";
    }

    public void UpdateScore()
    {
        //update score text
        score.text = ""+(int.Parse(score.text) + 1);
    }
    void ResetScore()
    {
        //reset score to 0
        score.text = "0";
    }
}
