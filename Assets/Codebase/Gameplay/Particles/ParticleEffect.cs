using UnityEngine;

namespace Codebase.Gameplay.Particles
{
    public class ParticleEffect : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private float _particleLifeTime = 1f;

        public void Play()
        {
            _particleSystem.Play();
            Destroy(gameObject, _particleLifeTime);
        }
    }
}