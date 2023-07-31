using System.Collections;
using Character;
using FIMSpace.FTail;
using Spawner;
using UI;
using Unity.VisualScripting;
using UnityEngine;

namespace Entity
{
    public class Worm : MonoBehaviour
    {


        private int _level;
        private float _timeSinceLastSpawn = 0f;
        [SerializeField] private float _objectDuration = 10f;
        private PooledObject _pooledObject;
        private int _xp = 20;
        public bool isTargeted = false;
        private Vector3 _headFirstPos;
        public GameObject levelText;
        public PooledObject levelPooledObject;

        void Awake()
        {
            _objectDuration = Random.Range(14, 26);
        }
        public void SetLevel(int level)
        {
            _level = level;
        }
        
        
        public void SetPooledObject(PooledObject pooledObject)
        {
            _pooledObject = pooledObject;
            
        }
        
        private void Update()
        {
            _timeSinceLastSpawn += Time.deltaTime;
            if (_timeSinceLastSpawn >= _objectDuration && !isTargeted)
            {
                _pooledObject.ReturnToPool();
                levelPooledObject.ReturnToPool();
                WormSpawner.Instance.RemoveWorm(gameObject);
                _timeSinceLastSpawn = 0f;
            }
        }

        private IEnumerator GetPulled(Transform head)
        {
            
            
            if (Vector3.Distance(transform.position, Player.Instance.transform.position) < 3f)
            {
                //head.position = Player.Instance.transform.position;
                CharacterMovement.Instance.SetPullingMode(3f,transform,true);
                head.position = Vector3.MoveTowards(head.position, Player.Instance.transform.position, 1f);
                yield return new WaitForSeconds(0.1f);
                StartCoroutine(GetPulled(head));
                
            }
            else
            {
                StopAllCoroutines();
                CharacterMovement.Instance.SetPullingMode(5f,false);
                head.position = _headFirstPos;
                Player.Instance.SetPulling(false);
                _pooledObject.ReturnToPool();
                levelPooledObject.ReturnToPool();
                WormSpawner.Instance.RemoveWorm(gameObject);
                Player.Instance.SetXp(_xp);
                ScoreManager.Instance.UpdateScore(ScoreManager.ScoreType.Worm,
                    ScoreManager.Instance.GetScore(ScoreManager.ScoreType.Worm) + 1);
                yield return new WaitForSeconds(0.5f);
            }
        }

        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (Player.Instance.GetLevel() >= _level)
                {
                    var head = transform.Find("Worm_Rig_bake/Armature/SplinIK_3/body/Head");
                    _headFirstPos = head.position;
                    if (Player.Instance.IsPulling()) return;
                    Player.Instance.SetPulling(true);
                    StartCoroutine(GetPulled(head));
                }
                else
                {
                    //throw player opposite direction from worm
                    var direction = (Player.Instance.transform.position - transform.position).normalized;

                    CharacterMovement.Instance.SetThrown(true);
                    Player.Instance.GetComponent<Rigidbody>().AddForce(direction * 400f);
                    
                }

            }
        
            if (other.CompareTag("Worker"))
            {
                if (other.gameObject.GetComponent<WorkerChicken>().IsCarrying()) return;
                other.gameObject.GetComponent<WorkerChicken>().SetCarrying(true);
                
                _pooledObject.ReturnToPool();
                levelPooledObject.ReturnToPool();
                
                
            }
        }
        

    }
}
