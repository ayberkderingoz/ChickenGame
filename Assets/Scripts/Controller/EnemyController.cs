using System;
using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine;

namespace Controller
{
    public class EnemyController : MonoBehaviour
    {
        public List<GameObject> enemyList = new List<GameObject>();
        public Action<List<GameObject>> OnEnemiesChanged;

        
        private static EnemyController _instance;
        public static EnemyController Instance => _instance;

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
        }

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(2);
            BroadcastEnemies();
        }


        public void BroadcastEnemies()
        {
            Invoke("BroadcastEnemiesDelayed",2f);
        }

        private void BroadcastEnemiesDelayed()
        {
            OnEnemiesChanged?.Invoke(enemyList);
        }

        public void RemoveEnemy(GameObject enemy)
        {
            enemyList.Remove(enemy);
            OnEnemiesChanged?.Invoke(enemyList);
        }


    }
}
