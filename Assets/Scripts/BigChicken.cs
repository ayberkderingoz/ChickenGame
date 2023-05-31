using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class BigChicken : MonoBehaviour
{
    private int gold { get; set; } = 100;
    
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var wormCount = ScoreManager.Instance.GetScore(ScoreManager.ScoreType.Worm);
            if (wormCount == 0)
            {
                return;
            }
            
            var multipliedGold = gold * wormCount;
            
            var currentGold = ScoreManager.Instance.GetScore(ScoreManager.ScoreType.Gold);
            ScoreManager.Instance.UpdateScore(ScoreManager.ScoreType.Gold,currentGold+multipliedGold);
            
            ScoreManager.Instance.ResetScore(ScoreManager.ScoreType.Worm);

        }
        else if (other.CompareTag("Worker"))
        {
            if(other.gameObject.GetComponent<WorkerChicken>().IsCarrying())
            {
                var currentGold = ScoreManager.Instance.GetScore(ScoreManager.ScoreType.Gold);
                ScoreManager.Instance.UpdateScore(ScoreManager.ScoreType.Gold,currentGold+gold);
            }
        }
    }
}
