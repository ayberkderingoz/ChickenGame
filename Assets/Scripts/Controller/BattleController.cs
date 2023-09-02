using System;
using System.Collections;
using System.Collections.Generic;
using Controller;
using Entity;
using UnityEngine;

public class BattleController : MonoBehaviour
{

    
    private bool battleStarted = false;

    private void Update()
    {
        if ((battleStarted && SoldierChickenController.Instance.soldierChickens.Count == 0) ||
            battleStarted && EnemyController.Instance.enemyList.Count == 0)
        {
            battleStarted = false;
            SetSoldiersAttack(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            SetSoldiersAttack(true);
            battleStarted = true;
        }
    }


    private void SetSoldiersAttack(bool attackMode)
    {
        foreach (var soldier in SoldierChickenController.Instance.soldierChickens)
        {
            soldier.GetComponent<SoldierChicken>().SetAttackMode(attackMode);
        }
    }
}
