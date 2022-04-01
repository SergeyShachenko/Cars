using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Develop/Settings/Game", fileName = "GameSettings", order = 0)]
    public class GameSettings : ScriptableObject
    {
        public float AimFOV => _aimFOV;
        public uint CountVehiclesForSpawn => _countVehiclesForSpawn;
        public float RandomSpeedOffset => _randomSpeedOffset;
        public float MinSpeed => _minSpeed;
        public float SpeedStage => _speedStage;

        [Header("Camera")] 
        [SerializeField] private float _aimFOV = 35f;
        
        [Header("Spawn")]
        [SerializeField] private uint _countVehiclesForSpawn = 40;
        
        [Header("Vehicles")] 
        [SerializeField] private float _randomSpeedOffset = 6f;
        [SerializeField] private float _minSpeed = 4f;
        [SerializeField] private float _speedStage = 0.5f;
    }
}