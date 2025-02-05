using System;
using BuildingViewer.Input;
using UnityEngine;

namespace TV.Camera
{
    /// <summary>
    /// Shamelessly lifted from https://gist.github.com/3dln/c16d000b174f7ccf6df9a1cb0cef7f80
    /// Modified by Arvin G
    /// </summary>
    public class CameraOrbit : MonoBehaviour
    {
        public GameObject target;
        public float minDistance = 0.5f;
        public float maxDistance = 10f;
        public float distance = 4.0f;

        public float xSpeed = 250.0f;
        public float ySpeed = 120.0f;

        public float yMinLimit = -20;
        public float yMaxLimit = 80;

        private float _x = 0.0f;
        private float _y = 0.0f;

        private float _prevDistance;
        private bool _didMouseStartOnScreen;
        private BuildingViewerActions _actions;

        private string _filePath;

        private void Awake()
        {
            _actions = new();
            _actions.Enable();
        }

        private void Start()
        {
            var angles = transform.eulerAngles;
            _x = angles.y;
            _y = angles.x;
        }

        private void LateUpdate()
        {
            if (!target) return;

            float scrollY = _actions.Player.Zoom.ReadValue<Vector2>().y;
            if (Math.Abs(scrollY) > 0)
            {
                distance *= 1 - scrollY * 0.1f;
                distance = Mathf.Clamp(distance, minDistance, maxDistance);
            }

            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                var pos = Input.mousePosition;
                _didMouseStartOnScreen = Screen.safeArea.Contains(pos);
                return;
            }

            if (CanMoveCamera() && _didMouseStartOnScreen && (Input.GetMouseButton(0) || Input.GetMouseButton(1)))
            {
                // comment out these two lines if you don't want to hide mouse curser or you have a UI button
                // Cursor.visible = false;
                // Cursor.lockState = CursorLockMode.Locked;
                _x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
                _y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

                _y = ClampAngle(_y, yMinLimit, yMaxLimit);
                var rotation = Quaternion.Euler(_y, _x, 0);
                var position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.transform.position;
                transform.rotation = rotation;
                transform.position = position;
            }
            else
            {
                // comment out these two lines if you don't want to hide mouse cursor or you have a UI button
                // Cursor.visible = true;
                // Cursor.lockState = CursorLockMode.None;
                _didMouseStartOnScreen = false;
            }

            if (Mathf.Abs(_prevDistance - distance) > 0.001f)
            {
                _prevDistance = distance;
                var rot = Quaternion.Euler(_y, _x, 0);
                var po = rot * new Vector3(0.0f, 0.0f, -distance) + target.transform.position;
                transform.rotation = rot;
                transform.position = po;
            }
        }

        private bool CanMoveCamera()
        {
            return true;
        }

        static float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360)
                angle += 360;
            if (angle > 360)
                angle -= 360;
            return Mathf.Clamp(angle, min, max);
        }
    }
}