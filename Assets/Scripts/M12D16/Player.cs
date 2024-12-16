using System;

namespace M12D16
{
    [Serializable]
    public class Player
    {
        public string name;
        public DateTime birthDate; // 不支持
        public int hp;
        public int attack;
        public bool isMale;
        public float height;
        public float weight;
    }
}
