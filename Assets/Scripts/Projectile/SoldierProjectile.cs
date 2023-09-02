using System;
using Character;
using Entity;
using UnityEngine;
using UnityEngine.AI;

namespace Projectile
{
    public class SoldierProjectile : MonoBehaviour
    {
        private PooledObject _pooledObject;
        private NavMeshAgent _agent;
        private GameObject target;


        private void Start() //TODO: ADD ATTACK SPEED ADD NAVMESHAGENT
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (target is not null)
            {
                _agent.SetDestination(target.transform.position);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                _pooledObject.ReturnToPool();
            }
        }
        public void MoveToEnemy(PooledObject pooledObject,GameObject enemy)
        {
            target = enemy;
            _pooledObject = pooledObject;
            
        }
    }
}
