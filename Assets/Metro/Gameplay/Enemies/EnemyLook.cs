using Metro.Infrastructure.Factories.Interfaces;
using UnityEngine;
using Zenject;

namespace Metro.Gameplay.Enemies
{
    public class EnemyLook : MonoBehaviour
    {
        private IPlayerFactory _playerFactory;

        [Inject]
        private void Construct(IPlayerFactory playerFactory)
        {
            _playerFactory = playerFactory;
        }

        private void Update()
        {
            transform.LookAt(_playerFactory.Player.transform);

            var tmp = transform.rotation.eulerAngles;
            tmp.z = 0;
            transform.rotation = Quaternion.Euler(tmp);
        }
    }
}