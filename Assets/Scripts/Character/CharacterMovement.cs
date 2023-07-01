using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Character
{
    public class CharacterMovement : MonoBehaviour
    {

        public float moveSpeed = 5f;
        private bool _isGrounded = true;
        private Rigidbody _rigidBody;
        [SerializeField] private FixedJoystick _joystick;
        private Animator _animator;
        private Vector3 _lastMovement;
        private float _timer = 0.0f;
        private float _waitTime = .5f;
        private Transform _direction;
        private bool _isPulling;

        


        public void SetPullingMode(float speed,Transform direction, bool isPulling)
        {
            moveSpeed = speed;
            _direction = direction;
            _isPulling = isPulling;
            
        }

        public void SetPullingMode(float speed,bool isPulling)
        {
            
            moveSpeed = speed;
            _isPulling = isPulling;
            _direction = null;
            

        }

        
        public void SetMoveSpeed(float speed)
        {
            moveSpeed = speed;
        }
        
        
        
        public static CharacterMovement Instance { get; private set; }
        private void Awake()
        {
            Instance = this;
            _animator = GetComponentInChildren<Animator>();
            
            _rigidBody = GetComponent<Rigidbody>();
        }
        

        private void FixedUpdate()
        {
            //_rigidBody.velocity = new Vector3(_joystick.Horizontal * moveSpeed, _rigidBody.velocity.y, _joystick.Vertical * moveSpeed);

            float moveX = _joystick.Horizontal;
            float moveZ = _joystick.Vertical;
            Vector3 movement = new Vector3(moveX, 0, moveZ);
            
            if (movement != Vector3.zero && _joystick.isActiveAndEnabled && !_isPulling)
            {
                
                transform.rotation = Quaternion.LookRotation(new Vector3(moveX, 0, moveZ));
                _rigidBody.velocity = new Vector3(_joystick.Horizontal * moveSpeed,
                    _rigidBody.velocity.y, _joystick.Vertical * moveSpeed);
                _lastMovement = _rigidBody.velocity;
            }
            else if (movement != Vector3.zero && _joystick.isActiveAndEnabled && _isPulling)
            {
                //smooth rotate to direction
                transform.rotation = Quaternion.LookRotation(_direction.position - transform.position);
                _rigidBody.velocity = new Vector3(_joystick.Horizontal * moveSpeed,
                    _rigidBody.velocity.y, _joystick.Vertical * moveSpeed);
                _lastMovement = _rigidBody.velocity;
            }
            else
            {
                _rigidBody.AddForce(_lastMovement * 0.1f, ForceMode.Impulse);
                _lastMovement = Vector3.zero;
                _timer += Time.deltaTime;
                if (_timer >= _waitTime)
                {
                    _timer = 0.0f;
                }
            }
        }
        //wait for 1 second

        /*private void Update()
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");
        

        
        
            Vector3 movement = new Vector3(moveX, 0,moveZ);
        
            if (movement != Vector3.zero)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    moveSpeed = 10f;
                    GetComponent<Animator>().runtimeAnimatorController = Resources.Load("BasicMotions@Sprint") as RuntimeAnimatorController;
                }
                else
                {
                    moveSpeed = 5f;
                    GetComponent<Animator>().runtimeAnimatorController = Resources.Load("BasicMotions@Walk") as RuntimeAnimatorController;
            
                }
                transform.rotation = Quaternion.LookRotation(new Vector3(moveX, 0, moveZ));
            }
            else
            {
            
            }
            _rigidBody.velocity = movement.normalized * moveSpeed;
            if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
            {
                _rigidBody.AddForce(Vector3.up * 20f, ForceMode.Impulse);
                GetComponent<Animator>().runtimeAnimatorController = Resources.Load("BasicMotions@Jump") as RuntimeAnimatorController;
                _isGrounded = false;
            
            }
        
        }
        //jump smoothly

    
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                _isGrounded = true;
                Debug.Log("xD");
                GetComponent<Animator>().runtimeAnimatorController = Resources.Load("BasicMotions@Idle") as RuntimeAnimatorController;
            }
        }*/
    
    

    }
}

