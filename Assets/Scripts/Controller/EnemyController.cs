using System;
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

        private void Start()
        {
            Invoke("BroadcastEnemies",2f);
        }

        public void BroadcastEnemies()
        {
            OnEnemiesChanged?.Invoke(enemyList);
        }

        public void RemoveEnemy(GameObject enemy)
        {
            enemyList.Remove(enemy);
        }


    }
}
