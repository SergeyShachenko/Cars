using Effects;
using UnityEngine;

namespace Vehicles
{
    public class VehicleView : MonoBehaviour
    {
        public DissolveEffect DissolveEffect => _dissolveEffect;
        
        [SerializeField] private DissolveEffect _dissolveEffect;
    }
}