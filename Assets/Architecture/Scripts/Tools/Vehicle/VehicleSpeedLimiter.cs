using UnityEngine;
using Vehicle.Base;

namespace Tools.Vehicle
{
    public class VehicleSpeedLimiter : MonoBehaviour
    {
        public bool IsEnable => _isEnable;
    
        [SerializeField] private bool _isEnable;
        [SerializeField] private float _speed;
        private VehicleBase _previousVehicle;

    
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out VehicleBase vehicle) && _isEnable)
            {
                if (_speed > 0f)
                {
                    vehicle.CurrentSpeed = _speed;
                }
                else
                {
                    vehicle.Stop(); 
                }
            
                _previousVehicle = vehicle;
            }
        }

    
        public void OpenStream(bool isOpen)
        {
            if (isOpen != _isEnable) return;
        
        
            _isEnable = !isOpen;
        
            if (_previousVehicle != null)
                _previousVehicle.GoLinkedVehicles();
        }
    }
}