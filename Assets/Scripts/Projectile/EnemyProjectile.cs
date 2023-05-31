using System;
using Character;
using Entity;
using UnityEngine;

namespace Projectile
{
    public class EnemyProjectile : MonoBehaviour
    {
        private PooledObject _pooledObject;
        
        

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Player.Instance.TakeDamage(5);
                _pooledObject.ReturnToPool();

            }
        }

        private void Update()
        {
            if (gameObject.activeSelf)
            {
                var position = Player.Instance.transform.position;
                transform.position = Vector3.MoveTowards(transform.position,new Vector3(position.x,position.y+1.2f,position.z), 10f * Time.deltaTime);
            }
        }


        public void MoveToPlayer(PooledObject pooledObject)
        {
            //move object to player
            _pooledObject = pooledObject;
            //var playerPosition = Player.Instance.transform.position;
            //move to player
            //transform.position = Vector3.MoveTowards(transform.position, playerPosition, 10f * Time.deltaTime);
        }
    }
}
