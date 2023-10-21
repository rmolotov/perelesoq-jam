using UnityEngine;
using Metro.Gameplay.Enemies;

namespace Metro.Gameplay.Player
{
    public class PlayerCollision : MonoBehaviour
    {
        [SerializeField] private PlayerMove moveComponent;

        private void OnTriggerEnter(Collider other)
        {
            EnemyMove enemy;
            if ((enemy = other.GetComponentInParent<EnemyMove>()) != null)
            {
                print(enemy.gameObject.name);
                moveComponent?.Collide();
            }
        }
    }
}