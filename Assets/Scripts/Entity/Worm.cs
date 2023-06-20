using Character;
using Spawner;
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
                WormSpawner.Instance.RemoveWorm(gameObject);
                _timeSinceLastSpawn = 0f;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Player.Instance.SetXp(_xp);
                ScoreManager.Instance.UpdateScore(ScoreManager.ScoreType.Worm,ScoreManager.Instance.GetScore(ScoreManager.ScoreType.Worm)+1);
                _pooledObject.ReturnToPool();
                WormSpawner.Instance.RemoveWorm(gameObject);
  
            }

            if (other.CompareTag("Worker"))
            {
                if (other.gameObject.GetComponent<WorkerChicken>().IsCarrying()) return;
                other.gameObject.GetComponent<WorkerChicken>().SetCarrying(true);
                
                _pooledObject.ReturnToPool();
                
                
            }
        }
        

    }
}
