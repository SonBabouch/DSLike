using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

namespace BP
{ 
    public class PlayerInputHandler : MonoBehaviour
    {
        public float horizontalAxis;
        public float verticalAxis;
        public float moveAmount;
        public float mouseX;
        public float mouseY;

        PlayerControls inputActions;
        CameraHandler cameraHandler;

        Vector2 movementAmount;
        Vector2 cameraInput;

        private void Awake()
        {
            cameraHandler = CameraHandler.singeltonCam;
        }

        private void FixedUpdate()
        {
            float delta = Time.deltaTime;

            if(cameraHandler != null)
            {
                cameraHandler.FollowTarget(delta);
                cameraHandler.HandleCameraRotation(delta, mouseX, mouseY);
            }
        }
        public void OnEnable()
        {
            if(inputActions == null)
            {
                inputActions = new PlayerControls();
                //After += is the same as calling func OnMove (Called Lambda function)
                //private void OnMove(InputAction.CallbackContext context)
                //{
                //    movementDirection = context.ReadValue<Vector2>();
                //}
                inputActions.PlayerMovement.Movement.performed += inputAction => movementAmount = inputAction.ReadValue<Vector2>();
                // Same Here
                inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
            }
            inputActions.Enable();
        }



        public void OnDisable()
        {
            inputActions.Disable();
        }

        public void TickInput(float delta)
        {
            MoveInput(delta);
        }

        public void MoveInput(float delta)
        {
            horizontalAxis = movementAmount.x;
            verticalAxis = movementAmount.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalAxis) + Mathf.Abs(verticalAxis));
            mouseX = cameraInput.x;
            mouseY = cameraInput.y;
        }
    }
}
