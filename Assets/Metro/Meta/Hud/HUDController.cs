using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Metro.Infrastructure.States;
using Metro.StaticData;
using Metro.StaticData.Levels;

namespace Metro.Meta.HUD
{
    public class HUDController : MonoBehaviour
    {
        private const string LoseText = "You've lost and should start stage again.";
        private const string WinText =  "You've won and got some bucks.";
        
        // [SerializeField] private ActorUI heroUI;
        [SerializeField] private Button returnButton;
        // [SerializeField] private TwoButtonWindow stagePopup;
        
        private GameStateMachine _stateMachine;

        [Inject]
        private void Construct(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Initialize(LevelStaticData levelStaticData)
        {
            SetupButtons();
            // SetupHeroUI(stageProgressData.Hero);
            // SetupStageProgressReactions(stageStaticData, stageProgressData);
        }

       
        private void SetupButtons() =>
            returnButton.onClick.AddListener(() => 
                _stateMachine.Enter<LoadMetaState>());
    }
}
