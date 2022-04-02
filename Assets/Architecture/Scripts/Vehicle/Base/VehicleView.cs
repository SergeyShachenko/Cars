using Tools;
using UnityEngine;

namespace Vehicle.Base
{
    public class VehicleView : MonoBehaviour
    {
        public DissolveEffect DissolveEffect => _dissolveEffect;
        
        [SerializeField] private DissolveEffect _dissolveEffect;
    }
}