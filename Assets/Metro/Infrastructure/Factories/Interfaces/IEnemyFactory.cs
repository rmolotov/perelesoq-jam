using System.Threading.Tasks;
using Metro.StaticData.Enemies;
using UnityEngine;

namespace Metro.Infrastructure.Factories.Interfaces
{
    public interface IEnemyFactory
    {
        Task WarmUp();
        void CleanUp();
        Task<GameObject> Create(EnemyStaticData enemyStaticData);
    }
}