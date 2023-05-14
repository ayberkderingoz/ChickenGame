using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //spawn enemy prefab
    public GameObject enemyPrefab;
    //spawn location

    //spawn rate
    public float spawnRate;
    //spawn timer
    private float _spawnTimer = 0f;
    //spawn enemy
    private void Update()
    {

    
        //get a random spawn point around the spawner in x and z axis
        var position = transform.position;
        var spawnPoint = new Vector3(position.x + Random.Range(-5, 5), position.y, position.z + Random.Range(-5, 5));

        


        _spawnTimer += Time.deltaTime;
        if (_spawnTimer >= spawnRate)
        {
            Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
            _spawnTimer = 0f;
        }
    }
}
