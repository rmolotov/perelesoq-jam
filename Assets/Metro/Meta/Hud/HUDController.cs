using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Metro.Infrastructure.States;
using Metro.StaticData;

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

        // private void SetupStageProgressReactions(LevelStaticData stageStaticData, StageProgressData stageProgressData)
        // {
        //     stageProgressData.EnemySpawners
        //         .Select(sp => sp.enemiesRemainder)
        //         .Zip()
        //         .Where(r => r.All(rm => rm == 0))
        //         .Subscribe(_ =>
        //         {
        //             SetupStageWindow(stageStaticData, WinText);
        //         });
        //     
        //     stageProgressData.Hero
        //         .OnDestroyAsObservable()
        //         .Subscribe(_ =>
        //         {
        //             SetupStageWindow(stageStaticData, LoseText);
        //         });
        // }
        //
        private void SetupButtons() =>
            returnButton.onClick.AddListener(() => 
                _stateMachine.Enter<LoadMetaState>());
        //
        // private void SetupHeroUI(GameObject hero) => 
        //     heroUI.Initialize(hero.GetComponent<IHealth>(), false);
        //
        // private void SetupStageWindow(LevelStaticData stageStaticData, string text) =>
        //     stagePopup
        //         .InitAndShow(text, stageStaticData.Title)
        //         .Then(toMenu =>
        //         {
        //             if (toMenu) _stateMachine.Enter<LoadMetaState>();
        //             else _stateMachine.Enter<LoadLevelState, LevelStaticData>(stageStaticData);
        //         });
    }
}
