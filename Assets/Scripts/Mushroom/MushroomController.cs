using UnityEngine;

namespace Mushroom
{
    public class MushroomController : MonoBehaviour
    {
        //make this class singleton
        public static MushroomController Instance { get; private set; }
        private void Awake()
        {
            Instance = this;
        }
        private void OnMouseDown()
        {
            ScoreManager.Instance.UpdateScore();
            
        }
        
        
        
        
        
        
        
        
    
    }
}
