using UnityEngine;

namespace UI
{
    public class WorkerUpgradeUI : MonoBehaviour
    {
        // Start is called before the first frame update
    
        private static WorkerUpgradeUI _instance;
        public static WorkerUpgradeUI Instance => _instance;
    
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
        }

        public bool IsActive()
        {
            return gameObject.activeSelf;
        }
    }
}
