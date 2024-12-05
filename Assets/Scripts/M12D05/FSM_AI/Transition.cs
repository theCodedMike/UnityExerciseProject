namespace Assets.Scripts.M12D05.FSM_AI
{
    /// <summary>
    /// 状态切换条件
    /// </summary>
    public enum Transition
    {
        NoneTransition,
        /// <summary>
        /// 看到玩家
        /// </summary>
        ChasePlayer,
        /// <summary>
        /// 看不到玩家
        /// </summary>
        LosePlayer,

        /// <summary>
        /// 攻击玩家
        /// </summary>
        AttackPlayer,
    }


    public enum StateID
    {
        /// <summary>
        /// 无状态ID
        /// </summary>
        NoneStateID,
        /// <summary>
        /// 巡逻状态
        /// </summary>
        Parol,
        /// <summary>
        /// 追逐状态
        /// </summary>
        Chase,

        /// <summary>
        /// 攻击状态
        /// </summary>
        Attack,
    }
}