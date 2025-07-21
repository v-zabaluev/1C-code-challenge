using UnityEngine;

namespace Codebase.Infrastructure.Services.StaticData.Data
{
    public class StaticDataService : IStaticDataService
    {
        public T LoadScriptableObject<T>(string filePath) where T : ScriptableObject
        {
            var asset = Resources.Load<T>(filePath);
            return asset;
        }
    }
}