using PathCreation;
using Vehicle.Base;

namespace Vehicle
{
    public class Car : VehicleBase
    {
        private VehicleWheel[] _wheels;

        public override void Init(PathCreator pathCreator)
        {
            _wheels = GetComponentsInChildren<VehicleWheel>();
            
            base.Init(pathCreator);
        }

        public override void Go()
        {
            foreach (var wheel in _wheels)
                wheel.Go();
            
            base.Go();
        }

        public override void Stop()
        {
            foreach (var wheel in _wheels)
                wheel.Stop();
            
            base.Stop();
        }
    }
}