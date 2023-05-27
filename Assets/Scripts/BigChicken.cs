using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class BigChicken : MonoBehaviour
{
    private int gold { get; set; } = 100;
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var wormCount = CountUI.Instance.GetCount(CountUI.CountType.Worm);
            if (wormCount == 0)
            {
                return;
            }
            
            var multipliedGold = gold * wormCount;
            
            CountUI.Instance.UpdateCount(CountUI.CountType.Gold,multipliedGold);
            CountUI.Instance.ResetCount(CountUI.CountType.Worm);

        }
    }
}
