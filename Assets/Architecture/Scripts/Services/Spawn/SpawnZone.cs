using System.Collections.Generic;
using UnityEngine;
using Vehicle.Base;

namespace Services.Spawn
{
    public class SpawnZone : MonoBehaviour
    {
        public List<VehicleBase> VehiclesOnTheZone { get; private set; }


        private void Start() => VehiclesOnTheZone = new List<VehicleBase>();

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent(out VehicleBase vehicle))
                VehiclesOnTheZone.Remove(vehicle);
        }
    }
}

