using UnityEngine;
using Vehicle.Base;

namespace Services
{
    public class SceneData : MonoBehaviour
    {
        [HideInInspector] public VehicleBase _previousSelectedVehicle;
    }
}