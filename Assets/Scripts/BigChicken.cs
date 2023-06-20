using System;
using System.Collections;
using System.Collections.Generic;
using Entity;
using UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class BigChicken : MonoBehaviour
{
    private int gold = 100;
    [SerializeField]private int _progressAmount = 10;
    [SerializeField] private Slider progressBar;
    private bool _isEggDropped = false;
    

    
    //singleton
    private static BigChicken _instance;
    public static BigChicken Instance => _instance;
    
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var wormCount = ScoreManager.Instance.GetScore(ScoreManager.ScoreType.Worm);
            if (wormCount == 0)
            {
                return;
            }
            UpdateProgressBar(wormCount);
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
                UpdateProgressBar(1);
                other.gameObject.GetComponent<WorkerChicken>().SetCarrying(false);
            }
        }
    }


    private void UpdateProgressBar(int wormCount)
    {
        for (int i = 0; i < wormCount; i++)
        {
            progressBar.value += progressBar.maxValue/_progressAmount;
            if(progressBar.value >= progressBar.maxValue)
            {
                progressBar.value = 0;
                LayEgg();
            }
        }
    }

    private void LayEgg()
    {
        var position = transform.position;
        var randomPosition = position + new Vector3(UnityEngine.Random.Range(-5, 5)+5, 0,
            UnityEngine.Random.Range(-5, 5)+5);
        
        var eggPooledObject = ObjectPool.Instance.GetPooledObject(PooledObjectType.Egg);
        var egg = eggPooledObject.gameObject;
        egg.GetComponent<Egg>().SetPooledObject(eggPooledObject);


        DropSmoothlyToLocation(egg,randomPosition);
        egg.SetActive(true);
        
    }
    private void DropSmoothlyToLocation(GameObject egg,Vector3 position)
    {
        StartCoroutine(DropSmoothly(egg,position));
        
    }
    private IEnumerator DropSmoothly(GameObject egg,Vector3 position)
    {
        egg.GetComponent<Egg>().SetPickable(false);
        var position1 = transform.position;
        var startPosition = new Vector3(position1.x, position1.y + 3, position1.z);
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime;
            egg.transform.position = Vector3.Lerp(startPosition, position, t);
            yield return null;
        }
        egg.GetComponent<Egg>().SetPickable(true);
    }

}
