using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Character;
using Entity;
using UnityEngine;
using Object = UnityEngine.Object;

public class Incubator : MonoBehaviour
{


    private bool _isIncubating;
    [SerializeField] private float _incubationTime;
    private float _timePassed;
    private GameObject _egg;
    [SerializeField] private GameObject _eggParent;
    
    
    
    private void Update()
    {
        if (_isIncubating)
        {
            _timePassed += Time.deltaTime;
            if (_timePassed >= _incubationTime)
            {
                _isIncubating = false;
                _timePassed = 0f;
                _egg.GetComponent<Egg>().SetIncubating(false);
                if (gameObject.CompareTag("WorkerIncubator"))
                {
                    var workerPooledObjet = ObjectPool.Instance.GetPooledObject(PooledObjectType.WorkerChicken);
                    var worker = workerPooledObjet.gameObject;
                    worker.transform.position = transform.position;
                    worker.SetActive(true);
                    worker.GetComponent<WorkerChicken>().SetPooledObject(workerPooledObjet);
                    _egg.transform.SetParent(_eggParent.transform);
                    _egg.GetComponent<Egg>().GetPooledObject().ReturnToPool();

                }

                if (gameObject.CompareTag("SoldierIncubator"))
                {
                    var soldierPooledObject = ObjectPool.Instance.GetPooledObject(PooledObjectType.SoldierChicken);
                    var soldier = soldierPooledObject.gameObject;
                    soldier.transform.position = transform.position;
                    soldier.SetActive(true);
                    soldier.GetComponent<SoldierChicken>().SetPooledObject(soldierPooledObject);
                    _egg.transform.SetParent(_eggParent.transform);
                    _egg.GetComponent<Egg>().GetPooledObject().ReturnToPool();
                }
            }
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (_isIncubating) return;
        if (other.CompareTag("Player"))
        {
            if (Player.Instance.GetEggs().Count > 0)
            {
                var eggs = Player.Instance.GetEggs();
                _egg = eggs.Last().Key;
                _isIncubating = true;
                _egg.transform.SetParent(transform);
                _egg.transform.localPosition = Vector3.zero;
                _egg.GetComponent<Egg>().SetIncubating(true);
                eggs.Remove(_egg);
                Player.Instance.SetEggs(eggs);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(_isIncubating) return;
        if (other.CompareTag("Player"))
        {
            if(Player.Instance.GetEggs().Count>0)
            {
                var eggs = Player.Instance.GetEggs();
                _egg = eggs.Last().Key;
                _isIncubating = true;
                _egg.transform.SetParent(transform);
                _egg.transform.localPosition = Vector3.zero;
                _egg.GetComponent<Egg>().SetIncubating(true);
                eggs.Remove(_egg);
                Player.Instance.SetEggs(eggs);
                
            }
        }
        
    }
}
