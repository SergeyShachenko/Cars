using Data;
using Services;
using UnityEngine;

namespace Vehicle.Base
{
    public class VehicleWheel : MonoBehaviour
    {
        private GameData _gameData;
        private float _speed;
        private bool _isRotate;


        private void Start()
        {
            _gameData = GameServices.Instance.GameData;
            _speed = _gameData.Settings.WheelSpeed;   
        }

        private void Update()
        {
            if (_isRotate) 
                transform.rotation *= Quaternion.Euler(_speed,0f, 0f);   
        }


        public void Go() => _isRotate = true;

        public void Stop() => _isRotate = false;
    }
}