using Metro.StaticData.Player;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

namespace Metro.Gameplay.Player
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private EventReference walkEvent;
        private EventInstance _walkInst;
        
        private float _currentSpeed, _minSpeed, _maxSpeed, _acceleration;
        private bool _running;
        
        private const string _speedParam = "Speed";

        public void Initialize(PlayerStaticData config)
        {
            _minSpeed = config.MinSpeed;
            _maxSpeed = config.MaxSpeed;
            _currentSpeed = config.MinSpeed;
            _acceleration = config.Accelaration;
            
            _walkInst = RuntimeManager.CreateInstance(walkEvent);
            RuntimeManager.AttachInstanceToGameObject(_walkInst, transform);
            _walkInst.start();
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
            _walkInst.setParameterByName(_speedParam, _currentSpeed / _maxSpeed);
        }
    }
}