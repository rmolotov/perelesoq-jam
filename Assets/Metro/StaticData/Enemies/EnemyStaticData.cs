using System;
using Sirenix.OdinInspector;

namespace Metro.StaticData.Enemies
{
    [Serializable]
    public record EnemyStaticData
    {
        [EnumToggleButtons]
        public EnemyType EnemyType;
        
        public int Speed;

        
        [HorizontalGroup("Spawn")]
        [EnumToggleButtons]
        public EnemySide SpawnSide;
        [HorizontalGroup("Spawn")][LabelText("at")][LabelWidth(30)]
        public int Position;
    }
}