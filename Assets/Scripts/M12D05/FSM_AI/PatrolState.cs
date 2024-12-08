/*
 * 增加一个PartalState类，
 * 用怪物的巡逻
 * */

using System.Collections.Generic;
using UnityEngine;

namespace M12D05.FSM_AI
{
    /// <summary>
    /// 怪物巡逻状态类
    /// </summary>
    public class PatrolState : FSMState
    {
        private List<Transform> path = new List<Transform>();//巡逻点
        private int index = 0;
        private Transform PlayerTrasform;

        /// <summary>
        /// 初始化巡逻状态数据
        /// </summary>
        /// <param name="fsm"></param>
        public PatrolState(FSMSystem fsm) : base(fsm)
        {
            stateID = StateID.Parol;
            //路点
            Transform pathTransform = GameObject.Find("Path").transform;
            Transform[] children = pathTransform.GetComponentsInChildren<Transform>();
            foreach (Transform child in children)
            {
                if (child != pathTransform)
                {
                    path.Add(child);
                }
            }
            PlayerTrasform = GameObject.Find("Player").transform;
        }

        /// <summary>
        /// 当前状态（巡逻状态）执行的逻辑
        /// </summary>
        /// <param name="TargetObj"></param>
        public override void CurrStateAction(GameObject TargetObj)
        {
            TargetObj.transform.LookAt(path[index].position);
            TargetObj.transform.Translate(Vector3.forward * Time.deltaTime * 3);
            if (Vector3.Distance(TargetObj.transform.position, path[index].position) < 1)
            {
                index++;
                index %= path.Count;
            }
        }
        /// <summary>
        /// 切换到追逐状态（下一状态）执行的的逻辑
        /// </summary>
        /// <param name="TargetObj"></param>
        public override void NextStateAction(GameObject TargetObj)
        {
            if (Vector3.Distance(PlayerTrasform.position, TargetObj.transform.position) < 6)
            {
                fsmSystem.PerformTransition(Transition.ChasePlayer);
            }
        }
    }
}
