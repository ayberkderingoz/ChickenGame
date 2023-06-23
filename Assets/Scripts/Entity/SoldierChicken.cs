using System;
using System.Collections.Generic;
using Projectile;
using Spawner;
using UnityEngine;
using UnityEngine.AI;

namespace Entity
{
    public class SoldierChicken : MonoBehaviour
    {
        
        private NavMeshAgent _agent;
        private PooledObject _pooledObject;
        private bool _attackMode = false;
        [SerializeField] private float range = 10f;
        private int _damage = 5;
        private float _timeSinceLastShot = 0f;
        [SerializeField] private float _shotInterval = 2f;



        void Update()
        {
            if (_attackMode)
            {
                //TODO: Stack around player

                _timeSinceLastShot += Time.deltaTime;
                if (IsEnemyInRange())
                {
                    var enemy = FindClosestSkeleton();
                    transform.LookAt(enemy.transform);
                    if (_timeSinceLastShot >= _shotInterval)
                    {
                        transform.LookAt(enemy.transform);
                        ShootProjectile();
                        _timeSinceLastShot = 0f;
                    }
                }

            }
            
        }

        public void SetAttackMode(bool attackMode)
        {
            _attackMode = attackMode;
        }
        public bool GetAttackMode()
        {
            return _attackMode;
        }
        private GameObject FindClosestSkeleton()
        {
            //find closest skeleton
            var skeletons = GameObject.FindGameObjectsWithTag("Skeleton");
            var closestSkeleton = skeletons[0];
            var closestDistance = Vector3.Distance(transform.position, closestSkeleton.transform.position);
            foreach (var skeleton in skeletons)
            {
                var distance = Vector3.Distance(transform.position, skeleton.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestSkeleton = skeleton;
                }
            }

            return closestSkeleton;
        }

        public void SetPooledObject(PooledObject pooledObject)
        {
            _pooledObject = pooledObject;
            MoveArea();
        }

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }
        private void MoveArea()
        {
            
            var areas = GameObject.FindGameObjectsWithTag("SoldierArea");
            _agent.SetDestination(SoldierPositionManager.Instance.GetPosition());
        }
        private bool IsEnemyInRange()
        {
            var enemy = GameObject.FindGameObjectWithTag("Skeleton");
            var enemyPosition = enemy.transform.position;
            var position = transform.position;
            var distance = Vector3.Distance(enemyPosition, position);
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
            projectile.GetComponent<EnemyProjectile>().MoveToPlayer(projectilePooledObject);
            


        }

    }
}