using System;
using System.Collections;
using System.Collections.Generic;
using Controller;
using Entity;
using UnityEngine;

public class BattleController : MonoBehaviour
{

    
    private bool _isBattleStarted;

    private void Update()
    {
        if (_isBattleStarted && SoldierChickenController.Instance.soldierChickens.Count == 0 ||
            _isBattleStarted && EnemyController.Instance.enemyList.Count == 0)
        {
            _isBattleStarted = false;
            SetSoldiersAttack(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SetSoldiersAttack(true);
            _isBattleStarted = true;
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
