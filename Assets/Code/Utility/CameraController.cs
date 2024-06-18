using System.Collections;
using Code.Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Utility
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private Transform _player1;

        [SerializeField]
        private Transform _player2;

        [SerializeField]
        private Transform _cameraAnchor1;

        [SerializeField]
        private Transform _cameraAnchor2;

        [SerializeField]
        private bool _enableFov;

        private CameraModel _cameraSettings;
        private Vector3 _targetPosition;
        private float _rotationAngle;
        private float _roamingOffset;
        private Camera _camera;
        private float _currentFov;

        private void Awake()
        {
            _cameraSettings = ServiceLocator.GetService<IDataLoader>().Data.cameraSettings;
            _camera = ServiceLocator.GetService<Camera>();
        }

        private void Start()
        {
            _cameraAnchor1.position = GetMidpoint() + new Vector3(0, _cameraSettings.height, 0);
            _targetPosition = GetMidpoint() + new Vector3(0, _cameraSettings.lookAtHeight, 0);
            _cameraAnchor2.localPosition = new Vector3(0, 0, -_cameraSettings.roundRadius);
            _currentFov = _cameraSettings.fovMin;

            StartCoroutine(RotateCamera());
            StartCoroutine(RoamCamera());
            StartCoroutine(AdjustFovCoroutine());
        }

        private void Update()
        {
            _cameraAnchor2.LookAt(_targetPosition);
        }

        private Vector3 GetMidpoint()
        {
            return (_player1.position + _player2.position) / 2;
        }

        private IEnumerator RotateCamera()
        {
            while (true)
            {
                _rotationAngle += 360 / _cameraSettings.roundDuration * Time.deltaTime;
                if (_rotationAngle > 360) _rotationAngle -= 360;

                _cameraAnchor1.rotation = Quaternion.Euler(0, _rotationAngle, 0);

                yield return null;
            }
        }

        private IEnumerator RoamCamera()
        {
            while (true)
            {
                var startZ = -_cameraSettings.roundRadius;
                var endZ = -(_cameraSettings.roundRadius + _cameraSettings.roamingRadius);
                var elapsedTime = 0f;

                while (elapsedTime < _cameraSettings.roamingDuration)
                {
                    elapsedTime += Time.deltaTime;
                    _roamingOffset = Mathf.Lerp(startZ, endZ, elapsedTime / _cameraSettings.roamingDuration);
                    _cameraAnchor2.localPosition = new Vector3(0, 0, _roamingOffset);

                    yield return null;
                }

                elapsedTime = 0f;

                while (elapsedTime < _cameraSettings.roamingDuration)
                {
                    elapsedTime += Time.deltaTime;
                    _roamingOffset = Mathf.Lerp(endZ, startZ, elapsedTime / _cameraSettings.roamingDuration);
                    _cameraAnchor2.localPosition = new Vector3(0, 0, _roamingOffset);

                    yield return null;
                }
            }
        }

        private IEnumerator AdjustFovCoroutine()
        {
            while (_enableFov)
            {
                yield return new WaitForSeconds(_cameraSettings.fovDelay);
                
                var targetFov = Random.Range(_cameraSettings.fovMin, _cameraSettings.fovMax);
                var elapsedTime = 0f;
                var initialFov = _camera.fieldOfView;

                while (elapsedTime < _cameraSettings.fovDuration)
                {
                    elapsedTime += Time.deltaTime;
                    _currentFov = Mathf.Lerp(initialFov, targetFov, elapsedTime / _cameraSettings.fovDuration);
                    _camera.fieldOfView = _currentFov;
                    yield return null;
                }
            }
        }
    }
}