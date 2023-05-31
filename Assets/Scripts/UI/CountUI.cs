using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SocialPlatforms.Impl;


namespace UI
{
    public class CountUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI wormCount;
        [SerializeField] private TextMeshProUGUI goldCount;
        [SerializeField] private TextMeshProUGUI eggCount;
        
        private static CountUI _instance;
        public static CountUI Instance => _instance;
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
        }

        private void Start()
        {
            ScoreManager.Instance.OnScoreChanged += OnScoreChanged;
            ScoreManager.Instance.OnScoreReset += OnScoreReset;
        }
        
        private void OnScoreReset(ScoreManager.ScoreType scoreType)
        {
            switch (scoreType)
            {
                case ScoreManager.ScoreType.Worm:
                    wormCount.text = "0";
                    break;
                case ScoreManager.ScoreType.Gold:
                    goldCount.text = "0";
                    break;
                case ScoreManager.ScoreType.Egg:
                    eggCount.text = "0";
                    break;
            }
        }
        private void OnScoreChanged(ScoreManager.ScoreType scoreType, int count)
        {
            switch (scoreType)
            {
                case ScoreManager.ScoreType.Worm:
                    wormCount.text = count.ToString();
                    break;
                case ScoreManager.ScoreType.Gold:
                    goldCount.text = count.ToString();
                    break;
                case ScoreManager.ScoreType.Egg:
                    eggCount.text = count.ToString();
                    break;
            }
        }

    }
}
