using _Project.CodeBase.Constants;
using UnityEngine;

namespace _Project.CodeBase.Logic.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private PlayerInput _input;
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _doubleJumpForce;
        
        [Header("Force should be positive number"), Tooltip("Force should be positive number")]
        [SerializeField] private float _fallingForce;
        
        private PlayerState _state;
        private float _gravity = -9.81f;

        private void Start()
        {
            _input.OnJump += JumpBehaviour;
            _state = PlayerState.InAir;
        }

        private void FixedUpdate() => 
            ApplyGravity();

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(TagConstants.Ground)) 
                _state = PlayerState.Grounded;
        }

        private void OnDisable() => 
            _input.OnJump -= JumpBehaviour;

        private void JumpBehaviour()
        {
            _rb.velocity = Vector2.zero;
            if (_state == PlayerState.Grounded)
                DefaultJump();
            else if (_state == PlayerState.InAir)
                DoubleJump();
            else if (_state == PlayerState.AtCeiling) 
                Falling();
        }

        private void DefaultJump()
        {
            _state = PlayerState.InAir;
            Jump(_jumpForce);
        }

        private void DoubleJump()
        {
            _state = PlayerState.AtCeiling;
            Jump(_doubleJumpForce);
            ChangeGravity();
        }

        private void Falling()
        {
            _state = PlayerState.InAir;
            Jump(-1 * _fallingForce);
            ChangeGravity();
        }

        private void Jump(float force) => 
            _rb.AddForce(Vector2.up * force);

        private void ApplyGravity() => 
            _rb.velocity += Vector2.up * _gravity * Time.deltaTime;

        private void ChangeGravity() =>
            _gravity *= -1;
    }
}