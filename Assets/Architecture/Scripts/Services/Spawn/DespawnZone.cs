using UnityEngine;
using Vehicles;

namespace Services.Spawn
{
    public class DespawnZone : MonoBehaviour
    {
        private GameServices _gameServices;


        private void Start() => _gameServices = GameServices.Instance;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Vehicle vehicle))
            {
                _gameServices.VehicleSpawner.SpawnRandomVehicle();
                Destroy(vehicle.gameObject);
            }
        }
    }
}

