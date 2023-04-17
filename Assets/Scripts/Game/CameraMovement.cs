
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace SVS
{

    public class CameraMovement : MonoBehaviour
    {
        public Camera gameCamera;
        public float cameraMovementSpeed = 5;

        private void Start()
        {
            gameCamera = GetComponent<Camera>();
        }
        public void MoveCamera(Vector3 inputVector)
        {
            var movementVector = Quaternion.Euler(0, 30, 0) * inputVector;
            gameCamera.transform.position += movementVector * Time.deltaTime * cameraMovementSpeed;
        }

        internal void ZoomIn()
        {
            if (GetComponent<Camera>().fieldOfView > 1)
                GetComponent<Camera>().fieldOfView--;

        }
        internal void ZoomOut()
        {
            if (GetComponent<Camera>().fieldOfView < 50)
            GetComponent<Camera>().fieldOfView++;
        }
    }
}