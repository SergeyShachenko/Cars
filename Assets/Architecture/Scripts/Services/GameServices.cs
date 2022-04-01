using Data;
using Services.Spawn;
using UnityEngine;

namespace Services
{
    public class GameServices : MonoBehaviour
    {
        public static GameServices Instance { get; private set; }
        public VehicleSpawner VehicleSpawner => _vehicleSpawner;
        public GameData GameData => _gameData;
        public SceneData SceneData => _sceneData;

        [Header("Services")] 
        [SerializeField] private VehicleSpawner _vehicleSpawner;
        
        [Header("Data")]
        [SerializeField] private GameData _gameData;
        [SerializeField] private SceneData _sceneData;

        private void Awake() => Instance = this;
    }
}