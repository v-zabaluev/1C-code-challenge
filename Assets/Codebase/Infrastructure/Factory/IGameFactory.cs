using System.Collections.Generic;
using Codebase.Infrastructure.Services;
using UnityEngine;

namespace Codebase.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateHeroAt(Transform initialPoint);
    }
}