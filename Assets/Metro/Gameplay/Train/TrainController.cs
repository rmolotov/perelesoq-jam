using System;
using Metro.Gameplay.Conductor;
using Metro.Infrastructure.Factories.Interfaces;
using UnityEngine;
using Zenject;

namespace Metro.Gameplay.Train
{
    public class TrainController : MonoBehaviour
    {
        [SerializeField] private Transform startModule;
        [SerializeField] private Transform endModule;
        
        [SerializeField] private Animator animator;
        
        [SerializeField] private float penaltyDistance = 0.5f;
        
        private ConductorController _conductor;
        private IPlayerFactory _playerFactory;

        [Inject]
        private void Construct(IPlayerFactory playerFactory)
        {
            _playerFactory = playerFactory;
        }
        
        public void Initialize(int length, ConductorController conductor)
        {
            _conductor = conductor;
            endModule.localPosition += length * Vector3.forward;
        }

        private void Update()
        {
            if (_playerFactory.Player != null && Vector3.Distance(_conductor.transform.position, _playerFactory.Player.transform.position) <= penaltyDistance)
            {
                Stop();
            }

            if (_playerFactory.Player != null && _playerFactory.Player.transform.position.z > endModule.transform.position.z)
            {
                _conductor.Stop(true);
                animator.SetTrigger("close");
            }
        }

        public void Run()
        {
            _conductor.Run();
            _playerFactory.Player.Run();
        }

        public void Stop()
        {
            _conductor.Stop();
            _playerFactory.Player.Stop();
        }
    }
}