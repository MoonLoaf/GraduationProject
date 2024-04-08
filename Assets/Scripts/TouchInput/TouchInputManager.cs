using UnityEngine;
using UnityEngine.InputSystem;
using Utility;

namespace TouchInput
{
    public delegate void TouchPositionHandler(InputAction.CallbackContext context);
    [DefaultExecutionOrder(-1)]
    public class TouchInputManager : GenericSingleton<TouchInputManager>
    {
        private PlayerInput _playerInput;

        private InputAction _touchPositionAction;
        
        public static event TouchPositionHandler OnTouchStartPosition;
        
        protected override void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _touchPositionAction = _playerInput.actions["TouchPosition"];
        }

        private void OnEnable()
        {
            _touchPositionAction.performed += TouchPerformed;
        }

        private void OnDisable()
        {
            _touchPositionAction.performed -= TouchPerformed;
        }

        private void TouchPerformed(InputAction.CallbackContext context)
        {
            OnTouchStartPosition?.Invoke(context);
        }
    }
}
