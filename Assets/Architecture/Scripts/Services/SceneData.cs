using UI;
using UnityEngine;
using Vehicles;

namespace Services
{
    public class SceneData : MonoBehaviour
    {
        public CameraController CameraController => _cameraController;
        public Vehicle PreviousSelectedVehicle 
        {
            get => _previousSelectedVehicle;
            set { if (value != null) _previousSelectedVehicle = value; }
        }

        [SerializeField] private CameraController _cameraController;
        private Vehicle _previousSelectedVehicle;
    }
}