using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class WormSpawner : MonoBehaviour
{

    public GameObject parentGameObject;
    public GameObject objectToSpawn;

    [SerializeField]public float spawnTimer = 5f;
    [SerializeField]public float objectDuration = 45f;

    private float _timeSinceLastSpawn;

    void Update()
    {
        _timeSinceLastSpawn += Time.deltaTime;

        if (_timeSinceLastSpawn >= spawnTimer)
        {
            SpawnObjectInRandomPosition();
            _timeSinceLastSpawn = 0f;
        }
    }

    void SpawnObjectInRandomPosition()
    {
        MeshRenderer[] childRenderers = parentGameObject.GetComponentsInChildren<MeshRenderer>();
        
        MeshRenderer randomChildRenderer = childRenderers[Random.Range(0, childRenderers.Length)];
        
        Bounds bounds = randomChildRenderer.bounds;
        
        Vector3 randomPosition = new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z));


        GameObject spawnedObject = Instantiate(objectToSpawn, randomPosition, Quaternion.identity);
        Destroy(spawnedObject, objectDuration);
    }

}
