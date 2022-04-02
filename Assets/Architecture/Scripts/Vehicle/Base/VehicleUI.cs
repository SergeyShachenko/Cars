using Services;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Vehicle.Base
{
    public class VehicleUI : MonoBehaviour, IPointerClickHandler
    {
        public bool IsVisible { get; private set; }

        [SerializeField] private VehicleBase _vehicle;
        [SerializeField] private Outline _outline;
        [SerializeField] private Transform _canvas;
        [SerializeField] private TMP_Text _textOwner, _textSpeed;
        private SceneData _sceneData;
        private CameraController _cameraController;


        private void Start()
        {
            _cameraController = GameServices.Instance.CameraController;
            _sceneData = GameServices.Instance.SceneData;
            _textOwner.text = _vehicle.Data.Owner;

            VisibleUI(IsVisible);
        }

        private void Update()
        {
            _canvas.LookAt(_canvas.position + _cameraController.transform.forward);
            _textSpeed.text = Mathf.Round(_vehicle.CurrentSpeed).ToString();
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            VisibleUI(!IsVisible);

            if (IsVisible)
            {
                var previousSelectedVehicle = _sceneData._previousSelectedVehicle;
                
                if (previousSelectedVehicle != null && previousSelectedVehicle != _vehicle)
                    _sceneData._previousSelectedVehicle.UI.VisibleUI(false);
                
                _sceneData._previousSelectedVehicle = _vehicle;
                _cameraController.LookAt(transform);
            }
            else
            {
                _cameraController.ResetCamera();
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