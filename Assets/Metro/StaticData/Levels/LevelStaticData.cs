using System;
using Newtonsoft.Json;
using UnityEngine;
using Metro.StaticData.Converters;

namespace Metro.StaticData
{
    [Serializable]
    public record LevelStaticData
    {
        public string Key;
        public string Title;
        [Multiline] 
        public string Description;

        [JsonConverter(typeof(Vector3Converter))]
        public Vector3 PlayerSpawnPoint;
    }
}