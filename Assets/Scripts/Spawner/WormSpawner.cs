using Entity;
using UnityEngine;

namespace Spawner
{
    public class WormSpawner : MonoBehaviour
    {

        public GameObject parentGameObject;
        

        [SerializeField]public float spawnTimer = 5f;
        

        private float _timeSinceLastSpawn=0f;

        void Update()
        {
            _timeSinceLastSpawn += Time.deltaTime;

            if (_timeSinceLastSpawn >= spawnTimer)
            {
                SpawnObjectInRandomPosition();
                _timeSinceLastSpawn = 0f;
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void SpawnObjectInRandomPosition()
        {
            MeshRenderer[] childRenderers = parentGameObject.GetComponentsInChildren<MeshRenderer>();
        
            MeshRenderer randomChildRenderer = childRenderers[Random.Range(0, childRenderers.Length)];
        
            Bounds bounds = randomChildRenderer.bounds;
        
            Vector3 randomPosition = new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y),
                Random.Range(bounds.min.z, bounds.max.z));

            var wormPooledObject = ObjectPool.Instance.GetPooledObject(PooledObjectType.Worm);
            var worm = wormPooledObject.gameObject;
            worm.GetComponent<Worm>().SetPooledObject(wormPooledObject);
        
            worm.transform.position = randomPosition;
            worm.SetActive(true);
        
        
        }

    }
}
