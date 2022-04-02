using UnityEngine;

namespace Vehicle.Base
{
    public class VehicleBumper : MonoBehaviour
    {
        public VehicleBase Vehicle => _vehicle;
        
        [SerializeField] private VehicleBase _vehicle;
    

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.TryGetComponent(out VehicleBase nearestVehicle))
            {
                if (nearestVehicle.CurrentSpeed > 0f)
                {
                    nearestVehicle.SpeedStageUp();
                    _vehicle.SpeedStageDown();   
                }
                else
                {
                    _vehicle.Stop();
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent(out VehicleBase nearestVehicle))
            {
                nearestVehicle.SpeedStageDown(_vehicle.Data.SpeedChangeCompensation);
                _vehicle.SpeedStageUp(_vehicle.Data.SpeedChangeCompensation);
            }
        }
    } 
}

