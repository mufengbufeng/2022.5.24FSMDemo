using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 转换条件
/// </summary>
public enum Transition
{
    NullTransition = 0,//空转换条件
    SeePlayer,//看到玩家
    LostPlayer,//遗失玩家
}
/// <summary>
/// 当前状态
/// </summary>
public enum StateID
{
    NullState,//空状态
    Patrol,//巡逻状态
    Chase,//追赶状态
}

public abstract class FSMState
{
    protected StateID stateID;//当前状态
    public StateID StateID { get { return stateID; } }
    protected Dictionary<Transition, StateID> transitionStateDic = new Dictionary<Transition, StateID>();
    protected FSMSystem fSMSystem;
    public FSMState(FSMSystem fSMSystem)
    {
        this.fSMSystem = fSMSystem;
    }

    /// <summary>
    /// 添加转换条件
    /// </summary>
    /// <param name="trans"></param>
    /// <param name="id"></param>
    public void AddTransition(Transition trans, StateID id)
    {
        if (trans == Transition.NullTransition) { Debug.LogError("不允许有NullTransition"); return; }
        if (id == StateID.NullState) { Debug.LogError("不允许NullStateID"); return; }
        if (transitionStateDic.ContainsKey(trans)) { Debug.LogError("转换条件" + trans + "已经存在于transitionStateDic中"); };
        transitionStateDic[trans] = id;
    }

    /// <summary>
    /// 删除转换条件
    /// </summary>
    /// <param name="trans"></param>
    public void DeleteTransition(Transition trans)
    {
        if (trans == Transition.NullTransition) { Debug.LogError("不允许有NullTransition"); return; }
        if (!transitionStateDic.ContainsKey(trans)) { Debug.LogError("转换条件" + trans + "不存在于transitionStateDic中"); };
        transitionStateDic.Remove(trans);
    }
    
    /// <summary>
    /// 获取当前转换条件下的状态
    /// </summary>
    /// <param name="trans"></param>
    /// <returns></returns>
    public StateID GetOutputState(Transition trans)
    {
        if (transitionStateDic.ContainsKey(trans))
        {
            return transitionStateDic[trans];
        }
        return StateID.NullState;
    }
    /// <summary>
    /// 进入新状态之前做的事
    /// </summary>
    public virtual void DoBeforEnter() { }

    /// <summary>
    /// 离开当前状态做的事
    /// </summary>
    public virtual void DoAfterLeave() { }

    /// <summary>
    /// 当前状态所做的事
    /// </summary>
    /// <param name="npc"></param>
    public abstract void Act(GameObject npc);

    /// <summary>
    /// 在某一状态执行过程中，新的转换条件满足时做的事
    /// </summary>
    /// <param name="npc"></param>
    public abstract void Reason(GameObject npc);
}
