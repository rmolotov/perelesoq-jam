using DG.Tweening;
using UnityEngine;
using Metro.StaticData.Enemies;
using Sirenix.OdinInspector;
using FMODUnity;
using FMOD.Studio;

namespace Metro.Gameplay.Enemies
{
    public class EnemyMove : MonoBehaviour
    {
        [SerializeField] private Collider collider;

        [SerializeField] private EventReference moveEvent;

        private EnemyStaticData _staticData;
        private bool _moving = false;
        private bool _inCenter = true;


        public void Initialize(EnemyStaticData enemyStaticData)
        {
            _staticData = enemyStaticData;
        }

        [Button]
        public void Tap()
        {
            if (!_moving)
                Move();
        }

        private void Move() =>
            DOTween
                .Sequence()
                .AppendCallback(() => { _moving = true; })
                .Append(
                    transform
                        .DOLocalMoveX(CalcDir(), _staticData.Speed * 0.1f)
                        .SetEase(Ease.OutBounce))
                .AppendCallback(() =>
                {
                    _moving = false;
                    _inCenter = !_inCenter;
                    
                    EventInstance _moveInst = RuntimeManager.CreateInstance(moveEvent);
                    RuntimeManager.AttachInstanceToGameObject(_moveInst, transform);
                    _moveInst.start();
                    _moveInst.release();
                })
                .Play();

        private float CalcDir() =>
            (_inCenter ? 2.5f : 1.5f) *
            (_staticData.SpawnSide is EnemySide.Right ? 1f : -1f);
    }
}