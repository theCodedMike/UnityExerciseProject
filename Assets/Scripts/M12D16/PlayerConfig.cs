using UnityEngine;

namespace M12D16
{
    [CreateAssetMenu(fileName = "PlayerConfig.asset", menuName = "Assets/Create PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        public Player player;
    }
}
