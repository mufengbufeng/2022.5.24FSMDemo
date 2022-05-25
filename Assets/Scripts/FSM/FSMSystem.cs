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
        currentState.Act(npc);//ִ�е�ǰ״̬�����������
        currentState.Reason(npc);//
    }

    /// <summary>
    /// ����µ�״̬
    /// </summary>
    /// <param name="state"></param>
    public void AddState(FSMState state)
    {
        if (state == null)
        {
            Debug.LogError("FSMState ����Ϊ��");
        }
        if (currentState == null)
        {
            currentState = state;
            currentStateID = state.StateID;
        }
        if (stateDic.ContainsKey(state.StateID))
        {
            Debug.LogError("״̬" + state.StateID + "���Ѿ����ڣ��޷��ظ����");
            return;
        }
        stateDic.Add(state.StateID, state);
    }

    /// <summary>
    /// ɾ��״̬
    /// </summary>
    /// <param name="stateID"></param>
    public void DeleteState(StateID stateID)
    {
        if (stateID == StateID.NullState)
        {
            Debug.LogError("�޷�ɾ����״̬");
            return;
        }
        if (!stateDic.ContainsKey(stateID))
        {
            Debug.LogError("�޷�ɾ�������ڵ�״̬");
            return;
        }
        stateDic.Remove(stateID);
    }

    /// <summary>
    /// ������������ʱ��Ӧ״̬��������
    /// </summary>
    /// <param name="trainsition"></param>
    public void PerformTransition(Transition trainsition)
    {
        if (trainsition == Transition.NullTransition)
        {
            Debug.LogError("�޷�ִ�пյ�ת������");
            return;
        }
        StateID id = currentState.GetOutputState(trainsition);//��ȡ��ǰת����������Ӧ��״̬
        if (!stateDic.ContainsKey(id))
        {
            Debug.LogError("״̬�����治����" + id + ",�޷�����״̬ת��");
            return;
        }
        FSMState state = stateDic[id];
        currentState.DoAfterLeave();
        currentState = state;
        currentStateID = state.StateID;
        currentState.DoBeforEnter();

    }
}
