using System.Threading.Tasks;
using UnityEngine;
using Metro.Meta.HUD;
using Metro.Meta.Menu;

namespace Metro.Infrastructure.Factories.Interfaces
{
    public interface IUIFactory
    {
        Task WarmUp();
        void CleanUp();
        
        Task <Canvas> CreateUIRoot();
        Task<HUDController> CreateHud();
        Task<MenuController> CreateMainMenu();
    }
}