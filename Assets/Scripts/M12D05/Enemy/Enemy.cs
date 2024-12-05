using Assets.Scripts.M12D05.FSM_AI;
using UnityEngine;

namespace Assets.Scripts.M12D05.Enemy
{
    /// <summary>
    /// 怪物类
    /// </summary>
    public class Enemy : MonoBehaviour
    {

        private FSMSystem fsmsystem; //在Enemy类中，实例化FSMSystem对象，添加巡逻和追逐状态，还有之间的转换条件
        void Start()
        {
            InitFsm();//创建状态机
        }

        void Update()
        {
            fsmsystem.UpdateState(this.gameObject);//检查更新状态
        }
        /// <summary>
        /// 创建状态机
        /// 怪物有两种状态分别是巡逻和追逐玩家
        /// 如果怪物初始状态（设置为Parol状态）一旦SeePlayer 切换状态被激活后，就切换到Chase状态
        /// 如果他在Chase状态一旦LosePlayer状态被激活了，它就转变到Parol状态
        /// </summary>
        void InitFsm()
        {
            fsmsystem = new FSMSystem();
            FSMState patrolState = new PatrolState(fsmsystem);
            FSMState attackState = new AttackState(fsmsystem);
            FSMState chaseState = new ChaseState(fsmsystem);
            //当巡逻状态看到玩家时改变为追赶玩家
            patrolState.AddTransition(Transition.ChasePlayer, StateID.Chase);

            //当在追赶状态看不到玩家时改变为巡逻状态
            chaseState.AddTransition(Transition.LosePlayer, StateID.Parol);

            //当在追赶玩家时可以攻击敌人时，停下来攻击敌人
            chaseState.AddTransition(Transition.AttackPlayer, StateID.Attack);

            //当玩家在攻击时，玩家离开，追赶玩家
            attackState.AddTransition(Transition.ChasePlayer, StateID.Chase);
            fsmsystem.AddState(patrolState);//初始状态
            fsmsystem.AddState(chaseState);
            fsmsystem.AddState(attackState);
        }
    }
}
