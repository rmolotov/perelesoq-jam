using Metro.Infrastructure.Factories.Interfaces;
using UnityEngine;
using Zenject;

namespace Metro.Gameplay.Conductor
{
    public class ConductorMove : MonoBehaviour
    {
        [SerializeField] private float minSpeed;
        [SerializeField] private float maxSpeed;
        [SerializeField] private float acceleration;

        [SerializeField] private float penaltyDistance;

        [SerializeField] private Animator animator;

        private IPlayerFactory _playerFactory;
        private float _currentSpeed;
        private bool _running;

        [Inject]
        private void Construct(IPlayerFactory playerFactory)
        {
            _playerFactory = playerFactory;
        }

        private void Update()
        {
            if (_running)
            {
                Move();
                
                if (Vector3.Distance(transform.position, _playerFactory.Player.transform.position) <= penaltyDistance)
                {
                    Stop();
                    _playerFactory.Player.Stop();
                }
            }
        }

        public void Run()
        {
            _running = true;
        }

        public void Stop()
        {
            _running = false;
            animator.SetTrigger("kill");
        }

        public void Collide()
        {
            _currentSpeed = minSpeed;
        }

        private void Move()
        {
            if (_currentSpeed < maxSpeed) 
                _currentSpeed += acceleration * Time.deltaTime;
            
            transform.Translate(Vector3.forward * _currentSpeed * Time.deltaTime, Space.World);
        }
    }
}