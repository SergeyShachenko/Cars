using Services;
using UnityEngine;
using UnityEngine.EventSystems;
using Vehicles;

namespace UI
{
    public class HiderVehicleUI : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private bool _autoHide;
        private GameServices _gameServices;
        

        private void Start() => _gameServices = GameServices.Instance;

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            if (_autoHide) return;


            if (_gameServices.SceneData.PreviousSelectedVehicle != null)
                _gameServices.SceneData.PreviousSelectedVehicle.UI.VisibleUI(false);

            _gameServices.SceneData.CameraController.ResetCamera();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_autoHide == false) return;


            if (other.gameObject.TryGetComponent(out Vehicle vehicle))
            {
                if (vehicle.UI.IsVisible == false) return;
                
                
                vehicle.UI.VisibleUI(false);
                _gameServices.SceneData.CameraController.ResetCamera();
            }
        }
    }
}