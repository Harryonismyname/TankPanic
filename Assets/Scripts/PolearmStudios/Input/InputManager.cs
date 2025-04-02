using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections.Generic;

namespace PolearmStudios.Input
{
    [RequireComponent(typeof(PlayerInput))]
    public class InputManager : MonoBehaviour
    {
        public InputScheme CurrentInputScheme { get; private set; }

        public event Action OnRightTriggerPulled;
        public event Action OnRightTriggerReleased;
        public event Action OnLeftTriggerPulled;
        public event Action OnLeftTriggerReleased;


        public event Action OnSelectPressed;
        public event Action OnStartPressed;
        public event Action OnAction1Start;
        public event Action OnAction1Canceled;
        public event Action OnAction2Start;
        public event Action OnAction2Canceled;
        public event Action OnAction3Start;
        public event Action OnAction3Canceled;
        public event Action OnAction4Start;
        public event Action OnAction4Canceled;
        

        public event Action<Vector2> OnMove;
        public event Action<Vector2> OnAim;

        public event Action OnControlsLockout;

        PlayerInput input;
        InputAction aimInput;
        InputAction moveInput;
        InputAction rightTrigger;
        InputAction leftTrigger;
        InputAction select;
        InputAction start;
        InputAction jump;
        InputAction sprint;
        InputAction crouch;
        InputAction slot4;

        Vector2 movement;
        Vector2 aim;
        public bool Locked { get; private set; }


        readonly string MOUSE_AND_KEYBOARD_BINDING = "DefaultPC";
        readonly string CONTROLLER_BINDING = "ControllerInput";


        #region Input Lockout
        public void LockoutControls()
        {
            Locked = true;
            OnControlsLockout?.Invoke();
        }

        public void UnlockControls()
        {
            Locked = false;
        }
        #endregion

        private void Awake()
        {
            CurrentInputScheme = InputScheme.MOUSE_AND_KEYBOARD;
            input = GetComponent<PlayerInput>();
            CacheActions();
            SubscribeToEvents();

        }

        private void Start() => OnControlsChanged(input);


        private void FixedUpdate()
        {
            if (input != null)
            {
                movement = moveInput.ReadValue<Vector2>();
                if (movement.magnitude > 0.01f && !Locked) OnMove?.Invoke(movement);
            }
        }

        private void Update()
        {
            if (input != null)
            {
                aim = aimInput.ReadValue<Vector2>();
                if (aim.magnitude > 0.01f) OnAim?.Invoke(aim);
            }
        }

        private void OnDestroy() => UnsubscribeToEvents();

        private void SetControlScheme(int scheme) => CurrentInputScheme = scheme >= 1 ? InputScheme.CONTROLLER : InputScheme.MOUSE_AND_KEYBOARD;

        public void OnControlsChanged(PlayerInput input)
        {
            int newScheme = 0;
            if (input.currentControlScheme == CONTROLLER_BINDING)
            {
                LockMouse();
                newScheme = 1;
            }
            SetControlScheme(newScheme);
        }

