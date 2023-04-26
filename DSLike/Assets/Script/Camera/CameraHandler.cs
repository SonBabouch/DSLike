using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BP
{
    public class CameraHandler : MonoBehaviour
    {
        public Transform targetTransform;
        public Transform cameraTransform;
        public Transform cameraPivotTransform;
        private Transform myTransform;
        private Vector3 cameraTransformPosition;
        private LayerMask ignoreLayers;

        public static CameraHandler singeltonCam;

        public float lookSpeed = 0.1f;
        public float followSpeed = 0.1f;
        public float pivotSpeed = 0.03f;

        private float defaultPosition;
        private float lookAngle;
        private float pivotAngle;
        public float minimumPivot = -35f;
        public float maximumPivot = 35f;

        private void Awake()
        {
            singeltonCam = this;
            myTransform = transform;
            defaultPosition = cameraTransform.localPosition.z;
            // Bit shift index of the layer ==> ~Inverse l'effet donc ici le layer 8,9 et 10 sont ignoré peut egalement faire une struct mais moins opti
            ignoreLayers = ~(1 << 8 | 1 << 9 | 1 << 10);
        }
    }
}
