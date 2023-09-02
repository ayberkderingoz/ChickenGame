using System;
using System.Collections.Generic;
using Controller;
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
        private int _health = 100;
        private float _timeSinceLastShot = 0f;
        [SerializeField] private float _shotInterval = 2f;
        public List<GameObject> enemyList;
        
        [SerializeField] private LayerMask targetLayer;
        [SerializeField] private float detectionRange = 15f;
        public GameObject target; //can change public


        private void Start()
        {
            EnemyController.Instance.OnEnemiesChanged += OnEnemiesChanged;
        }

        void Update()
        {
            if (_attackMode)
            {
                SearchTarget();
                if (target is not null)
                {
                    if (Vector3.Distance(gameObject.transform.position, target.transform.position) > range)
                    {
                        MoveToEnemy();
                    }
                    else
                    {
                        ShootProjectile();
                        target.gameObject.GetComponent<Enemy>().TakeDamage(_damage);
                    }
                }
                else
                {
                    
                }

            }
            
        }

        private void MoveToEnemy()
        {
            Vector3 direction = target.transform.position - transform.position;

            // Subtract the range from the direction vector to get the position
            var calculatedPosition = target.transform.position - direction.normalized * (range*0.9f);
            _agent.SetDestination(calculatedPosition);
        }

        private void OnEnemiesChanged(List<GameObject> enemies)
        {
            this.enemyList = enemies;
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
        private bool IsEnemyInRange() //deprecated
        {
            var enemy = GameObject.FindGameObjectWithTag("Skeleton");
            var enemyPosition = enemy.transform.position;
            var position = transform.position;
            var distance = Vector3.Distance(enemyPosition, position);
            return distance < range;
        }

        private void ShootProjectile()
        {
        
            var projectilePooledObject = ObjectPool.Instance.GetPooledObject(PooledObjectType.SoldierProjectile);
            var projectile = projectilePooledObject.gameObject;
            var position = transform.position;
            projectile.transform.position =
                new Vector3(position.x, position.y + 1.2f, position.z);
            
            projectile.SetActive(true);
            projectile.GetComponent<SoldierProjectile>().MoveToEnemy(projectilePooledObject,target);
            


        }


        private void SearchTarget()
        {
            Transform closestTarget = null;
            float closestDistance = Mathf.Infinity;
            foreach (GameObject targetObject in enemyList)
            {
                Vector3 rayDirection = targetObject.transform.position - transform.position;
                if (Physics.Raycast(transform.position, rayDirection, out RaycastHit hit, detectionRange, targetLayer))
                {

                    float distanceToTarget = Vector3.Distance(transform.position, targetObject.transform.position);

                    if (distanceToTarget < closestDistance)
                    {
                        closestDistance = distanceToTarget; 
                        closestTarget = targetObject.transform;
                    }
                }
            }

            if (closestTarget is not null) target = closestTarget.gameObject;
            else
            {
                target = null;
            }
        }

        public void TakeDamage(int damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                Die();
            }
            
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}