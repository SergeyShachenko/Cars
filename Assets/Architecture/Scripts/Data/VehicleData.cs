using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Develop/Data/Vehicle", fileName = "VehicleData", order = 1)]
    public class VehicleData : ScriptableObject
    {
        public GameObject Prefab => _prefab;
        public float Speed => _speed;
        public float SpeedChangeCompensation => _speedChangeCompensation;
        public string Owner => _owner;
        
        [SerializeField] private GameObject _prefab;
        [SerializeField] private float _speed, _speedChangeCompensation = 2f;
        [SerializeField] private string _owner;
    }   
}
