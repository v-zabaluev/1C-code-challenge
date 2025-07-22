using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Codebase.Gameplay.Particles.Installer
{
    public class ParticleServiceInstaller : MonoInstaller
    {
        [SerializeField] private ParticleEffect _explodePrefab;

        public override void InstallBindings()
        {
            Container.BindFactory<ParticleEffect, ParticleFactory>()
                .WithId(Effects.Explode)
                .FromComponentInNewPrefab(_explodePrefab)
                .UnderTransformGroup("Particles");

            Container.Bind<Dictionary<Effects, ParticleFactory>>()
                .FromMethod(context =>
                {
                    var container = context.Container;

                    return new Dictionary<Effects, ParticleFactory>
                    {
                        { Effects.Explode, container.ResolveId<ParticleFactory>(Effects.Explode) },
                    };
                })
                .AsSingle();

            Container.Bind<ParticleService>().AsSingle();
        }
    }
}