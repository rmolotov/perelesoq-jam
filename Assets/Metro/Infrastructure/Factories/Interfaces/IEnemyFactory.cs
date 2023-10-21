using System.Threading.Tasks;
using Metro.Gameplay.Enemies;
using Metro.StaticData.Enemies;

namespace Metro.Infrastructure.Factories.Interfaces
{
    public interface IEnemyFactory
    {
        Task WarmUp();
        void CleanUp();
        Task<EnemyMove> Create(EnemyStaticData enemyStaticData);
    }
}