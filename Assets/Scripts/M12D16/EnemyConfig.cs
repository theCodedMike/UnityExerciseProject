using System.Collections.Generic;
using UnityEngine;

namespace M12D16
{
    [CreateAssetMenu(fileName = "EnemyConfig.asset", menuName = "Assets/Create EnemyConfig")]
    public class EnemyConfig : ScriptableObject
    {
        public List<Enemy> enemies;
    }
}
