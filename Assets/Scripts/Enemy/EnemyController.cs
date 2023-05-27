using System;
using Mushroom;
using Character;
using UnityEngine;

namespace Enemy
{
    public class EnemyController : MonoBehaviour
    {
        // Start is called before the first frame update
        //move to the player
        private GameObject _village;

        private GameObject _player;
        //speed of enemy
        public float speed;
        // Use this for initialization
        void Start()
        {

        }

        private void FixedUpdate()
        {
 
            

            
        }

        // Update is called once per frame
        /*void Update() // NAVMESH YAP BU NE AMK
        {
            _player = CharacterMovement.Instance.gameObject;
            _village = MushroomController.Instance.gameObject;
            
            
            if (Vector3.Distance(transform.position, _player.transform.position) < .01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, .01f);
                transform.LookAt(_player.transform);

            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, _village.transform.position, .01f);
                transform.LookAt(_village.transform);

            }
            GetComponentInChildren<Animator>().runtimeAnimatorController = Resources.Load("BasicMotions@Walk") as RuntimeAnimatorController;
        
        }*/
    }
}
