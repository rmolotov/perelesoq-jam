using UnityEngine;

namespace Metro.Gameplay.Conductor
{
    public class ConductorController : MonoBehaviour
    {
        [SerializeField] private ConductorMove moveComponent;
        [SerializeField] private Animator animator;
        
        public void Run() => 
            moveComponent?.Run();

        public void Stop(bool win = false)
        {
            moveComponent?.Stop();
            
            if (win == false)
                animator.SetTrigger("kill");
        }
    }
}