using Services;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Vehicles;

namespace UI
{
    public class VehicleUIController : MonoBehaviour, IPointerClickHandler
    {
        public bool IsVisible => _isVisible;

        [SerializeField] private Vehicle _vehicle;
        [SerializeField] private Transform _canvas;
        [SerializeField] private Outline _outline;
        [SerializeField] private TMP_Text _textOwner, _textSpeed;
        private GameServices _gameServices;
        private bool _isVisible;


        private void Start()
        {
            _gameServices = GameServices.Instance;
            _textOwner.text = _vehicle.Data.Owner;

            VisibleUI(false);
        }

        private void Update()
        {
            _canvas.LookAt(_canvas.position + _gameServices.SceneData.CameraController.transform.forward);
            _textSpeed.text = Mathf.Round(_vehicle.CurrentSpeed).ToString();
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            VisibleUI(!_isVisible);

            if (_isVisible)
            {
                if (_gameServices.SceneData.PreviousSelectedVehicle != null)
                    _gameServices.SceneData.PreviousSelectedVehicle.UI.VisibleUI(false);
                
                _gameServices.SceneData.PreviousSelectedVehicle = _vehicle;
                _gameServices.SceneData.CameraController.LookAt(transform);
            }
            else
            {
                _gameServices.SceneData.CameraController.ResetCamera();
            }
        }
        

        public void VisibleUI(bool isVisible)
        {
            _isVisible = isVisible;
            _outline.enabled = isVisible;
            _textOwner.transform.parent.gameObject.SetActive(isVisible);
            _textSpeed.transform.parent.gameObject.SetActive(isVisible);
        }
    }
}