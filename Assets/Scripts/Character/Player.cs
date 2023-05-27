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

        
        private static Player _instance;
        public static Player Instance => _instance;
        
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
                //TODO:Update health bar
                ProgressBar.Instance.UpdateHealthBar(_health);
                
            }
        }

        private void LevelUp()
        {
            _level += 1;
            ProgressBar.Instance.LevelUp();
            //TODO: Load Corresponding Level
        }


        public void SetXp(int xp)
        {
            _xp += xp;
            if (_xp >= 100)
            {
                LevelUp();
                _xp -= 100;
            }
            ProgressBar.Instance.UpdateXpBar(_xp);
        }

        
    }
}