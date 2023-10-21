using Metro.StaticData.Player;
using UnityEngine;

namespace Metro.Gameplay.Player
{
    public class PlayerMove : MonoBehaviour
    {
        private float _currentSpeed, _minSpeed, _maxSpeed, _acceleration;

        public void Initialize(PlayerStaticData config)
        {
            _minSpeed = config.MinSpeed;
            _maxSpeed = config.MaxSpeed;
            _currentSpeed = config.MinSpeed;
            _acceleration = config.Accelaration;
        }

        private void Update() => 
            Move();

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