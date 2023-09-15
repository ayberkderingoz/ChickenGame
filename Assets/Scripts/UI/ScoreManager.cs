using System;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

      public enum ScoreType
    {
        Worm,
        Gold,
        Egg
    }
    public static ScoreManager Instance { get; private set; }
    private Dictionary<ScoreType, int> _scores;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
        _scores = new Dictionary<ScoreType, int>();
        foreach (ScoreType scoreType in Enum.GetValues(typeof(ScoreType)))
        {
            _scores.Add(scoreType,0);
        }
    }

    public int GetScore(ScoreType type)
    {
        return _scores[type];
    }
    public void UpdateScore(ScoreType type,int newScore)
    {
        _scores[type] = newScore;
        OnScoreChanged?.Invoke(type, _scores[type]);
    }
    private int _score;
    
    public Action<ScoreType,int> OnScoreChanged;
    public Action<ScoreType> OnScoreReset;
    
    public void ResetScore(ScoreType type)
    {
        _scores[type] = 0;
        OnScoreReset?.Invoke(type);
    }
    

}
