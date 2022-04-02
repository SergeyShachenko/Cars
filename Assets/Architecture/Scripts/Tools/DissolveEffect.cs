using Data;
using Services;
using UnityEngine;

namespace Tools
{
    public class DissolveEffect : MonoBehaviour
    {
        public float PlayTime { get; private set; }

        [SerializeField] [Range(0f, 1f)] private float _startDissolve = 1f;
        [SerializeField] private Renderer[] _renderers;
        private GameData _gameData;
        private Type _type;
        private float _duration;
        

        private void Update()
        {
            if (PlayTime < _duration)
            {
                foreach (var render in _renderers)
                {
                    EditRendererProperty(render, "_Dissolve", CalcDissolveValue(
                        _type, PlayTime / _duration));
                }
                
                PlayTime += Time.deltaTime;
            }
        }
        
        
        public void Play(Type type)
        {
            _gameData = GameServices.Instance.GameData;
            _type = type;
            _duration = _gameData.Settings.DissolveDuration;

            foreach (var render in _renderers)
                EditRendererProperty(render, "_Dissolve", _startDissolve);
            
            PlayTime = 0;
        }

        private void EditRendererProperty(Renderer render, string propertyName, float newValue)
        {
            var propertyBlock = new MaterialPropertyBlock();
            render.GetPropertyBlock(propertyBlock);
            propertyBlock.SetFloat(propertyName, newValue);
            render.SetPropertyBlock(propertyBlock);
        }
        
        private float CalcDissolveValue(Type type, float normalizedTime)
        {
            return type switch
            {
                Type.Appear => 1f - normalizedTime,
                Type.Disappear => normalizedTime,
                _ => 0f
            };
        }
        
        
        public enum Type
        {
            Appear,
            Disappear
        }
    }
}