using Metro.StaticData.Player;
using UnityEngine;

namespace Metro.Gameplay.Player
{
    public class PlayerMove : MonoBehaviour
    {
        private float _currentSpeed, _minSpeed, _maxSpeed, _acceleration;
        private bool _running;


        public void Initialize(PlayerStaticData config)
        {
            _minSpeed = config.MinSpeed;
            _maxSpeed = config.MaxSpeed;
            _currentSpeed = config.MinSpeed;
            _acceleration = config.Accelaration;
        }

        private void Update()
        {
            if (_running)
                Move();
        }
        
        public void Run() => 
            _running = true;

        public void Stop() => 
            _running = false;

        public void Collide()
        {
            _currentSpeed = _minSpeed;
        }

        private void Move()
        {
            if (_currentSpeed < _maxSpeed) 
                _currentSpeed += _acceleration * Time.deltaTime;
            
            transform.Translate(Vector3.forward * _currentSpeed * Time.deltaTime, Space.World);
        }
    }
}