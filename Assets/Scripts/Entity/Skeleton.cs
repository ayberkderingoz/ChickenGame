using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace Entity
{

    public class Skeleton : MonoBehaviour
    {

        private float _timeSinceLastSpawn = 0f;
        [SerializeField] private float _objectDuration = 60f;
        private PooledObject _pooledObject;
        //navmesh
        private NavMeshAgent _agent;

        public void SetPooledObject(PooledObject pooledObject)
        {
            _pooledObject = pooledObject;

        }

        private void Update()
        {
            _timeSinceLastSpawn += Time.deltaTime;
            if (_timeSinceLastSpawn >= _objectDuration)
            {
                _pooledObject.ReturnToPool();
                _timeSinceLastSpawn = 0f;
            }
            
        }

        private void FixedUpdate()
        {
            if (IsPlayerInRange())
            {
                MoveToPlayerNavMesh();
            }
            else
            {
                MoveRandom();
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _pooledObject.ReturnToPool();
            }
        }

        private void MoveToPlayerNavMesh()
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            var playerPosition = player.transform.position;
            var agent = GetComponent<NavMeshAgent>();
            agent.SetDestination(playerPosition);
        }

        private void MoveRandom()
        {
            var position = transform.position;
            var randomPosition = new Vector3(position.x + UnityEngine.Random.Range(-5, 5), position.y, position.z + UnityEngine.Random.Range(-5, 5));
            var agent = GetComponent<NavMeshAgent>();
            agent.SetDestination(randomPosition);
        }
        private bool IsPlayerInRange()
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            var playerPosition = player.transform.position;
            var position = transform.position;
            var distance = Vector3.Distance(playerPosition, position);
            return distance < 5f;
        }
        
    }
}
