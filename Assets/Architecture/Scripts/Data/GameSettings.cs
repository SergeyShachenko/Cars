using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Develop/Settings/Game", fileName = "GameSettings", order = 0)]
    public class GameSettings : ScriptableObject
    {
        public float AimFOV => _aimFOV;
        public uint CountVehiclesForSpawn => _countVehiclesForSpawn;
        public float DissolveDuration => _dissolveDuration;
        public float RandomSpeedOffset => _randomSpeedOffset;
        public float MinSpeed => _minSpeed;
        public float SpeedStage => _speedStage;
        public float WheelSpeed => _wheelSpeed;

        [Header("Camera")] 
        [SerializeField] private float _aimFOV = 35f;
        
        [Header("Spawn")]
        [SerializeField] private uint _countVehiclesForSpawn = 40;
        [SerializeField] private float _dissolveDuration = 0.5f;
        
        [Header("Vehicles")] 
        [SerializeField] private float _randomSpeedOffset = 6f;
        [SerializeField] private float _minSpeed = 4f;
        [SerializeField] private float _speedStage = 0.5f;
        [SerializeField] private float _wheelSpeed = 3f;
    }
}