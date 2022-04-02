using Data;
using Services.Spawn;
using UI;
using UnityEngine;

namespace Services
{
    public class GameServices : MonoBehaviour
    {
        public static GameServices Instance { get; private set; }
        public CameraController CameraController => _cameraController;
        public VehicleSpawner VehicleSpawner => _vehicleSpawner;
        public GameData GameData => _gameData;
        public SceneData SceneData { get; private set; }

        [Header("Services")] 
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private VehicleSpawner _vehicleSpawner;

        [Header("Data")]
        [SerializeField] private GameData _gameData;


        private void Awake()
        {
            SceneData = gameObject.AddComponent<SceneData>();
            
            Instance = this;  
        } 
    }
}