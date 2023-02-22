using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JetMonkey.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float acceleration = 2f;
        [SerializeField] private float maxFlyVelocity = 2f;
        [SerializeField] private float maxFallVelocity = 2f;

        [SerializeField] private Transform groundCheck;
        [SerializeField] private LayerMask groundLayer;



        public Vector2 CurrentVelocity { get; private set; }
        public bool isJumping { get; private set; } = false;
        public bool isGrounded { get; private set; } = false;
        private bool jetpackActive = false;

        private Rigidbody2D RB;

        private void Awake()
        {
            RB = GetComponent<Rigidbody2D>();

        }
        private void Start()
        {
            RB.velocity = new Vector3(6, RB.velocity.y);
        }

        private void Update()
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
            Inputs();
            MoveY();
            Fall();

            

            CurrentVelocity = RB.velocity;

            if(Input.GetKeyDown(KeyCode.R)) 
            {
                SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
            }
        }

        private void MoveY()
        {
            //Limitador de velocidade
            RB.velocity = new Vector2(RB.velocity.x, Mathf.Min(RB.velocity.y, maxFlyVelocity));

            float newAccelerations = 0f;
            if (isJumping)
            {
                 newAccelerations = Mathf.Lerp(0f, acceleration, 0.5f);
                isJumping = false;
            }
                RB.velocity = new Vector2(RB.velocity.x, RB.velocity.y + newAccelerations);
                //Acelera o Player em Y
                
        }

        private void Fall()
        {
            if(RB.velocity.y < 0f)
            {
            RB.velocity = new Vector2(RB.velocity.x, Mathf.Max(RB.velocity.y, -maxFallVelocity));

            }

        }

        private void Inputs()
        {
            if (Input.GetButton("Jump"))
            {
                isJumping = true;
            }

            
        }
    }
}
