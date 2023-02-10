using System;
using System.Collections;
using System.Collections.Generic;
using TefDara.Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace  TefDara.OK.Input
{
    public class InputManager : Singleton<InputManager>
    {
        private PlayerInputMap _inputMap;

        public event Action<Vector3> OnMove = delegate{  };
        public event Action<Vector3> OnLook = delegate{  };
        public event Action OnJump = delegate{  };
        public event Action<bool> OnRun = delegate{  };
        public event Action<Vector2> OnUiRotate = delegate{ };
        public event Action OnInteract = delegate {  };
        public bool TriggerPressed { get; private set; }
        protected override void Awake()
        {
            base.Awake();
            _inputMap = new PlayerInputMap();
        }


        private void OnEnable()
        {
            _inputMap.Enable();
            _inputMap.Player.Move.performed += c => OnMoved(c);
            _inputMap.Player.Look.performed += c => OnLooked(c);
            _inputMap.Player.Jump.performed += c => OnJumped(c);
            _inputMap.Player.Run.performed += c => OnRan(c);
            _inputMap.UI.RotateObject.performed += c => OnUiRotated(c);
            _inputMap.Player.Fire.performed += c => OnTriggerPressed(c);
            _inputMap.Player.Interact.performed += OnInteraction;
        }

        private void OnInteraction(InputAction.CallbackContext obj)
        {
            OnInteract?.Invoke();
        }

        private void OnTriggerPressed(InputAction.CallbackContext callbackContext)
        {
            var triggerState = callbackContext.ReadValue<float>();
            TriggerPressed = triggerState > 0.5f;
        }

        private void OnUiRotated(InputAction.CallbackContext callbackContext)
        {
            if(!TriggerPressed)
                return;

            var mousePosition = callbackContext.ReadValue<Vector2>();
            OnUiRotate?.Invoke(mousePosition);
        }

        private void OnRan(InputAction.CallbackContext callbackContext)
        {
            OnRun?.Invoke(callbackContext.ReadValue<float>() > 0.5f);
        }

        private void OnJumped(InputAction.CallbackContext callbackContext)
        {
            if (_inputMap.Player.Jump.triggered)
                OnJump?.Invoke();
        }

        private void OnLooked(InputAction.CallbackContext callbackContext)
        {
            Vector2 lookVector = callbackContext.ReadValue<Vector2>();
            Vector3 lookDirection = new Vector3(lookVector.x, 0, lookVector.y);
            OnLook?.Invoke(lookDirection);
        }

        private void OnMoved(InputAction.CallbackContext callbackContext)
        {
            Vector2 moveVector = callbackContext.ReadValue<Vector2>();
            Vector3 moveDirection = new Vector3(moveVector.x, 0, moveVector.y);
            OnMove?.Invoke(moveDirection);
        }
    }
    
}
