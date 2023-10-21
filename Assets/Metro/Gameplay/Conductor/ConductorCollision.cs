using Metro.Gameplay.Enemies;
using UnityEngine;

namespace Metro.Gameplay.Conductor
{
    public class ConductorCollision : MonoBehaviour
    {
        [SerializeField] private ConductorMove moveComponent;

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