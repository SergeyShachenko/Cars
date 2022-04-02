using Services;
using UnityEngine;

namespace Effects
{
    public class DissolveEffect : MonoBehaviour
    {
        public float PlayTime => _playTime;
        
        [SerializeField] [Range(0f, 1f)] private float _startDissolve = 1f;
        [SerializeField] private Renderer[] _renderers;
        private DissolveType _dissolveType;
        private float _playTime, _duration;
        

        private void Update()
        {
            if (_playTime < _duration)
            {
                foreach (var renderer in _renderers)
                {
                    EditRendererProperty(renderer, "_Dissolve", Dissolve(
                        _dissolveType, _playTime / _duration));
                }
                
                _playTime += Time.deltaTime;
            }
        }
        
        
        public void Play(DissolveType type)
        {
            _duration = GameServices.Instance.GameData.Settings.DissolveDuration;

            foreach (var renderer in _renderers)
                EditRendererProperty(renderer, "_Dissolve", _startDissolve);

            _dissolveType = type;
            _playTime = 0;
        }

        private void EditRendererProperty(Renderer renderer, string propertyName, float newValue)
        {
            var propertyBlock = new MaterialPropertyBlock();
            renderer.GetPropertyBlock(propertyBlock);
            propertyBlock.SetFloat(propertyName, newValue);
            renderer.SetPropertyBlock(propertyBlock);
        }
        
        private float Dissolve(DissolveType type, float normalizedTime)
        {
            switch (type)
            {
                case DissolveType.Appear: return 1f - normalizedTime;
                case DissolveType.Disappear: return normalizedTime;
                default: return 0f;
            }
        }
        
        
        public enum DissolveType
        {
            Appear,
            Disappear
        }
    }
}