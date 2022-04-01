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
        public VehicleUIController UI => _ui;
        public VehicleData Data => _data;
        public Vehicle BackVehicle => _backVehicle;

        [SerializeField] private VehicleData _data;
        [SerializeField] private VehicleUIController _ui;
        private GameServices _gameServices;
        private PathCreator _pathCreator;
        private Vehicle _backVehicle;
        private float _defaultSpeed, _currentSpeed, _speedOffset, _minSpeed, _speedStage;
        private float _distanceTravelled;
        private bool _isMovable;
        
        
        private void Update()
        {
            Move(canMove: _isMovable);
        }


        public void Init(PathCreator pathCreator)
        {
            _gameServices = GameServices.Instance;
            _pathCreator = pathCreator;
            
            _speedOffset = _gameServices.GameData.Settings.RandomSpeedOffset;
            _minSpeed = _gameServices.GameData.Settings.MinSpeed;
            _speedStage = _gameServices.GameData.Settings.SpeedStage;
            _defaultSpeed = Random.Range(Mathf.Clamp(_data.Speed - _speedOffset, _minSpeed, _data.Speed), _data.Speed);
            _currentSpeed = _defaultSpeed;

            var startPosition = _pathCreator.path.GetPointAtDistance(_distanceTravelled);
            transform.position = startPosition;

            _isMovable = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Bumper bumper))
                _backVehicle = bumper.Vehicle;
        }

        public void SpeedStageUp(float multiplier = 1) => 
            _currentSpeed = Mathf.Clamp(_currentSpeed + _speedStage * multiplier, _minSpeed, _data.Speed);
    
        public void SpeedStageDown(float multiplier = 1) => 
            _currentSpeed = Mathf.Clamp(_currentSpeed - _speedStage * multiplier, _minSpeed, _data.Speed);
    
        public void SetDefaultSpeed() => _currentSpeed = _defaultSpeed;

        public void StartLinkedVehicles()
        {
            SetDefaultSpeed();
            if (_backVehicle != null) _backVehicle.StartLinkedVehicles();
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