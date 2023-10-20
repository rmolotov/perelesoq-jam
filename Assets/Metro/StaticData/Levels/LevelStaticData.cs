using System;
using System.Collections.Generic;
using Metro.StaticData.Converters;
using Metro.StaticData.Enemies;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Metro.StaticData.Levels
{
    [Serializable]
    public record LevelStaticData
    {
        [BoxGroup("Info")]
        public string Key;
        
        [BoxGroup("Info")]
        public string Title;
        [BoxGroup("Info")] [Multiline] 
        public string Description;

        [JsonConverter(typeof(Vector3Converter))]
        public Vector3 PlayerSpawnPoint;

        [BoxGroup("Params")]
        [Range(1, 30)] public int Length;

        public List<EnemyStaticData> Enemies;
    }
}