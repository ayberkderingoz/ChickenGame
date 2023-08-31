using System;
using System.Collections.Generic;
using Character;
using UnityEngine;
using UnityEngine.Serialization;

namespace Controller
{
    public class TargetController : MonoBehaviour
    {

        public List<GameObject> targetList = new List<GameObject>();
        public Action<List<GameObject>> OnTargetChanged;
        
        private static TargetController _instance;
        public static TargetController Instance => _instance;

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

        void Start()
        {
            
            SoldierChickenController.Instance.OnSoldierChickenChanged += OnSoldierChickenChanged;
            Invoke("InitPlayer",1f);
        }
        void InitPlayer () {
            targetList.Add(Player.Instance.gameObject);
            OnTargetChanged?.Invoke(targetList);
        }
        private void OnSoldierChickenChanged(List<GameObject> soldiers)
        {
            this.targetList = soldiers;
            targetList.Add(Player.Instance.gameObject);
            OnTargetChanged?.Invoke(targetList);
        }
        
        
    }
}