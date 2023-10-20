using System.Threading.Tasks;
using Metro.Gameplay.Train;

namespace Metro.Infrastructure.Factories.Interfaces
{
    public interface ILevelFactory
    {
        Task WarmUp();
        void CleanUp();
        
        Task<TrainController> Create(int length);
    }
}