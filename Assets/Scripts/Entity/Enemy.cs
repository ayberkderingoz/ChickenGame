using System.Collections;
using System.Collections.Generic;
using Character;
using Controller;
using Entity;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private int _damage = 0;
    
    public GameObject target;


    private Animator animator;
    private bool isDead = false;

    private bool playerDetected;

    private Transform player;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private float detectionRange;
    public string[] targetTags = { "Player", "Soldier" };


    public List<GameObject> targetList;
    
    private void OnTargetChanged(List<GameObject> targets)
    {
        this.targetList = targets;
    }
    
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        player = Player.Instance.gameObject.transform;
        TargetController.Instance.OnTargetChanged += OnTargetChanged;
    }

    // Update is called once per frame
    private void Update()
    {
        SearchTarget();
        if (target is not null)
        {
            Attack();
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player.Instance.TakeDamage(_damage);

        }
    }


    
    private void SearchTarget()
    {
        Transform closestTarget = null;
        float closestDistance = Mathf.Infinity;
        foreach (GameObject targetObject in targetList)
        {
            Vector3 rayDirection = targetObject.transform.position - transform.position;
            if (Physics.Raycast(transform.position, rayDirection, out RaycastHit hit, detectionRange, targetLayer))
            {

                float distanceToTarget = Vector3.Distance(transform.position, targetObject.transform.position);

                if (distanceToTarget < closestDistance)
                {
                    closestDistance = distanceToTarget; 
                    closestTarget = targetObject.transform;
                }
            }
        }

        if (closestTarget is not null) target = closestTarget.gameObject;
        else
        {
            target = null;
        }
    }



    private GameObject SetTarget()
    {
        var playerGameOjbect = Player.Instance.gameObject;
        var soldier = SoldierChickenController.Instance.GetClosestSoldier(gameObject.transform.position);

        if (Vector3.Distance(playerGameOjbect.transform.position, gameObject.transform.position) > Vector3.Distance(soldier.transform.position,gameObject.transform.position))
        {
            return soldier;
        }
        else
        {
            return playerGameOjbect;
        }
    }

    private bool IsInRange(Vector3 distance)
    {
        
        
        return true;
    }

    private void Idle()
    {
        animator.SetTrigger("Dance");
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");
    }

    private void Die()
    {
        isDead = true;
        animator.SetTrigger("Die");
    }

}
