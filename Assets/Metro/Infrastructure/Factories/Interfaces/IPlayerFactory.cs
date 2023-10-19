using System.Threading.Tasks;
using Metro.Gameplay;
using Metro.Gameplay.Player;
using UnityEngine;

namespace Metro.Infrastructure.Factories.Interfaces
{
    public interface IPlayerFactory
    {
        PlayerController Player { get; }
        Task WarmUp();
        void CleanUp();
        
        Task<PlayerController> Create(Vector3 at);
    }
}