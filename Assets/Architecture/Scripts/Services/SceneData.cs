using UI;
using UnityEngine;
using Vehicles;

namespace Services
{
    public class SceneData : MonoBehaviour
    {
        public Vehicle PreviousSelectedVehicle 
        {
            get => _previousSelectedVehicle;
            set { if (value != null) _previousSelectedVehicle = value; }
        }
        
        private Vehicle _previousSelectedVehicle;
    }
}