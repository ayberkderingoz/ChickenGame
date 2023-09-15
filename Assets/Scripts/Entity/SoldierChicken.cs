using System.Collections.Generic;
using System.ComponentModel;
using Controller;
using Projectile;
using UnityEngine;
using UnityEngine.AI;

namespace Entity
{
    public class SoldierChicken : MonoBehaviour
    {
        
        private NavMeshAgent _agent;
        private PooledObject _pooledObject;
        private bool _attackMode = false;

        [SerializeField] private float attackRange = 12f;
        private int _damage = 5;
        public int _health = 100;
        private float _timeSinceLastShot = 0f;

        public List<GameObject> enemyList;

        [SerializeField] private float attackCooldown = 2.19f;
        private float lastAttackTime;

        [SerializeField] private LayerMask targetLayer;
        [SerializeField] private float detectionRange = 25;
        public GameObject target; //can change public


        private void Start()
        {
            EnemyController.Instance.OnEnemiesChanged += OnEnemiesChanged;
        }

        void FixedUpdate()
        {
            if (!_attackMode) return;
            SearchTarget();
            if (target is not null)
            {
                if (Vector3.Distance(gameObject.transform.position, target.transform.position) > attackRange)
                {
                    MoveToEnemy();
                }
                else
                {
                    StopMoving();
                    ShootProjectile();
                }
            }
            else
            {
            }
        }

        private void StopMoving()
        {
            _agent.SetDestination(transform.position);
        }

        private void MoveToEnemy()
        {
            _agent.SetDestination(target.transform.position);
        }

        private void OnEnemiesChanged(List<GameObject> enemies)
        {
            enemyList = enemies;
        }


        public void SetAttackMode(bool attackMode)
        {
            _attackMode = attackMode;
        }

        public bool GetAttackMode()
        {
            return _attackMode;
        }


        public void SetPooledObject(PooledObject pooledObject)
        {
            _pooledObject = pooledObject;
        }

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        public void MoveArea()
        {
            _agent.SetDestination(SoldierPositionManager.Instance.GetPosition());
        }


        private void ShootProjectile()
        {
            if (!(Time.time - lastAttackTime >= attackCooldown)) return;
            
            var projectilePooledObject = ObjectPool.Instance.GetPooledObject(PooledObjectType.SoldierProjectile);
            var projectile = projectilePooledObject.gameObject;
            var position = transform.position;
            projectile.transform.position =
                new Vector3(position.x, position.y + 1.2f, position.z + 2f);

            projectile.SetActive(true);
            projectile.GetComponent<SoldierProjectile>().MoveToEnemy(projectilePooledObject, target);
            target.gameObject.GetComponent<Enemy>().TakeDamage(_damage);
            lastAttackTime = Time.time;
        }


        private void SearchTarget()
        {
            Transform closestTarget = null;
            float closestDistance = Mathf.Infinity;
            foreach (GameObject targetObject in enemyList)
            {
                Vector3 rayDirection = targetObject.transform.position - transform.position;
                rayDirection.y += 1;
                Debug.DrawRay(transform.position, rayDirection, Color.red);
                if (Physics.Raycast(transform.position, rayDirection, out _, detectionRange, targetLayer))
                {
                    float distanceToTarget = Vector3.Distance(transform.position, targetObject.transform.position);

                    if (distanceToTarget < closestDistance)
                    {
                        closestDistance = distanceToTarget;
                        closestTarget = targetObject.transform;
                    }
                }
            }

            target = closestTarget != null ? closestTarget.gameObject : null;
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
            SoldierChickenController.Instance.RemoveSoldier(gameObject);
            _pooledObject.ReturnToPool();
        }
    }
}