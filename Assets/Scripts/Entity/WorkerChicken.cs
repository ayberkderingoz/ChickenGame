using System.Collections.Generic;
using Entity;
using Spawner;
using UnityEngine;
using UnityEngine.AI;

public class WorkerChicken : MonoBehaviour
{
    private NavMeshAgent _agent;
    public bool _isCarryingWorm = false;
    private GameObject bigChicken;
    public List<GameObject> _wormList;
    public GameObject _targetWorm;


    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        bigChicken = BigChicken.Instance.gameObject;
    }

    //wait then initialize big chicken
    public void SetAvailableWorms()
    {
        _wormList = WormSpawner.Instance.worms;
    }

    private void Start()
    {
        WormSpawner.Instance.OnWormsChanged += OnWormsChanged;
    }


    private void OnWormsChanged(List<GameObject> worms)
    {
        _wormList = worms;
    }

    private void Update()
    {
        if (_isCarryingWorm)
        {
            transform.LookAt(_agent.nextPosition);
            if (!_isMovingToBigChicken)
                MoveToBigChicken();
        }
        else if (_targetWorm == null)
        {
            MoveToWorm();
        }
    }

    private bool _isMovingToBigChicken;

    private void MoveToBigChicken()
    {
        _isMovingToBigChicken = true;
        _targetWorm = null;
        _agent.SetDestination(bigChicken.transform.position);
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
    public void SetCarrying(bool isCarrying )
    {
        _isCarryingWorm = isCarrying;
        if (isCarrying)
            _isMovingToBigChicken = false;


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