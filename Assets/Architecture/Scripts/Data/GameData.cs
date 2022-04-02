using System.Collections.Generic;
using UnityEngine;
using Vehicle.Base;

namespace Data
{
    [CreateAssetMenu(menuName = "Develop/Data/Game", fileName = "GameData", order = 0)]
    public class GameData : ScriptableObject
    {
        public GameSettings Settings => _settings;
        public IEnumerable<VehicleBase> Vehicles => _vehicles;

        [SerializeField] private GameSettings _settings;
        [SerializeField] private List<VehicleBase> _vehicles;
    }   
}