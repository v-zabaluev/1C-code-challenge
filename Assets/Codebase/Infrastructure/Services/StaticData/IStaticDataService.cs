using UnityEngine;

namespace Codebase.Infrastructure.Services.StaticData.Data
{
    public interface IStaticDataService
    {
        T LoadScriptableObject<T>(string filePath) where T : ScriptableObject;
    }
}