using Data;
using PathCreation;
using Services;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Vehicle.Base
{
    public abstract class VehicleBase : MonoBehaviour
    {
        public float CurrentSpeed
        {
            get => Speed;
            set => Speed = Mathf.Clamp(value, 0f, VehicleData.Speed);
        }
        
        public VehicleView View => VehicleView;
        public VehicleUI UI => VehicleUI;
        public VehicleData Data => VehicleData;
        public VehicleBase BackVehicle { get; protected set; }
        
        [SerializeField] protected VehicleView VehicleView;
        [SerializeField] protected VehicleUI VehicleUI;
        [SerializeField] protected VehicleData VehicleData;
        protected GameData GameData;
        protected PathCreator PathCreator;
        protected float DefaultSpeed, Speed, SpeedOffset, MinSpeed, SpeedStage;
        protected float DistanceTravelled;
        

        public virtual void Init(PathCreator pathCreator)
        {
            GameData = GameServices.Instance.GameData;
            PathCreator = pathCreator;
            
            SpeedOffset = GameData.Settings.RandomSpeedOffset;
            MinSpeed = GameData.Settings.MinSpeed;
            SpeedStage = GameData.Settings.SpeedStage;
            DefaultSpeed = 
                Random.Range(Mathf.Clamp(VehicleData.Speed - SpeedOffset, MinSpeed, VehicleData.Speed), VehicleData.Speed);

            var startPosition = PathCreator.path.GetPointAtDistance(DistanceTravelled);
            transform.position = startPosition;
        } 

        protected virtual void Update() => Move(canMove: Speed > 0f);

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out VehicleBumper bumper))
                BackVehicle = bumper.Vehicle;
        }

        public virtual void SpeedStageUp(float multiplier = 1) => 
            Speed = Mathf.Clamp(Speed + SpeedStage * multiplier, MinSpeed, VehicleData.Speed);
    
        public virtual void SpeedStageDown(float multiplier = 1) => 
            Speed = Mathf.Clamp(Speed - SpeedStage * multiplier, MinSpeed, VehicleData.Speed);
    
        public virtual void SetDefaultSpeed() => Speed = DefaultSpeed;

        public virtual void Go() => SetDefaultSpeed();

        public virtual void Stop() => Speed = 0f;

        public virtual void GoLinkedVehicles()
        {
            Go();
            
            if (BackVehicle != null) 
                BackVehicle.GoLinkedVehicles();
        }

        protected virtual void Move(bool canMove)
        {
            if (canMove == false) return;
        
        
            DistanceTravelled += Time.deltaTime * Speed;

            var nextPosition = PathCreator.path.GetPointAtDistance(DistanceTravelled, EndOfPathInstruction.Stop);
            transform.position = nextPosition;
        }
    }
}