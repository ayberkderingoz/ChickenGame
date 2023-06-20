using System.Collections.Generic;
using Spawner;
using UnityEngine;
using UnityEngine.AI;

namespace Entity
{
    public class SoldierChicken : MonoBehaviour
    {
        private bool _isCarryingWorm = false;
        private NavMeshAgent _agent;
        private PooledObject _pooledObject;


        public void SetPooledObject(PooledObject pooledObject)
        {
            _pooledObject = pooledObject;
        }

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }
        




        private void Update()
        {

        }
        

    }
}