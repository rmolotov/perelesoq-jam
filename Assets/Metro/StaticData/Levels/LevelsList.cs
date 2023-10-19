using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Metro.StaticData
{
    [CreateAssetMenu(menuName = "Metro/Static Data/Levels list", fileName = "LevelsList")]

    public class LevelsList : ScriptableObject
    {
        [ListDrawerSettings(Expanded = true)]
        public List<LevelStaticData> levels;
    }
}