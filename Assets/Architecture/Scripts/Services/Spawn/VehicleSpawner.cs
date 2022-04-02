using System.Collections.Generic;
using System.Linq;
using Effects;
using PathCreation;
using UnityEngine;
using Vehicles;

namespace Services.Spawn
{
    public class VehicleSpawner : MonoBehaviour
    {
        [SerializeField] private PathCreator _pathCreator;
        [SerializeField] private SpawnZone _spawnZone;
        private GameServices _gameServices;
        private List<Vehicle> _vehicleData, _vehiclesForSpawn;


        private void Start()
        {
            _gameServices = GameServices.Instance;
            _vehiclesForSpawn = new List<Vehicle>();
            _vehicleData = new List<Vehicle>(_gameServices.GameData.Vehicles);
            
            CollectVehiclesForSpawn(count: _gameServices.GameData.Settings.CountVehiclesForSpawn);
        }
        
        private void Update()
        {
            SpawnVehicle(canSpawn: _vehiclesForSpawn.Count > 0 && _spawnZone.VehiclesOnTheZone.Count == 0);
        }
        

        public void SpawnVehicle(Vehicle vehicle) => _vehiclesForSpawn.Add(vehicle);

        public void SpawnRandomVehicle() => _vehiclesForSpawn.Add(_vehicleData[Random.Range(0, _vehicleData.Count)]);

        private void CollectVehiclesForSpawn(uint count)
        {
            for (var i = 0; i < count; i++)
                _vehiclesForSpawn.Add(_vehicleData[Random.Range(0, _vehicleData.Count)]);
        }

        private void SpawnVehicle(bool canSpawn)
        {
            if (canSpawn == false) return;


            var vehicle = _vehiclesForSpawn.First();
            
            var spawnedVehicle = Instantiate(vehicle.Data.Prefab, transform).GetComponent<Vehicle>();
            spawnedVehicle.Init(_pathCreator);
            spawnedVehicle.View.DissolveEffect.Play(DissolveEffect.DissolveType.Appear);

            _spawnZone.VehiclesOnTheZone.Add(spawnedVehicle);    
            _vehiclesForSpawn.Remove(vehicle);
        }
    }
}

