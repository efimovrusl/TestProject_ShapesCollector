using Managers;
using UnityEngine;
using Zenject;

namespace Installers
{
public class LevelInstaller : MonoInstaller
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private MenuManager menuManager;
    [SerializeField] private LevelGrid levelGrid;

    public override void InstallBindings()
    {
        Container.Bind<LevelManager>().FromInstance( levelManager ).AsSingle();
        Container.Bind<MenuManager>().FromInstance( menuManager ).AsSingle();
        Container.Bind<LevelGrid>().FromInstance( levelGrid ).AsSingle();
    }
}
}