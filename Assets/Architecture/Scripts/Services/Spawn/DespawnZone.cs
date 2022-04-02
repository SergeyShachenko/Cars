using System.Collections.Generic;
using System.Linq;
using Data;
using Tools;
using UnityEngine;
using Vehicle.Base;

namespace Services.Spawn
{
    public class DespawnZone : MonoBehaviour
    {
        private GameData _gameData;
        private VehicleSpawner _vehicleSpawner;
        private List<VehicleBase> _vehiclesForDestroy;


        private void Start()
        {
            _gameData = GameServices.Instance.GameData;
            _vehicleSpawner = GameServices.Instance.VehicleSpawner;
            _vehiclesForDestroy = new List<VehicleBase>();   
        }

        private void Update() => DestroyVehicles(canDestroy: _vehiclesForDestroy.Count > 0);

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out VehicleBase vehicle))
            {
                vehicle.View.DissolveEffect.Play(DissolveEffect.Type.Disappear);
                _vehiclesForDestroy.Add(vehicle);
                _vehicleSpawner.SpawnRandomVehicle();
            }
        }
        
        
        private void DestroyVehicles(bool canDestroy)
        {
            if (canDestroy == false) return;


            var vehicle = _vehiclesForDestroy.First();
            var dissolveEnd = 
                vehicle.View.DissolveEffect.PlayTime > _gameData.Settings.DissolveDuration;

            if (dissolveEnd)
            {
                _vehiclesForDestroy.Remove(vehicle);
                Destroy(vehicle.gameObject);
            }
        }
    }
}

