using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Slider xpBar;
        [SerializeField] private Image healthBar;
        [SerializeField] private TextMeshProUGUI _levelText ;

        private static ProgressBar _instance;
        public static ProgressBar Instance => _instance;
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }

            healthBar.fillAmount = 0;
        }


        public void LevelUp()
        {
            _levelText.text = (int.Parse(_levelText.text) + 1).ToString();
            xpBar.value = 0;
        }
        public void UpdateHealthBar(int health)
        {
            healthBar.fillAmount = 1-health / 100f;
        }
        public void UpdateXpBar(int xp)
        {
            xpBar.value = xpBar.value+xp / 100f;
        }

    }
}