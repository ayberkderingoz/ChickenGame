using UnityEngine;
using Entity;
using UnityEngine.Serialization;

namespace Spawner
{
    public class EnemySpawner : MonoBehaviour
    {
        /*[SerializeField]private float spawnTimer=5f;
        //spawn timer
        private float _timeSinceLastSpawn = 0f;
        //spawn enemy
        void Update()
        {
            _timeSinceLastSpawn += Time.deltaTime;
            if (_timeSinceLastSpawn >= spawnTimer)
            {
                SpawnObjectInRandomPosition();
                _timeSinceLastSpawn = 0f;
            }
        }
        private void SpawnObjectInRandomPosition()
        {
        
            //Change this when you have a spawn area. 
            var position = transform.position;
            var randomPosition = new Vector3(position.x + Random.Range(-5, 5), position.y, position.z + Random.Range(-5, 5));

            
            var enemyPooledObject = ObjectPool.Instance.GetPooledObject(PooledObjectType.Enemy);
            var enemy = enemyPooledObject.gameObject;
            enemy.GetComponent<Skeleton>().SetPooledObject(enemyPooledObject);
        
            enemy.transform.position = randomPosition;
            enemy.SetActive(true);
        
        
        }*/
    
    }
}
