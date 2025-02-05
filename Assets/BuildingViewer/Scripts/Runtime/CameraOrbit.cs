using System;
using BuildingViewer.Input;
using Unity.Cinemachine;
using UnityEngine;

namespace TV.Camera
{
    /// <summary>
    /// Shamelessly lifted from https://gist.github.com/3dln/c16d000b174f7ccf6df9a1cb0cef7f80
    /// Modified by Arvin G
    /// </summary>
    public class CameraOrbit : MonoBehaviour
    {
        private GameObject target => _cmCamera.LookAt.gameObject;
        
        [SerializeField] private float _minDistance = 0.5f;
        [SerializeField] private float _maxDistance = 10f;
        [SerializeField] private float _distance = 4.0f;

        [SerializeField] private float _xSpeed = 250.0f;
        [SerializeField] private float _ySpeed = 120.0f;

        [SerializeField] private float _yMinLimit = -20;
        [SerializeField] private float _yMaxLimit = 80;

        private float _x;
        private float _y;

        private float _prevDistance;
        private bool _didMouseStartOnScreen;
        private BuildingViewerActions _actions;

        private CinemachineCamera _cmCamera;

        private void Awake()
        {
            _actions = new();
            _actions.Enable();
            _cmCamera = GetComponent<CinemachineCamera>();
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
                _distance *= 1 - scrollY * 0.1f;
                _distance = Mathf.Clamp(_distance, _minDistance, _maxDistance);
            }

            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                var pos = Input.mousePosition;
                _didMouseStartOnScreen = Screen.safeArea.Contains(pos);
                return;
            }

            if (_didMouseStartOnScreen && (Input.GetMouseButton(0) || Input.GetMouseButton(1)))
            {
                _x += Input.GetAxis("Mouse X") * _xSpeed * 0.02f;
                _y -= Input.GetAxis("Mouse Y") * _ySpeed * 0.02f;

                _y = ClampAngle(_y, _yMinLimit, _yMaxLimit);
                var rotation = Quaternion.Euler(_y, _x, 0);
                var position = rotation * new Vector3(0.0f, 0.0f, -_distance) + target.transform.position;
                transform.rotation = rotation;
                transform.position = position;
            }
            else
            {
                _didMouseStartOnScreen = false;
            }

            if (Mathf.Abs(_prevDistance - _distance) > 0.001f)
            {
                _prevDistance = _distance;
                var rot = Quaternion.Euler(_y, _x, 0);
                var po = rot * new Vector3(0.0f, 0.0f, -_distance) + target.transform.position;
                transform.rotation = rot;
                transform.position = po;
            }
        }

        private static float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360)
                angle += 360;
            if (angle > 360)
                angle -= 360;
            return Mathf.Clamp(angle, min, max);
        }
    }
}