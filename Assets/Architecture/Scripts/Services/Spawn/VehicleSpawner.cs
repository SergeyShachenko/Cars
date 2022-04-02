using System.Collections.Generic;
using System.Linq;
using Data;
using PathCreation;
using Tools;
using UnityEngine;
using Vehicle.Base;

namespace Services.Spawn
{
    public class VehicleSpawner : MonoBehaviour
    {
        [SerializeField] private PathCreator _pathCreator;
        [SerializeField] private SpawnZone _spawnZone;
        private GameData _gameData;
        private List<VehicleBase> _vehicleData, _vehiclesForSpawn;


        private void Start()
        {
            _gameData = GameServices.Instance.GameData;
            _vehicleData = new List<VehicleBase>(_gameData.Vehicles);
            _vehiclesForSpawn = new List<VehicleBase>();

            CollectVehiclesForSpawn(count: _gameData.Settings.CountVehiclesForSpawn);
        }
        
        private void Update() =>
            SpawnVehicle(canSpawn: _vehiclesForSpawn.Count > 0 && _spawnZone.VehiclesOnTheZone.Count == 0);


        public void SpawnVehicle(VehicleBase vehicle) => _vehiclesForSpawn.Add(vehicle);

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
            
            var spawnedVehicle = Instantiate(vehicle.Data.Prefab, transform).GetComponent<VehicleBase>();
            spawnedVehicle.Init(_pathCreator);
            spawnedVehicle.Go();
            spawnedVehicle.View.DissolveEffect.Play(DissolveEffect.Type.Appear);

            _spawnZone.VehiclesOnTheZone.Add(spawnedVehicle);    
            _vehiclesForSpawn.Remove(vehicle);
        }
    }
}

