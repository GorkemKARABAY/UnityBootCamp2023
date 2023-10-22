using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Platformer
{
    public class PlayerControler : MonoBehaviour
    {
        
        private GameManager _gameManager;
        private PlayerData _playerData;
        public PlayerData Data { get { return _playerData; } private set { _playerData = value; } }
        private PlayerAgent _agent;

        [SerializeField]
        Rigidbody2D _rigidBody;

        [SerializeField]
        private AudioClip _pickUpClip;

        [SerializeField]
        private AudioClip _damageClip;

        [SerializeField]
        private bool _isGrounded;

        [SerializeField]
        private Transform _groundCheck;

        [SerializeField]
        private bool _isPaused;


        private void Start ()
        {
            _playerData = GetComponent<PlayerData>();
            _agent = GetComponent<PlayerAgent>();

            if(_rigidBody == null)
            {
                _rigidBody = GetComponent<Rigidbody2D>();
            }

            if (_groundCheck == null)
            {
                _groundCheck = transform.Find("TouchGround");
            }
            
            _isPaused = false;
        }

        public void Init(GameManager gameManager)
        {
            _gameManager = gameManager;
            _gameManager.OnLevelStarted += StartNewLevel;
            _gameManager.OnLevelCompleted += LevelFinished;
        }

        private void OnDestroy()
        {
            _gameManager.OnLevelStarted -= StartNewLevel;
            _gameManager.OnLevelCompleted -= LevelFinished;
        }

        private void LevelFinished()
        {
            _isPaused = true;

        }

        private void StartNewLevel()
        {
            _playerData.CoinsCollected = 0;
            transform.position = Vector3.zero;
            _agent.StopAnimations();
            _isPaused = false;
        }

        private void FixedUpdate()
        {
            if (_isPaused)
            {
                return;
            }

            int layerMask = LayerMask.GetMask("Floor");
            _isGrounded = Physics2D.OverlapPoint(_groundCheck.position, layerMask);

            float moveX = Input.GetAxis("Horizontal");          
            

            if (_isGrounded && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)))
            {
                _rigidBody.AddForce(Vector2.up * _playerData.JumpSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
                _isGrounded = false;
                _agent.Jump();
            }
            Vector2 newVelocity = new Vector2(moveX * _playerData.MoveSpeed * Time.fixedDeltaTime, _rigidBody.velocity.y);
            _rigidBody.velocity = newVelocity;

            _agent.Move(_rigidBody.velocity);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag == "Coin") 
            {
                _playerData.CoinsCollected += collision.GetComponent<PickUp>().GetPickUp();
                _gameManager.AudioPlayer.PlaySound(_pickUpClip);
            }
            else if(collision.tag == "Finish")
            {
                _gameManager.CheckIfLevelEnded();
            }
            else if(collision.tag == "Enemy")
            {
                _playerData.CoinsCollected--;
                _gameManager.AudioPlayer.PlaySound(_damageClip);
            }
        }
    }
}
