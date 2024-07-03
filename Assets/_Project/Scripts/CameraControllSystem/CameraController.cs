using _Project.Scripts.ServiceLocatorSystem;
using _Project.Scripts.StatsAndBuffsSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts.CameraControllSystem
{
    public interface ICameraController
    {
        void SetupLookAtPosition(Vector3 lookAtPosition);
        Camera GameCamera { get; }
    }

    public class CameraController : MonoBehaviour, ICameraController
    {
        [Header("References")]
        [SerializeField]
        private Transform _cameraAnchor1;

        [SerializeField]
        private Transform _cameraAnchor2;

        [SerializeField]
        private Camera _camera;

        [Space]
        [Header("Settings")]
        [SerializeField]
        private bool _enableFov;

        [SerializeField]
        private bool _enableRotation;

        [SerializeField]
        private bool _enableRoam;

        private CameraModel _cameraModel;
        private Vector3 _targetPosition;
        private float _rotationAngle;
        private float _currentFov;
        private Vector3 _lookAtPosition;
        private bool _isInitialized;
        private float _rotationTime;
        private float _roamingOffset;
        private float _roamingTime;
        private float _waitTime;
        private float _fovTarget;
        private float _fovElapsed;

        public Camera GameCamera => _camera;

        private void Awake()
        {
            ServiceLocator.For(this).Register<ICameraController>(this);
            ServiceLocator.For(this).Register(_camera);
            _cameraModel = ServiceLocator.For(this).Get<IConfigProvider>().Data.cameraSettings;
        }


        private void Update()
        {
            if (!_isInitialized)
            {
                return;
            }

            _cameraAnchor2.LookAt(_targetPosition);

            if (_enableRotation)
            {
                RotateCamera();
            }

            if (_enableRoam)
            {
                RoamCamera();
            }

            if (_enableFov)
            {
                AdjustFov();
            }
        }

        public void SetupLookAtPosition(Vector3 lookAtPosition)
        {
            _cameraAnchor1.position = lookAtPosition + new Vector3(0, _cameraModel.height, 0);
            _targetPosition = lookAtPosition + new Vector3(0, _cameraModel.lookAtHeight, 0);
            _cameraAnchor2.localPosition = new Vector3(0, 0, _cameraModel.roundRadius);
            _currentFov = _cameraModel.fovMin;
            _fovTarget = Random.Range(_cameraModel.fovMin, _cameraModel.fovMax);
            _waitTime = _cameraModel.fovDelay;

            _isInitialized = true;
        }

        private void RotateCamera()
        {
            const float rotationAngle = 360;

            _rotationAngle += rotationAngle / _cameraModel.roundDuration * Time.deltaTime;

            if (_rotationAngle > rotationAngle)
                _rotationAngle -= rotationAngle;

            _cameraAnchor1.rotation = Quaternion.Euler(0, _rotationAngle, 0);
        }

        private void RoamCamera()
        {
            if (_roamingTime <= 0)
            {
                _roamingOffset = _cameraModel.roamingRadius;
                _roamingTime = _cameraModel.roamingDuration;
            }

            var direction = Mathf.Sign(_roamingOffset);
            var speed = Mathf.Abs(_roamingOffset) / _cameraModel.roamingDuration;

            _cameraAnchor2.localPosition = new Vector3(
                _cameraAnchor2.localPosition.x,
                _cameraAnchor2.localPosition.y,
                _cameraAnchor2.localPosition.z + direction * speed * Time.deltaTime
            );

            _roamingTime -= Time.deltaTime;

            if (_roamingTime > 0)
                return;

            _roamingOffset = -_roamingOffset;
            _roamingTime = _cameraModel.roamingDuration;
        }


        private void AdjustFov()
        {
            if (_waitTime > 0)
            {
                _waitTime -= Time.deltaTime;
                return;
            }

            if (_fovElapsed < _cameraModel.fovDuration)
            {
                _fovElapsed += Time.deltaTime;
                _currentFov = Mathf.Lerp(_camera.fieldOfView, _fovTarget, _fovElapsed / _cameraModel.fovDuration);
            }
            else
            {
                _fovElapsed = 0f;
                _fovTarget = Random.Range(_cameraModel.fovMin, _cameraModel.fovMax);
                _waitTime = _cameraModel.fovDelay;
            }

            _camera.fieldOfView = _currentFov;
        }
    }
}