        public void LockMouse()
        {
            if (CurrentInputScheme != InputScheme.MOUSE_AND_KEYBOARD) { return; }
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void UnlockMouse()
        {
            if (CurrentInputScheme != InputScheme.MOUSE_AND_KEYBOARD) { return; }
            Cursor.lockState = CursorLockMode.None;
        }

        private void RightTriggerPull(InputAction.CallbackContext context) {if(!Locked) OnRightTriggerPulled?.Invoke(); }

        private void ReleaseRightTrigger(InputAction.CallbackContext context) { if (!Locked) OnRightTriggerReleased?.Invoke(); }

        private void LeftTriggerPull(InputAction.CallbackContext context) { if (!Locked) OnLeftTriggerPulled?.Invoke(); }

        private void ReleaseLeftTrigger(InputAction.CallbackContext context) { if (!Locked) OnLeftTriggerReleased?.Invoke(); }

        private void SelectPerformed(InputAction.CallbackContext context) { if(!Locked) OnSelectPressed?.Invoke(); }
        
        private void StartPerformed(InputAction.CallbackContext context) { if (!Locked) OnStartPressed?.Invoke(); }
        
        private void Action1Start(InputAction.CallbackContext context) { if (!Locked) OnAction1Start?.Invoke(); }

        private void Action1Cancel(InputAction.CallbackContext context) { if (!Locked) OnAction1Canceled?.Invoke(); }
        
        private void Action2Start(InputAction.CallbackContext context) { if (!Locked) OnAction2Start?.Invoke(); }

        private void Action2Cancel(InputAction.CallbackContext context) { if (!Locked) OnAction2Canceled?.Invoke(); }

        private void Action3Start(InputAction.CallbackContext context) { if (!Locked) OnAction3Start?.Invoke(); }
        private void Action3Cancel(InputAction.CallbackContext context) { if (!Locked) OnAction3Canceled?.Invoke(); }

        private void Action4Start(InputAction.CallbackContext context) { if (!Locked) OnAction4Start?.Invoke(); }
        private void Action4Cancel(InputAction.CallbackContext context) { if (!Locked) OnAction4Canceled?.Invoke(); }

    
        private void SubscribeToEvents()
        {
            rightTrigger.performed += RightTriggerPull;
            rightTrigger.canceled += ReleaseRightTrigger;

            leftTrigger.performed += LeftTriggerPull;
            leftTrigger.canceled += ReleaseLeftTrigger;

            jump.performed += Action1Start;
            jump.canceled += Action1Cancel;

            sprint.performed += Action2Start;
            sprint.canceled += Action2Cancel;

            crouch.performed += Action3Start;
            crouch.canceled += Action3Cancel;
            //select.performed += SelectPerformed;
            //start.performed += StartPerformed;
            //slot4.performed += ActivateSlot4;
        }

        private void UnsubscribeToEvents()
        {
            //GameManager.OnStateChange -= HandleState;
            rightTrigger.performed -= RightTriggerPull;
            rightTrigger.canceled -= ReleaseRightTrigger;
            leftTrigger.performed -= LeftTriggerPull;
            leftTrigger.canceled -= ReleaseLeftTrigger;
            jump.performed -= Action1Start;
            jump.canceled -= Action1Cancel;
            //select.performed -= SelectPerformed;
            //start.performed -= StartPerformed;
            sprint.performed -= Action2Start;
            sprint.canceled -= Action2Cancel;
            crouch.performed -= Action3Start;
            crouch.canceled -= Action3Cancel;
            //slot4.performed -= ActivateSlot4;
        }

        private void CacheActions()
        {
            moveInput = input.actions["Walk"];
            aimInput = input.actions["Look"];
            rightTrigger = input.actions["RightHand"];
            leftTrigger = input.actions["LeftHand"];
            //select = input.actions["Cheat"];
            //start = input.actions["PauseGame"];
            jump = input.actions["Jump"];
            sprint = input.actions["Sprint"];
            crouch = input.actions["Crouch"];
            //slot4 = input.actions["ActivateSlot4"];
        }
    }

    public enum InputScheme
    {
        None = 0,
        MOUSE_AND_KEYBOARD,
        CONTROLLER,
    }

    public class InputBus
    {
        private readonly PlayerInput input;
        private readonly Dictionary<InputCommand, List<Action<InputAction.CallbackContext>>> inputEvents = new();

        private InputBus() { }
        public InputBus(PlayerInput _input)
        {
            input = _input;
        }

        public void Subscribe(InputCommand command, Action<InputAction.CallbackContext> callback)
        {
            if (input == null) return;
            var action = input.actions[command.ToString()];
            if(action == null) { Debug.LogWarning($"Action `{command}` not found."); return; }

            if (!inputEvents.ContainsKey(command)) inputEvents[command] = new();
            inputEvents[command].Add(callback);
            action.performed += callback;
        }
        public void Unsubscribe(InputCommand command, Action<InputAction.CallbackContext> callback)
        {
            if (inputEvents.TryGetValue(command, out var callbacks))
            {
                callbacks.Remove(callback);
                input.actions[command.ToString()].performed -= callback;
                if (callbacks.Count > 1) inputEvents.Remove(command);
            }
        }
    }
}

