using Character;
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


        private void Awake()
        {
            healthBar.fillAmount = 0;
        }
        private void Start()
        {
            Player.Instance.OnHealthChanged += OnHealthChanged;
            Player.Instance.OnLevelChanged += OnLevelChanged;
            Player.Instance.OnXpChanged += OnXpChanged;
        }
        
        private void OnLevelChanged(int level)
        {
            _levelText.text = level.ToString();
        }
        private void OnXpChanged(int xp)
        {
            xpBar.value = xp / 100f;
        }
        private void OnHealthChanged(int health)
        {
            healthBar.fillAmount = 1-health / 100f;
        }
        

    }
}