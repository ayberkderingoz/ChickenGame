using Character;
using UnityEngine;

namespace Entity
{
    public class Egg : MonoBehaviour
    {
        private PooledObject _pooledObject;
        private bool isIncubating = false;
        private bool _isPickable = false;
        
        
        public void SetPickable(bool value)
        {
            _isPickable = value;
        }
        public bool GetPickable()
        {
            return _isPickable;
        }


        public void SetIncubating(bool value)
        {
            isIncubating = value;
        }
        public void SetPooledObject(PooledObject obj)
        {
            _pooledObject = obj;
        }
        public PooledObject GetPooledObject()
        {
            return _pooledObject;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (isIncubating) return;
                if(!_isPickable) return;
                if (Player.Instance.IsEggInDictionary(_pooledObject)) return;
                Player.Instance.StackEgg(_pooledObject.gameObject, _pooledObject);
            }
            
        }
    }
}