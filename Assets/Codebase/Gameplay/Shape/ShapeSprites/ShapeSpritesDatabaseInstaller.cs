using Codebase.Gameplay;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ShapeSpritesDatabaseInstaller", menuName = "Installers/ShapeSpritesDatabaseInstaller")]
public class ShapeSpritesDatabaseInstaller : ScriptableObjectInstaller<ShapeSpritesDatabaseInstaller>
{
    [SerializeField] private ShapeSpritesDatabase _database;

    public override void InstallBindings()
    {
        _database.Initialize();

        Container.Bind<ShapeSpritesDatabase>().FromInstance(_database).AsSingle();
    }
}