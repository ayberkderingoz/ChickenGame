using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Character;
using Controller;
using Entity;
using UnityEngine;
using Object = UnityEngine.Object;

public class Incubator : MonoBehaviour
{
    private bool _isIncubating;
    [SerializeField] private float _incubationTime;
    private float _timePassed;
    private GameObject _eggObject;
    private Egg _egg;
    [SerializeField] private GameObject _eggParent;


    private void Update()
    {
        if (!_isIncubating) return;

        _timePassed += Time.deltaTime;
        if (!(_timePassed >= _incubationTime)) return;

        _isIncubating = false;
        _timePassed = 0f;
        _egg.SetIncubating(false);

        if (gameObject.CompareTag("WorkerIncubator"))
        {
            var workerPooledObjet = ObjectPool.Instance.GetPooledObject(PooledObjectType.WorkerChicken);
            var worker = workerPooledObjet.gameObject;
            worker.transform.position = transform.position;
            worker.SetActive(true);
            worker.GetComponent<WorkerChicken>().SetAvailableWorms();
            _eggObject.transform.SetParent(_eggParent.transform);
            _egg.GetPooledObject().ReturnToPool();
        }

        if (gameObject.CompareTag("SoldierIncubator")) //TODO: Add count check for every level
        {
            var soldierPooledObject = ObjectPool.Instance.GetPooledObject(PooledObjectType.SoldierChicken);
            var soldier = soldierPooledObject.gameObject;
            soldier.transform.position = transform.position;
            soldier.SetActive(true);
            var soldierChicken = soldier.GetComponent<SoldierChicken>();
            soldierChicken.SetPooledObject(soldierPooledObject);
            soldierChicken.MoveArea();

            SoldierChickenController.Instance.AddSoldier(soldier);
            _eggObject.transform.SetParent(_eggParent.transform);
            EnemyController.Instance.BroadcastEnemies();
            _egg.GetPooledObject().ReturnToPool();
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
                _eggObject = eggs.Last().Key;
                _egg = _eggObject.GetComponent<Egg>();
                _isIncubating = true;
                _eggObject.transform.SetParent(transform);
                _eggObject.transform.localPosition = Vector3.zero;
                _egg.SetIncubating(true);
                eggs.Remove(_eggObject);
                Player.Instance.SetEggs(eggs);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isIncubating) return;
        if (other.CompareTag("Player"))
        {
            if (Player.Instance.GetEggs().Count > 0)
            {
                var eggs = Player.Instance.GetEggs();
                _eggObject = eggs.Last().Key;
                _egg = _eggObject.GetComponent<Egg>();

                _isIncubating = true;
                _eggObject.transform.SetParent(transform);
                _eggObject.transform.localPosition = Vector3.zero;
                _egg.SetIncubating(true);
                eggs.Remove(_eggObject);
                Player.Instance.SetEggs(eggs);
            }
        }
    }
}