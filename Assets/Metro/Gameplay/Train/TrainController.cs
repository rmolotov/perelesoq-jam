using UnityEngine;

namespace Metro.Gameplay.Train
{
    public class TrainController : MonoBehaviour
    {
        [SerializeField] private Transform startModule;
        [SerializeField] private Transform endModule;

        public void Initialize(int length)
        {
            endModule.localPosition += length * Vector3.forward;
        }
    }
}