using System;
using Character;
using Entity;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace Projectile
{
    public class SoldierProjectile : MonoBehaviour
    {
        private PooledObject _pooledObject;
        private GameObject target;
        private const float LifeTime = 1.5f;
        private float timeCreated;
        private Vector3 normalizedTargetLocation;


        private void FixedUpdate()
        {
            if (target is not null && Time.time - timeCreated <= LifeTime)
            {
                transform.position =
                    Vector3.MoveTowards(transform.position, normalizedTargetLocation, 25 * Time.deltaTime);
            }
            else if (LifeTime <= Time.time - timeCreated)
            {
                _pooledObject.ReturnToPool();
            }
        }


        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                _pooledObject.ReturnToPool();
            }
        }

        public void MoveToEnemy(PooledObject pooledObject, GameObject enemy)
        {
            target = enemy;
            _pooledObject = pooledObject;
            var pos = enemy.transform.position;
            normalizedTargetLocation = new Vector3(pos.x, 1.2f, pos.z);
            timeCreated = Time.time;
        }
    }
}