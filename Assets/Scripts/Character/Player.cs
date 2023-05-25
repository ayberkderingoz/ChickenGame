using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Character
{
    public class Player : MonoBehaviour
    {
        private int _level = 0;
        private float _xp = 0;
        private int _health = 100;


        private void Awake()
        {
            
        }


        private void TakeDamage(int damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                //Game Over
            }
            else
            {
                //Update health bar
                
            }
        }
    }
}