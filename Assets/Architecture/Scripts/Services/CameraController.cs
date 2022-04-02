using Cinemachine;
using Data;
using UnityEngine;

namespace Services
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _camera;
        private GameData _gameData;
        private Transform _defaultTarget;
        private float _defaultFOV;
        private Quaternion _defaultRotation;
        

        private void Start()
        {
            _gameData = GameServices.Instance.GameData;
            _defaultTarget = _camera.m_LookAt;
            _defaultFOV = _camera.m_Lens.FieldOfView;
            _defaultRotation = transform.rotation;
        }

        public void LookAt(Transform target)
        {
            _camera.m_LookAt = target;
            _camera.m_Lens.FieldOfView = _gameData.Settings.AimFOV;
        }
        
        public void ResetCamera()
        {
            _camera.m_LookAt = _defaultTarget;
            _camera.m_Lens.FieldOfView = _defaultFOV;
            transform.rotation = _defaultRotation;
        }
    }
}