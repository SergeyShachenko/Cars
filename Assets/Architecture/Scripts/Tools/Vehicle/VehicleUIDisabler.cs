using Services;
using UnityEngine;
using UnityEngine.EventSystems;
using Vehicle.Base;

namespace Tools.Vehicle
{
    public class VehicleUIDisabler : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private bool _autoDisable;
        private SceneData _sceneData;
        private CameraController _cameraController;


        private void Start()
        {
            _sceneData = GameServices.Instance.SceneData;
            _cameraController = GameServices.Instance.CameraController;
        } 

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            if (_autoDisable) return;


            if (_sceneData._previousSelectedVehicle != null)
                _sceneData._previousSelectedVehicle.UI.VisibleUI(false);

            _cameraController.ResetCamera();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_autoDisable == false) return;


            if (other.gameObject.TryGetComponent(out VehicleBase vehicle))
            {
                if (vehicle.UI.IsVisible == false) return;
                
                
                vehicle.UI.VisibleUI(false);
                _cameraController.ResetCamera();
            }
        }
    }
}