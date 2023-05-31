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
    
    private bool _isCarryingWorm = false;
    private NavMeshAgent _agent;
    [SerializeField] private GameObject bigChicken;
    private List<GameObject> _wormList;
    
    
    
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        
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
        else
        {

            MoveToWorm();
        }
    }
    private void MoveToBigChicken()
    {
        _agent.SetDestination(bigChicken.transform.position);
        transform.LookAt(_agent.nextPosition);
    }

    //move to the closest worm
    public void MoveToWorm()
    {
        var closestWorm = GetClosestWormPosition(transform.position);
        _agent.SetDestination(closestWorm);
        transform.LookAt(_agent.nextPosition);
    }


    private Vector3 GetClosestWormPosition(Vector3 position)
    {
        var closestWorm = _wormList[0];
        foreach (var worm in _wormList)
        {
            if (Vector3.Distance(position, worm.transform.position) <
                Vector3.Distance(position, closestWorm.transform.position))
            {
                closestWorm = worm;
            }
        }
        return closestWorm.transform.position;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Worm"))
        {
            _isCarryingWorm = true;
        }

        if (other.CompareTag("BigChicken"))
        {
            _isCarryingWorm = false;
        }
    }
    public bool IsCarrying()
    {
        return _isCarryingWorm;
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
