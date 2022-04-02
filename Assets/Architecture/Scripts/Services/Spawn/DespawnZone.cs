using System.Collections.Generic;
using System.Linq;
using Effects;
using UnityEngine;
using Vehicles;

namespace Services.Spawn
{
    public class DespawnZone : MonoBehaviour
    {
        private List<Vehicle> _vehiclesForDestroy;

        
        private void Start() => _vehiclesForDestroy = new List<Vehicle>();

        private void Update() => DestroyVehicles(canDestroy: _vehiclesForDestroy.Count > 0);

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Vehicle vehicle))
            {
                vehicle.View.DissolveEffect.Play(DissolveEffect.DissolveType.Disappear);
                _vehiclesForDestroy.Add(vehicle);
                GameServices.Instance.VehicleSpawner.SpawnRandomVehicle();
            }
        }
        
        
        private void DestroyVehicles(bool canDestroy)
        {
            if (canDestroy == false) return;


            var vehicle = _vehiclesForDestroy.First();
            var dissolveEnd = 
                vehicle.View.DissolveEffect.PlayTime > GameServices.Instance.GameData.Settings.DissolveDuration;

            if (dissolveEnd)
            {
                _vehiclesForDestroy.Remove(vehicle);
                Destroy(vehicle.gameObject);
            }
        }
    }
}

