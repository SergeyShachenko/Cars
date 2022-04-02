using Data;
using PathCreation;
using Services;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Vehicles
{
    public class Vehicle : MonoBehaviour
    {
        public float CurrentSpeed
        {
            get => _currentSpeed;
            set => _currentSpeed = Mathf.Clamp(value, 0f, _data.Speed);
        }
        
        public VehicleView View => _view;
        public VehicleUIController UI => _ui;
        public VehicleData Data => _data;
        public Vehicle BackVehicle => _backVehicle;
        
        [SerializeField] private VehicleView _view;
        [SerializeField] private VehicleUIController _ui;
        [SerializeField] private VehicleData _data;
        private PathCreator _pathCreator;
        private Vehicle _backVehicle;
        private VehicleWheel[] _wheels;
        private float _defaultSpeed, _currentSpeed, _speedOffset, _minSpeed, _speedStage;
        private float _distanceTravelled;


        private void Update()
        {
            Move(canMove: _currentSpeed > 0f);
        }


        public void Init(PathCreator pathCreator)
        {
            _pathCreator = pathCreator;
            _wheels = GetComponentsInChildren<VehicleWheel>();
            
            _speedOffset = GameServices.Instance.GameData.Settings.RandomSpeedOffset;
            _minSpeed = GameServices.Instance.GameData.Settings.MinSpeed;
            _speedStage = GameServices.Instance.GameData.Settings.SpeedStage;
            _defaultSpeed = Random.Range(Mathf.Clamp(_data.Speed - _speedOffset, _minSpeed, _data.Speed), _data.Speed);

            var startPosition = _pathCreator.path.GetPointAtDistance(_distanceTravelled);
            transform.position = startPosition;
            
            Go();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out VehicleBumper bumper))
                _backVehicle = bumper.Vehicle;
        }

        public void SpeedStageUp(float multiplier = 1) => 
            _currentSpeed = Mathf.Clamp(_currentSpeed + _speedStage * multiplier, _minSpeed, _data.Speed);
    
        public void SpeedStageDown(float multiplier = 1) => 
            _currentSpeed = Mathf.Clamp(_currentSpeed - _speedStage * multiplier, _minSpeed, _data.Speed);
    
        public void SetDefaultSpeed() => _currentSpeed = _defaultSpeed;

        public void Go()
        {
            SetDefaultSpeed();
            
            foreach (var wheel in _wheels)
                wheel.Go();
        }

        public void Stop()
        {
            _currentSpeed = 0f;
            
            foreach (var wheel in _wheels)
                wheel.Stop();
        }
        
        public void GoLinkedVehicles()
        {
            Go();
            
            if (_backVehicle != null) 
                _backVehicle.GoLinkedVehicles();
        }

        private void Move(bool canMove)
        {
            if (canMove == false) return;
        
        
            _distanceTravelled += Time.deltaTime * _currentSpeed;

            var nextPosition = _pathCreator.path.GetPointAtDistance(_distanceTravelled, EndOfPathInstruction.Stop);
            transform.position = nextPosition;
        }
    }
}