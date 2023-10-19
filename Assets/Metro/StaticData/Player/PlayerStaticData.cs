using UnityEngine;

namespace Metro.StaticData.Player
{
    [CreateAssetMenu(menuName = "Metro/Static Data/Player config", fileName = "PlayerConfig")]
    public class PlayerStaticData : ScriptableObject
    {
        public int MaxHealth;
        public int MaxSpeed;
    }
}