using System;
using System.Collections;
using System.Collections.Generic;
using Entity;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawner
{
    public class WormSpawner : MonoBehaviour
    {
        public GameObject parentGameObject;


        [SerializeField] public float spawnTimer = 1f;


        private float _timeSinceLastSpawn = 0f;
        public List<GameObject> worms = new List<GameObject>();
        private const int MaxWormCount = 7;

        public Action<List<GameObject>> OnWormsChanged;


        private bool _isSpawning;
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

            StartCoroutine(StartSpawning());
        }

        private IEnumerator StartSpawning()
        {
            _isSpawning = true;
            while (_isSpawning)
            {
                var currentWormCount = worms.Count;
                var numberOfWormsToSpawn = MaxWormCount - currentWormCount;
                for (int i = 0; i < numberOfWormsToSpawn; i++)
                {
                    SpawnObjectInRandomPosition();
                    yield return null;
                }
                yield return null;
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
            if (randomPosition == Vector3.zero) return;

            var wormPooledObject = ObjectPool.Instance.GetPooledObject(PooledObjectType.Worm);
            var wormObject = wormPooledObject.gameObject;
            var worm = wormObject.GetComponent<Worm>();
            worm.SetPooledObject(wormPooledObject);
            worms.Add(wormObject);
            OnWormsChanged?.Invoke(worms);


            wormObject.transform.position = randomPosition;

            var level = GetRandomLevel();
            worm.SetLevel(level);
            worm.levelText =
                SpawnWormText(new Vector3(randomPosition.x + .5f, randomPosition.y + .5f, randomPosition.z), level,
                    wormObject);

            wormObject.SetActive(true);
        }

        private Vector3 RandomPositionFarFromWorms()
        {
            var bestLocation = Vector3.zero;


            if (worms.Count == 0)
            {
                return RandomPosition();
            }

            for (var i = 0; i < 60; i++)
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
                Random.Range(bounds.min.x + 5, bounds.max.x - 5),
                Random.Range(bounds.min.y, bounds.max.y),
                Random.Range(bounds.min.z + 5, bounds.max.z - 5));
        }

        private GameObject SpawnWormText(Vector3 wormLocation, int wormLevel, GameObject worm)
        {
            var wormTextPooledObject = ObjectPool.Instance.GetPooledObject(PooledObjectType.WormText);
            worm.GetComponent<Worm>().levelTextPooledObject = wormTextPooledObject;
            var wormText = wormTextPooledObject.gameObject;
            wormText.transform.position = wormLocation;
            wormText.SetActive(true);
            return wormText.GetComponent<WormText>().SetLevel(wormLevel, worm);
        }

        private int GetRandomLevel()
        {
            var level = Random.Range(1, 100);

            return level switch
            {
                >= 1 and <= 50 => 1,
                >= 51 and <= 80 => 2,
                >= 81 and <= 95 => 3,
                >= 96 and <= 100 => 4,
                _ => 1
            };
        }
    }
}