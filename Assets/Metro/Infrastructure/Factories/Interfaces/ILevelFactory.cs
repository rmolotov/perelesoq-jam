using System.Threading.Tasks;
using Metro.Gameplay.Train;

namespace Metro.Infrastructure.Factories.Interfaces
{
    public interface ILevelFactory
    {
        TrainController Train { get; }
        
        Task WarmUp();
        void CleanUp();
        
        Task<TrainController> Create(int length);
    }
}