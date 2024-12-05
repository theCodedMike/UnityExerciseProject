using UnityEngine;

namespace Assets.Scripts.M12D05.FSM_AI
{
    public class AttackState : FSMState
    {
        private Transform PlayerTransForm;//玩家位置信息
        public AttackState(FSMSystem fsm):base(fsm)
        {
            stateID = StateID.Attack;
            PlayerTransForm = GameObject.Find("Player").transform;
        }

        public override void CurrStateAction(GameObject TargetObj)
        {
            Debug.Log("攻击玩家");
        }

        public override void NextStateAction(GameObject TargetObj)
        {
            if (Vector3.Distance(PlayerTransForm.position, TargetObj.transform.position) > 2)
            {
                fsmSystem.PerformTransition(Transition.ChasePlayer);
            }
        }


    }
}
