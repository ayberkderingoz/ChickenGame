using UnityEngine;

namespace Entity
{
    public class Worm : MonoBehaviour
    {

        private float _timeSinceLastSpawn = 0f;
        [SerializeField] private float _objectDuration = 20f;
        private PooledObject _pooledObject;
    
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
                _pooledObject.ReturnToPool();
            }
        }
    }
}
