using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BP
{
    public class PlayerMovement : MonoBehaviour
    {
        Transform cameraObject;
        PlayerInputHandler inputHandler;
        Vector3 moveDirection;

        [HideInInspector]
        public Transform myTransform;
        [HideInInspector]
        public PlayerAnimatorHandler animatorHandler;

        public new Rigidbody rigidbody;
        public GameObject normalCarmera;

        [Header("Stats")]
        [SerializeField]
        float movementSpeed = 5;
        [SerializeField]
        float rotationSpeed = 10;

        private void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            inputHandler = GetComponent<PlayerInputHandler>();
            animatorHandler = GetComponentInChildren<PlayerAnimatorHandler>();
            cameraObject = Camera.main.transform;
            myTransform = transform;
            animatorHandler.Initialize();
        }

        public void Update()
        {
            float delta = Time.deltaTime;

            inputHandler.TickInput(delta);

            moveDirection = cameraObject.forward * inputHandler.verticalAxis;
            moveDirection += cameraObject.right * inputHandler.horizontalAxis;
            moveDirection.Normalize();

            float speed = movementSpeed;
            moveDirection *= speed;

            Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
            rigidbody.velocity = projectedVelocity;

            animatorHandler.UpdateAnimatorValues(inputHandler.moveAmount, 0);

            if(animatorHandler.canRotate)
            {
                HandleRotation(delta);
            }
        }

        #region movement
        Vector3 normalVector;
        Vector3 targetPosition;

        private void HandleRotation(float delta)
        {
            Vector3 targetDir = Vector3.zero;
            float moveOverride = inputHandler.moveAmount;

            targetDir = cameraObject.forward * inputHandler.verticalAxis;
            targetDir += cameraObject.right * inputHandler.horizontalAxis;

            targetDir.Normalize();
            targetDir.y = 0;

            if(targetDir == Vector3.zero)
                targetDir = myTransform.forward;

            float tempRotationSpeed = rotationSpeed;

            Quaternion tempTargetDir = Quaternion.LookRotation(targetDir);
            Quaternion targetRotation = Quaternion.Slerp(myTransform.rotation, tempTargetDir, tempRotationSpeed * delta);

            myTransform.rotation = targetRotation;

        }
        #endregion

    }
}
