using Character;
using UI;
using UnityEngine;

namespace Entity
{
    public class Worm : MonoBehaviour
    {

        private float _timeSinceLastSpawn = 0f;
        [SerializeField] private float _objectDuration = 20f;
        private PooledObject _pooledObject;
        private int _xp = 20;
    
        public void SetPooledObject(PooledObject pooledObject)
        {
            _pooledObject = pooledObject;
        }

        private void Update()
        {
            _timeSinceLastSpawn += Time.deltaTime;
            if (_timeSinceLastSpawn >= _objectDuration)
            {
                _pooledObject.ReturnToPool();
                _timeSinceLastSpawn = 0f;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Player.Instance.SetXp(_xp);
                CountUI.Instance.UpdateCount(CountUI.CountType.Worm, 1);
                _pooledObject.ReturnToPool();
            }
        }
    }
}
