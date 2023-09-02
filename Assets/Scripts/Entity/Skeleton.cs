using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Character;
using Projectile;
using UnityEngine;
using UnityEngine.AI;

namespace Entity
{

    public class Skeleton : MonoBehaviour
    {

        /*private float _timeSinceLastSpawn = 0f;
        [SerializeField] private float _objectDuration = 60f;
        private PooledObject _pooledObject;
        [SerializeField] private float range = 10f;
        //navmesh
        private NavMeshAgent _agent;
        private int _damage = 5;
        private float _timeSinceLastShot = 0f;
        [SerializeField] private float _shotInterval = 2f;

        public void SetPooledObject(PooledObject pooledObject)
        {
            _pooledObject = pooledObject;

        }
        

        private void Update()
        {
            _timeSinceLastShot += Time.deltaTime;
            if (IsPlayerInRange())
            {
                transform.LookAt(Player.Instance.transform);
                if (_timeSinceLastShot >= _shotInterval)
                {
                    transform.LookAt(Player.Instance.transform);
                    ShootProjectile();
                    _timeSinceLastShot = 0f;
                }
            }
            
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Player.Instance.TakeDamage(_damage);
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
            return distance < range;
        }

        private void ShootProjectile()
        {
        
            var projectilePooledObject = ObjectPool.Instance.GetPooledObject(PooledObjectType.EnemyProjectile);
            var projectile = projectilePooledObject.gameObject;
            var position = transform.position;
            projectile.transform.position =
                new Vector3(position.x, position.y + 1.2f, position.z);
            
            projectile.SetActive(true);
            projectile.GetComponent<EnemyProjectile>().MoveToPlayer(projectilePooledObject); //TODO: Ardaya sor kesin bok gibi bu kullanım
            


        }*/

    }
}
