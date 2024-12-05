/*
 * 创建状态基类FSMstate 
 * 此类负责处理一个状态的周期，状态的进入前，状态中，离开状态等。以及状态切换条件的增删
 * 
 * */

using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.M12D05.FSM_AI
{
    public abstract class FSMState 
    {
        protected StateID stateID; //状态对应的ID
        public StateID ID {
            get { return stateID; }
        }

        protected Dictionary<Transition, StateID> Transition_StateIDDic = new Dictionary<Transition, StateID>();//存储转换条件和状态的ID

        public FSMSystem fsmSystem; //管理状态对象（因为要状态更新需要通过FSMsystem去管理实现的，所以需要一个管理对象）
        protected FSMState(FSMSystem fsm)
        {
            this.fsmSystem = fsm;
        }
        /// <summary>
        /// 添加转换条件
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="id"></param>
        public void AddTransition(Transition trans, StateID id)
        {
            if (trans == Transition.NoneTransition)
            {
                Debug.Log("添加的转换条件不能为null");
                return;
            }

            if (id == StateID.NoneStateID)
            {
                Debug.Log("添加的状态ID不能为null");
                return;
            }

            if (Transition_StateIDDic.ContainsKey(trans))
            {
                Debug.Log("添加转换条件的时候，" + trans + "已经存在于Transition_StateIDDic中");
                return;
            }
            Transition_StateIDDic.Add(trans, id);
        }

        /// <summary>
        /// 删除转换条件
        /// </summary>
        /// <param name="trans"></param>
        public void DeleteTransition(Transition trans)
        {
            if (trans == Transition.NoneTransition)
            {
                Debug.Log("删除的转换条件不能为null");
                return;
            }
            if (!Transition_StateIDDic.ContainsKey(trans))
            {
                Debug.Log("删除转换条件的时候，" + trans + "不存在于Transition_StateIDDic中");
                return;
            }
            Transition_StateIDDic.Remove(trans);
        }

        /// <summary>
        /// 根据转换条件获得状态ID
        /// </summary>
        /// <param name="trans"></param>
        /// <returns></returns>
        public StateID GetStateID(Transition trans)
        {
            if (Transition_StateIDDic.ContainsKey(trans))
            {
                return Transition_StateIDDic[trans];
            }
            return StateID.NoneStateID;
        }


        /// <summary>
        ///转换到此状态前要执行的逻辑
        /// </summary>
        public virtual void DoBeforeEnterAcion() { }
        /// <summary>
        /// 离开此状态前要执行的逻辑
        /// </summary>
        public virtual void DoAfterLevAction() { }
        /// <summary>
        /// 处在本状态时要执行的逻辑
        /// </summary>
        /// <param name="TargetObj"></param>
        public abstract void CurrStateAction(GameObject TargetObj);
        /// <summary>
        /// 切换到下一状态需要的条件           
        /// </summary>
        /// <param name="TargetObj"></param>
        public abstract void NextStateAction(GameObject TargetObj);
    }
}
