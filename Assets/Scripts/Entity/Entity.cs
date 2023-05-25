using UnityEngine;

namespace Entity
{
    
    public abstract class Entity : MonoBehaviour
    {
        public abstract void SetPooledObject(PooledObject pooledObject);
    }
    
    
}