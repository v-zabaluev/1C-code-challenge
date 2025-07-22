using UnityEngine;
using Zenject;

namespace Codebase.Gameplay.Particles
{
    using System.Collections.Generic;
    using UnityEngine;
    using Zenject;

    public class ParticleService
    {
        private readonly Dictionary<Effects, ParticleFactory> _factories;

        [Inject]
        public ParticleService(Dictionary<Effects, ParticleFactory> factories)
        {
            _factories = factories;
        }

        public void SpawnParticle(Effects effectType, Vector3 position)
        {
            if (!_factories.TryGetValue(effectType, out var factory))
            {
                Debug.LogWarning($"No particle factory registered for effect type: {effectType}");
                return;
            }

            var particle = factory.Create();
            particle.transform.position = position;
            particle.Play();
        }
    }

    
    public enum Effects
    {
        Explode,
    }
}