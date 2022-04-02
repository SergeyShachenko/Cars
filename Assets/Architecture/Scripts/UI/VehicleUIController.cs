using Effects;
using Services;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Vehicles;

namespace UI
{
    public class VehicleUIController : MonoBehaviour, IPointerClickHandler
    {
        public bool IsVisible { get; private set; }

        [SerializeField] private Vehicle _vehicle;
        [SerializeField] private Outline _outline;
        [SerializeField] private Transform _canvas;
        [SerializeField] private TMP_Text _textOwner, _textSpeed;
        private GameServices _gameServices;


        private void Start()
        {
            _gameServices = GameServices.Instance;
            _textOwner.text = _vehicle.Data.Owner;

            VisibleUI(false);
        }

        private void Update()
        {
            _canvas.LookAt(_canvas.position + _gameServices.CameraController.transform.forward);
            _textSpeed.text = Mathf.Round(_vehicle.CurrentSpeed).ToString();
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            VisibleUI(!IsVisible);

            if (IsVisible)
            {
                if (_gameServices.SceneData.PreviousSelectedVehicle != null)
                    _gameServices.SceneData.PreviousSelectedVehicle.UI.VisibleUI(false);
                
                _gameServices.SceneData.PreviousSelectedVehicle = _vehicle;
                _gameServices.CameraController.LookAt(transform);
            }
            else
            {
                _gameServices.CameraController.ResetCamera();
            }
        }
        

        public void VisibleUI(bool isVisible)
        {
            IsVisible = isVisible;
            _outline.enabled = isVisible;
            _textOwner.transform.parent.gameObject.SetActive(isVisible);
            _textSpeed.transform.parent.gameObject.SetActive(isVisible);
        }
    }
}