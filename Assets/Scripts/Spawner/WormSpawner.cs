using System;
using System.Collections.Generic;
using Entity;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Spawner
{
    public class WormSpawner : MonoBehaviour
    {

        public GameObject parentGameObject;
        

        [SerializeField]public float spawnTimer = 5f;
        

        private float _timeSinceLastSpawn=0f;
        public List<GameObject> worms = new List<GameObject>();
        
                
        public Action<List<GameObject>> OnWormsChanged;
        

        private static WormSpawner _instance;
        public static WormSpawner Instance => _instance;
        
        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else 
            {
                _instance = this;
            }
        
            //Spawn worms until objectpool queue is empty
            while (ObjectPool.Instance.PoolDictionary[PooledObjectType.Worm].Count > 10)
            {
                SpawnObjectInRandomPosition();
            }
        }

        void Update()
        {
            
            if(ObjectPool.Instance.PoolDictionary[PooledObjectType.Worm].Count == 0) return;
            _timeSinceLastSpawn += Time.deltaTime;

            if (_timeSinceLastSpawn >= spawnTimer)
            {
                SpawnObjectInRandomPosition();
                _timeSinceLastSpawn = 0f;
            }
            
            
            
            
        }


        public void RemoveWorm(GameObject worm)
        {
            worms.Remove(worm);
            OnWormsChanged?.Invoke(worms);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void SpawnObjectInRandomPosition()
        {
            MeshRenderer[] childRenderers = parentGameObject.GetComponentsInChildren<MeshRenderer>();
        
            MeshRenderer randomChildRenderer = childRenderers[Random.Range(0, childRenderers.Length)];
        
            Bounds bounds = randomChildRenderer.bounds;
        
            Vector3 randomPosition = new Vector3(
                Random.Range(bounds.min.x+5, bounds.max.x-5),
                Random.Range(bounds.min.y, bounds.max.y),
                Random.Range(bounds.min.z+5, bounds.max.z-5));

            var wormPooledObject = ObjectPool.Instance.GetPooledObject(PooledObjectType.Worm);
            var worm = wormPooledObject.gameObject;
            worm.GetComponent<Worm>().SetPooledObject(wormPooledObject);
            worms.Add(worm);
            OnWormsChanged?.Invoke(worms);
            worm.transform.position = randomPosition;
            worm.SetActive(true);
        
        
        }


    }
}
