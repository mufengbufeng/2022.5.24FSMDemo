using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : FSMState
{
    private Transform playerTrans;
    public ChaseState(FSMSystem fSMSystem) : base(fSMSystem)
    {
        stateID = StateID.Chase;
        playerTrans = GameObject.Find("Player").transform;
    }

    /// <summary>
    /// ��ǰ״̬���������飬׷��
    /// </summary>
    /// <param name="npc"></param>
    public override void Act(GameObject npc)
    {
        npc.transform.LookAt(playerTrans);
        npc.transform.Translate(Vector3.forward * Time.deltaTime * 2);
    }

    public override void Reason(GameObject npc)
    {
        if (Vector3.Distance(playerTrans.position, npc.transform.position) > 6)
        {
            fSMSystem.PerformTransition(Transition.LostPlayer);
        }
    }
}
