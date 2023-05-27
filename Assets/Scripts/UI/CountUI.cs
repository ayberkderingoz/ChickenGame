using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class CountUI : MonoBehaviour
    {
        //TODO: Ardaya sor bunların countunu tutup count değişikliğinde eventte updatelemek mi daha mantıklı bu mu daha mantıklı?
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
        public enum CountType
        {
            Worm,
            Gold,
            Egg
        }
        
        public void UpdateCount(CountType countType, int count)
        {
            count += GetCount(countType);
            switch (countType)
            {
                case CountType.Worm:
                    wormCount.text = count.ToString();
                    break;
                case CountType.Gold:
                    goldCount.text = count.ToString();
                    break;
                case CountType.Egg:
                    eggCount.text = count.ToString();
                    break;
            }
        }
        
        public int GetCount(CountType countType)
        {
            switch (countType)
            {
                case CountType.Worm:
                    return int.Parse(wormCount.text);
                case CountType.Gold:
                    return int.Parse(goldCount.text);
                case CountType.Egg:
                    return int.Parse(eggCount.text);
            }
            return 0;
        }
        
        public void ResetCount(CountType countType)
        {
            switch (countType)
            {
                case CountType.Worm:
                    wormCount.text = "0";
                    break;
                case CountType.Gold:
                    goldCount.text = "0";
                    break;
                case CountType.Egg:
                    eggCount.text = "0";
                    break;
            }
        }
        
    }
}
