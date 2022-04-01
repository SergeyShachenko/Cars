using System.Collections.Generic;
using UnityEngine;
using Vehicles;

namespace Data
{
    [CreateAssetMenu(menuName = "Develop/Data/Game", fileName = "GameData", order = 0)]
    public class GameData : ScriptableObject
    {
        public GameSettings Settings => _settings;
        public IEnumerable<Vehicle> Vehicles => _vehicles;

        [SerializeField] private GameSettings _settings;
        [SerializeField] private List<Vehicle> _vehicles;
    }   
}
