using Metro.Gameplay.Conductor;
using UnityEngine;

namespace Metro.Gameplay.Train
{
    public class TrainController : MonoBehaviour
    {
        [SerializeField] private Transform startModule;
        [SerializeField] private Transform endModule;
        
        private ConductorMove _conductorMove;

        public void Initialize(int length, ConductorMove conductor)
        {
            _conductorMove = conductor;
            endModule.localPosition += length * Vector3.forward;
        }

        public void Run()
        {
            _conductorMove.Run();
        }
    }
}