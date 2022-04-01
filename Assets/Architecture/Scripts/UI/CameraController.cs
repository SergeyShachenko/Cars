using Cinemachine;
using Services;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _camera;
        private GameServices _gameServices;
        private Transform _defaultTarget;
        private float _defaultFOV;
        private Quaternion _defaultRotation;
        

        private void Start()
        {
            _gameServices = GameServices.Instance;
            _defaultTarget = _camera.m_LookAt;
            _defaultFOV = _camera.m_Lens.FieldOfView;
            _defaultRotation = transform.rotation;
        }

        public void LookAt(Transform target)
        {
            _camera.m_LookAt = target;
            _camera.m_Lens.FieldOfView = _gameServices.GameData.Settings.AimFOV;
        }
        
        public void ResetCamera()
        {
            _camera.m_LookAt = _defaultTarget;
            _camera.m_Lens.FieldOfView = _defaultFOV;
            transform.rotation = _defaultRotation;
        }
    }
}