using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;


namespace Character
{
    public class Player : MonoBehaviour
    {
        private int _level = 0;
        private int _xp = 0;
        private int _health = 100;

        private bool _pulling;


        private static Player _instance;
        public static Player Instance => _instance;


        public Action<int> OnHealthChanged;
        public Action<int> OnXpChanged;
        public Action<int> OnLevelChanged;


        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
        }

        public void TakeDamage(int damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                //TODO:Game Over
            }
            else
            {
                OnHealthChanged?.Invoke(_health);

            }
        }

        private void LevelUp()
        {
            _level += 1;
            OnLevelChanged?.Invoke(_level);

        }


        public void SetXp(int xp)
        {
            _xp += xp;
            if (_xp >= 100)
            {
                LevelUp();
                _xp -= 100;
            }

            OnXpChanged?.Invoke(_xp);
        }

        public void StackEgg(GameObject egg, PooledObject pooledObjet)
        {
            _eggs.Add(egg, pooledObjet);
            ScoreManager.Instance.UpdateScore(ScoreManager.ScoreType.Egg, _eggs.Count);
            egg.transform.SetParent(gameObject.transform);
            float maxXPos = egg.transform.position.y + egg.transform.localScale.y / 2;
            var position = gameObject.transform.position;
            egg.transform.localPosition = new Vector3(0, (maxXPos * _eggs.Count), -1);

        }

        private Dictionary<GameObject, PooledObject> _eggs;

        private void Start()
        {
            LevelUp();
            _eggs = new Dictionary<GameObject, PooledObject>();
        }

        public bool IsEggInDictionary(PooledObject pooledObject)
        {
            return _eggs.ContainsValue(pooledObject);
        }
        
        
        public Dictionary<GameObject,PooledObject> GetEggs()
        {
            return _eggs;
        }
        public void SetEggs(Dictionary<GameObject,PooledObject> eggs)
        {
            _eggs = eggs;
        }

        public int GetLevel()
        {
            return _level;
        }

        public bool IsPulling()
        {
            return _pulling;
        }

        public void SetPulling(bool pulling)
        {
            _pulling = pulling;
        }

    }
}