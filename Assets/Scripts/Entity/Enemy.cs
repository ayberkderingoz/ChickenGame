using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private int _damage = 0;

    private int range = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player.Instance.TakeDamage(_damage);

        }
    }

    private void MoveToPlayerNavMesh(Vector3 playerPosition)
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        var playerPosition = player.transform.position;
        var agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.SetDestination(playerPosition);
    }

    private void MoveToClosestEntity()
    {
        
    }
    private bool IsPlayerInRange()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        var playerPosition = player.transform.position;
        var position = transform.position;
        var distance = Vector3.Distance(playerPosition, position);
        return distance < range;
    }
}
