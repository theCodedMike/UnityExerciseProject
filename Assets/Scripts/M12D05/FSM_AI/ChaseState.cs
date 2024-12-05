/*
 * 
 * 增加一个ChaseState类，
 * 用怪物的追逐
 * */

using UnityEngine;

namespace Assets.Scripts.M12D05.FSM_AI
{
    /// <summary>
    /// 追逐状态类
    /// </summary>
    public class ChaseState : FSMState
    {

        private Transform PlayerTransForm;//玩家位置信息

        public ChaseState(FSMSystem fsm) : base(fsm)
        {
            stateID = StateID.Chase;
            PlayerTransForm = GameObject.Find("Player").transform;
        }
        /// <summary>
        /// 追逐状态下执行的逻辑
        /// </summary>
        /// <param name="targrtObj"></param>
        public override void CurrStateAction(GameObject targrtObj)
        {
            targrtObj.transform.LookAt(PlayerTransForm.transform.position);
            targrtObj.transform.Translate(Vector3.forward * 2 * Time.deltaTime);
        }
        /// <summary>
        /// 切换到下一状态（巡逻）前要执行的逻辑
        /// </summary>
        /// <param name="targrtObj"></param>
        public override void NextStateAction(GameObject targrtObj)
        {
            if (Vector3.Distance(PlayerTransForm.position, targrtObj.transform.position) > 6)
            {
                fsmSystem.PerformTransition(Transition.LosePlayer);
            }
            if (Vector3.Distance(PlayerTransForm.position, targrtObj.transform.position) <= 2)
            {
                fsmSystem.PerformTransition(Transition.AttackPlayer);
            }
        }
    }
}
