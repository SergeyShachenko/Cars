using UnityEngine;

namespace Vehicles
{
    public class VehicleBumper : MonoBehaviour
    {
        public Vehicle Vehicle => _vehicle;
        
        [SerializeField] private Vehicle _vehicle;
    

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Vehicle nearestVehicle))
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
            if (other.gameObject.TryGetComponent(out Vehicle nearestVehicle))
            {
                nearestVehicle.SpeedStageDown(_vehicle.Data.SpeedChangeCompensation);
                _vehicle.SpeedStageUp(_vehicle.Data.SpeedChangeCompensation);
            }
        }
    } 
}

