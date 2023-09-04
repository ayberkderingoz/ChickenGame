using System;
using System.Collections;
using System.Collections.Generic;
using Character;
using Controller;
using Entity;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    private int _damage = 5;
    [SerializeField] private int _health = 100;
    
    public GameObject target;


    private Animator animator;
    private bool isDead = false;
    private NavMeshAgent _agent;

    
    

    private Transform player;
    [SerializeField] private float attackCooldown = 2.19f;
    private float lastAttackTime;
    [SerializeField] private LayerMask targetLayer;
    private float detectionRange = 13f;
    public string[] targetTags = { "Player", "Soldier" };


    public List<GameObject> targetList;
    
    private void OnTargetChanged(List<GameObject> targets)
    {
        this.targetList = targets;
    }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        player = Player.Instance.gameObject.transform;
        TargetController.Instance.OnTargetChanged += OnTargetChanged;
    }
    

    // Update is called once per frame
    private void FixedUpdate()
    {
        SearchTarget();
        if (target is not null)
        {
            if (Vector3.Distance(target.transform.position, gameObject.transform.position) > 3)
            {
                MoveToTarget();
            }
            else
            {
                Attack();
            }
        }
        if (target is null)
        {
            _agent.SetDestination(transform.position);
            Idle();
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


    private void MoveToTarget()
    {
        animator.SetTrigger("Walk");
        _agent.SetDestination(target.transform.position);
        transform.LookAt(_agent.nextPosition);
    }


    private void Idle()
    {
        animator.SetTrigger("Idle");
    }

    private void Attack()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            animator.SetTrigger("Attack");
            if (target.CompareTag("Player"))
            {
                Invoke("DealDamageDelayed",1f);
                
            }
            else if (target.CompareTag("Soldier"))
            {
                Invoke("DealDamageSoldierDelayed",1f);
            }

            lastAttackTime = Time.time;
        }
    }


    private void DealDamageSoldierDelayed()
    {
        target.GetComponent<SoldierChicken>().TakeDamage(_damage);
    }

    private void DealDamageDelayed()
    {
        Player.Instance.TakeDamage(_damage);
    }

    private void Die()
    {
        animator.SetTrigger("Die");
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Die();
            Invoke("RemoveEnemy",2.2f);
        }
    }

    private void RemoveEnemy()
    {
        EnemyController.Instance.RemoveEnemy(gameObject);
        Destroy(gameObject);
    }
    

}
