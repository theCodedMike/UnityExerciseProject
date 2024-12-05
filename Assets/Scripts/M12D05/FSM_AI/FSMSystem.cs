/*
 * 创建状态管理类FSMSystem的创建。
 * 用来管理所有的状态（状态的添加，删除，切换，更新等）。
 * 
 * */

using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.M12D05.FSM_AI
{
    public class FSMSystem
    {
        private Dictionary<StateID, FSMState> stateDic = new Dictionary<StateID, FSMState>();

        private StateID _CurrentStateID; //当前处于的状态ID

        private FSMState _CurrentState; //当前处于的状态

        /// <summary>
        /// 添加状态
        /// </summary>
        /// <param name="state">需要管理的状态</param>
        public void AddState(FSMState state)
        {
            if (state == null)
            {
                Debug.LogFormat("添加的状态{0}不能为null", state);
                return;
            }

            if (_CurrentState == null)
            {
                _CurrentState = state;
                _CurrentStateID = state.ID;
            }

            if (stateDic.ContainsKey(state.ID))
            {
                Debug.LogFormat("状态机{0}已经存在,无法添加", state.ID);
                return;
            }
            stateDic.Add(state.ID, state);
        }

        /// <summary>
        /// 删除状态
        /// </summary>
        /// <param name="stateID">删除要管理状态的ID</param>
        public void DeleteState(StateID stateID)
        {
            if (stateID == StateID.NoneStateID)
            {
                Debug.Log("无法删除null的状态");
            }
            if (!stateDic.ContainsKey(stateID))
            {
                Debug.Log("无法删除不存在的状态：" + stateID);
                return;
            }
            stateDic.Remove(stateID);
        }

        /// <summary>
        /// 状态转换（状态的切换是根据转换条件的变化）
        /// </summary>
        /// <param name="trans">转换条件</param>
        public void PerformTransition(Transition trans)
        {
		
            if (trans == Transition.NoneTransition)
            {
                Debug.Log("无法执行null的转换条件");
                return;
            }

            StateID stateId = _CurrentState.GetStateID(trans);
            if (stateId == StateID.NoneStateID)
            {
                Debug.Log("要转换的状态ID为null");
                return;
            }
            if (!stateDic.ContainsKey(stateId))
            {
                Debug.Log("状态机中没找到状态ID  " + stateId + "  无法转换状态");
                return;
            }
            FSMState state = stateDic[stateId];//根据状态ID获取要转换的状态
            _CurrentState.DoAfterLevAction();//执行离开上一状态逻辑
            _CurrentState = state;//更新当前状态
            _CurrentStateID = stateId;//更新当前状态ID
            _CurrentState.DoBeforeEnterAcion();//执行进入当前状态前要执行的逻辑
        }
        /// <summary>
        /// 更新当前状态行为
        /// </summary>
        /// <param name="targetObj"></param>
        public void UpdateState(GameObject targetObj)
        {
            _CurrentState.CurrStateAction(targetObj);
            _CurrentState.NextStateAction(targetObj);
        }
    }
}
