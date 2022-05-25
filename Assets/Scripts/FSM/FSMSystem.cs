using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMSystem
{
    private Dictionary<StateID, FSMState> stateDic = new Dictionary<StateID, FSMState>();
    private StateID currentStateID;
    private FSMState currentState;

    public void Update(GameObject npc)
    {
        currentState.Act(npc);//执行当前状态因该做的事情
        currentState.Reason(npc);//
    }

    /// <summary>
    /// 添加新的状态
    /// </summary>
    /// <param name="state"></param>
    public void AddState(FSMState state)
    {
        if (state == null)
        {
            Debug.LogError("FSMState 不能为空");
        }
        if (currentState == null)
        {
            currentState = state;
            currentStateID = state.StateID;
        }
        if (stateDic.ContainsKey(state.StateID))
        {
            Debug.LogError("状态" + state.StateID + "；已经存在，无法重复添加");
            return;
        }
        stateDic.Add(state.StateID, state);
    }

    /// <summary>
    /// 删除状态
    /// </summary>
    /// <param name="stateID"></param>
    public void DeleteState(StateID stateID)
    {
        if (stateID == StateID.NullState)
        {
            Debug.LogError("无法删除空状态");
            return;
        }
        if (!stateDic.ContainsKey(stateID))
        {
            Debug.LogError("无法删除不存在的状态");
            return;
        }
        stateDic.Remove(stateID);
    }

    /// <summary>
    /// 过度条件满足时对应状态该做的事
    /// </summary>
    /// <param name="trainsition"></param>
    public void PerformTransition(Transition trainsition)
    {
        if (trainsition == Transition.NullTransition)
        {
            Debug.LogError("无法执行空的转换条件");
            return;
        }
        StateID id = currentState.GetOutputState(trainsition);//获取当前转换条件所对应的状态
        if (!stateDic.ContainsKey(id))
        {
            Debug.LogError("状态机里面不存在" + id + ",无法进行状态转换");
            return;
        }
        FSMState state = stateDic[id];
        currentState.DoAfterLeave();
        currentState = state;
        currentStateID = state.StateID;
        currentState.DoBeforEnter();

    }
}
