using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ת������
/// </summary>
public enum Transition
{
    NullTransition = 0,//��ת������
    SeePlayer,//�������
    LostPlayer,//��ʧ���
}
/// <summary>
/// ��ǰ״̬
/// </summary>
public enum StateID
{
    NullState,//��״̬
    Patrol,//Ѳ��״̬
    Chase,//׷��״̬
}

public abstract class FSMState
{
    protected StateID stateID;//��ǰ״̬
    public StateID StateID { get { return stateID; } }
    protected Dictionary<Transition, StateID> transitionStateDic = new Dictionary<Transition, StateID>();
    protected FSMSystem fSMSystem;
    public FSMState(FSMSystem fSMSystem)
    {
        this.fSMSystem = fSMSystem;
    }

    /// <summary>
    /// ���ת������
    /// </summary>
    /// <param name="trans"></param>
    /// <param name="id"></param>
    public void AddTransition(Transition trans, StateID id)
    {
        if (trans == Transition.NullTransition) { Debug.LogError("��������NullTransition"); return; }
        if (id == StateID.NullState) { Debug.LogError("������NullStateID"); return; }
        if (transitionStateDic.ContainsKey(trans)) { Debug.LogError("ת������" + trans + "�Ѿ�������transitionStateDic��"); };
        transitionStateDic[trans] = id;
    }

    /// <summary>
    /// ɾ��ת������
    /// </summary>
    /// <param name="trans"></param>
    public void DeleteTransition(Transition trans)
    {
        if (trans == Transition.NullTransition) { Debug.LogError("��������NullTransition"); return; }
        if (!transitionStateDic.ContainsKey(trans)) { Debug.LogError("ת������" + trans + "��������transitionStateDic��"); };
        transitionStateDic.Remove(trans);
    }
    
    /// <summary>
    /// ��ȡ��ǰת�������µ�״̬
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
    /// ������״̬֮ǰ������
    /// </summary>
    public virtual void DoBeforEnter() { }

    /// <summary>
    /// �뿪��ǰ״̬������
    /// </summary>
    public virtual void DoAfterLeave() { }

    /// <summary>
    /// ��ǰ״̬��������
    /// </summary>
    /// <param name="npc"></param>
    public abstract void Act(GameObject npc);

    /// <summary>
    /// ��ĳһ״ִ̬�й����У��µ�ת����������ʱ������
    /// </summary>
    /// <param name="npc"></param>
    public abstract void Reason(GameObject npc);
}
