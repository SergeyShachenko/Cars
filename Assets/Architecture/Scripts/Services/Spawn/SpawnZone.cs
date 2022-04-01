using System.Collections.Generic;
using UnityEngine;
using Vehicles;

namespace Services.Spawn
{
    public class SpawnZone : MonoBehaviour
    {
        public List<Vehicle> VehiclesOnTheZone { get; private set; }


        private void Start() => VehiclesOnTheZone = new List<Vehicle>();

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Vehicle vehicle))
                VehiclesOnTheZone.Remove(vehicle);
        }
    }
}

