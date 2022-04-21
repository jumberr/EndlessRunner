using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.CodeBase.Logic.Player
{
    public class PlayerInput : MonoBehaviour
    {
        public event Action OnJump;

        private InputMaster _input;

        private void Awake() => 
            _input = new InputMaster();

        private void OnEnable()
        {
            _input.Enable();
            Subscribe();
        }

        private void OnDisable()
        {
            UnSubscribe();
            _input.Disable();
        }

        private void Subscribe() => 
            _input.Player.Jump.performed += JumpAction();

        private void UnSubscribe() => 
            _input.Player.Jump.performed -= JumpAction();

        private Action<InputAction.CallbackContext> JumpAction() => 
            ctx => OnJump?.Invoke();
    }
}