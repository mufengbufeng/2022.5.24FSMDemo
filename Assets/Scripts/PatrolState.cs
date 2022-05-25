using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ñ²Âß×´Ì¬
/// </summary>
public class PatrolState : FSMState
{
    private List<Transform> pathList = new List<Transform>();
    private int index   = 0;
    private Transform playerTrans;
    public PatrolState(FSMSystem fSMSystem) : base(fSMSystem)
    {
        stateID = StateID.Patrol;
        Transform pathTrans = GameObject.Find("path").transform;
        Transform[] trans = pathTrans.GetComponentsInChildren<Transform>();
        foreach (Transform t in trans)
        {
            if(t != pathTrans) pathList.Add(t);
        }
        playerTrans = GameObject.Find("Player").transform;
    }

    public override void Act(GameObject npc)
    {
        npc.transform.LookAt(pathList[index]);
        npc.transform.Translate(Vector3.forward * Time.deltaTime);
        if (Vector3.Distance(npc.transform.position,pathList[index].position) < 0.5f)
        {
            index++;
            index %= pathList.Count;
        }
    }

    public override void Reason(GameObject npc)
    {
        if (Vector3.Distance(playerTrans.position,npc.transform.position) < 3)
        {
            fSMSystem.PerformTransition(Transition.SeePlayer);
        }
    }
}
