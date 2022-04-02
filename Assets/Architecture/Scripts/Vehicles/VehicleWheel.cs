using Services;
using UnityEngine;

namespace Vehicles
{
    public class VehicleWheel : MonoBehaviour
    {
        private float _speed;
        private bool _isRotate;


        private void Start() => _speed = GameServices.Instance.GameData.Settings.WheelSpeed;

        private void Update()
        {
            if (_isRotate) 
                transform.rotation *= Quaternion.Euler(_speed,0f, 0f);   
        }


        public void Go() => _isRotate = true;

        public void Stop() => _isRotate = false;
    }
}