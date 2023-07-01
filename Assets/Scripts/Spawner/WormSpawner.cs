using System;
using System.Collections;
using System.Collections.Generic;
using Entity;
using UI;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Spawner
{
    public class WormSpawner : MonoBehaviour
    {

        public GameObject parentGameObject;
        

        [SerializeField]public float spawnTimer = 1f;
        

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
            while (ObjectPool.Instance.PoolDictionary[PooledObjectType.Worm].Count > 33)
            {
                SpawnObjectInRandomPosition();
            }
        }

        void FixedUpdate()
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
            var randomPosition = RandomPositionFarFromWorms();
            if(randomPosition == Vector3.zero) return;
            var wormPooledObject = ObjectPool.Instance.GetPooledObject(PooledObjectType.Worm);
            var worm = wormPooledObject.gameObject;
            worm.GetComponent<Worm>().SetPooledObject(wormPooledObject);
            worms.Add(worm);
            OnWormsChanged?.Invoke(worms);

            worm.transform.position = randomPosition;
            var level = GetRandomLevel();
            worm.GetComponent<Worm>().SetLevel(level);
            worm.GetComponent<Worm>().levelText = SpawnWormText(new Vector3(randomPosition.x+.5f,randomPosition.y+.5f,randomPosition.z), level,worm);
            
            worm.SetActive(true);
            
        
        
        }

        private Vector3 RandomPositionFarFromWorms()
        {
            var bestLocation = Vector3.zero;


            if (worms.Count == 0)
            {
                return RandomPosition();
            }
            for (var i = 0; i < 30; i++)
            {
                var randomPosition = RandomPosition();
                var j = 0;
                foreach (var worm in worms)
                {
                    var distance = Vector3.Distance(randomPosition, worm.transform.position);
                    if (distance < 3)
                    {
                        break;
                    }
                    j++;
                    if (j == worms.Count)
                    {
                        bestLocation = randomPosition;
                    }
                }
            }

            return bestLocation;
        }

        private Vector3 RandomPosition()
        {
            MeshRenderer[] childRenderers = parentGameObject.GetComponentsInChildren<MeshRenderer>();
        
            MeshRenderer randomChildRenderer = childRenderers[Random.Range(0, childRenderers.Length)];
        
            Bounds bounds = randomChildRenderer.bounds;
            
            return new Vector3(
                Random.Range(bounds.min.x+5, bounds.max.x-5),
                Random.Range(bounds.min.y, bounds.max.y),
                Random.Range(bounds.min.z+5, bounds.max.z-5));
            
        }
        private GameObject SpawnWormText(Vector3 wormLocation, int wormLevel,GameObject worm)
        {
            var wormTextPooledObject = ObjectPool.Instance.GetPooledObject(PooledObjectType.WormText);
            worm.GetComponent<Worm>().levelPooledObject = wormTextPooledObject;
            var wormText = wormTextPooledObject.gameObject;
            wormText.transform.position = wormLocation;
            wormText.SetActive(true);
            return wormText.GetComponent<WormText>().SetLevel(wormLevel,worm);
        }
        private int GetRandomLevel()
        {
            var level = Random.Range(1, 100);

            if(level>=1 && level <= 50)
                return 1;
            else if (level>=51 && level <= 80)
                return 2;
            else if (level>=81 && level <= 95)
                return 3;
            else if (level>=96 && level <= 100)
                return 4;
            else
                return 1;
        }

    }
}
