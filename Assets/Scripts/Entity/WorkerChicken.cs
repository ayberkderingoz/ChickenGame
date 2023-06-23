using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Entity;
using Spawner;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class WorkerChicken : MonoBehaviour
{
    
    public bool _isCarryingWorm = false;
    private NavMeshAgent _agent;
    private GameObject bigChicken;
    public List<GameObject> _wormList;
    private PooledObject _pooledObject;
    public GameObject _targetWorm;

    

    public void SetPooledObject(PooledObject pooledObject)
    {
        _pooledObject = pooledObject;
        SetAvaliableWorms();
    }
    
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        bigChicken = GameObject.FindGameObjectWithTag("BigChicken");

    }

    //wait then initialize big chicken
    private void SetAvaliableWorms()
    {
        _wormList = WormSpawner.Instance.worms;
    }

    private void Start()
    {
        WormSpawner.Instance.OnWormsChanged += OnWormsChanged;
    }

    
    private void OnWormsChanged(List<GameObject> worms)
    {
        this._wormList = worms;
    }
    private void Update()
    {
        if (_isCarryingWorm)
        {

            MoveToBigChicken();
        }
        else if(_targetWorm == null)
        {
            
            MoveToWorm();
        }
    }
    private void MoveToBigChicken()
    {
        _targetWorm = null;
        _agent.SetDestination(bigChicken.transform.position);
        transform.LookAt(_agent.nextPosition);
    }

    //move to the closest worm
    public void MoveToWorm()
    {
        var closestWorm = GetClosestWormPosition(transform.position);
        _targetWorm = closestWorm;
        _agent.SetDestination(closestWorm.transform.position);
        transform.LookAt(_agent.nextPosition);
        WormSpawner.Instance.RemoveWorm(_targetWorm);
    }


    private GameObject GetClosestWormPosition(Vector3 position)
    {
        
        var closestWorm = _wormList[0];
        foreach (var worm in _wormList)
        {
            if (Vector3.Distance(position, worm.transform.position) <
                Vector3.Distance(position, closestWorm.transform.position))
            {
                closestWorm = worm;
                worm.GetComponent<Worm>().isTargeted = true;

            }
        }
        return closestWorm;
    }

    

    
    public bool IsCarrying()
    {
        return _isCarryingWorm;
    }
    
    //delayed setIscarrying
    public void SetCarrying(bool isCarrying)
    {
        _isCarryingWorm = isCarrying;
    }
    
    
    
    /*private Vector3 GetClosestWormPositionAAA(Vector3 position) //object pooled but don't work
    {
        var worms = ObjectPool.Instance.PoolDictionary[PooledObjectType.Worm].ToArray();
        if (worms.Length == 0)
        {
            return position;
        }
        var closestWorm = worms[0];
        Debug.Log(closestWorm.gameObject.transform.position);
        foreach (var worm in worms)
        {
            if (Vector3.Distance(position, worm.gameObject.transform.position) <
                Vector3.Distance(position, closestWorm.gameObject.transform.position) && worm.gameObject.activeSelf)
            {
                closestWorm = worm;
                Debug.Log(closestWorm.gameObject.transform.position);
            }
        }
        return closestWorm.gameObject.transform.position;
    }*/
}
