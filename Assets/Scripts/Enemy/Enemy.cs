using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private FSMSystem fsm;
    private void Start()
    {
        InitFSM();
    }

    private void InitFSM()
    {
        fsm = new FSMSystem();
        FSMState patrolState = new PatrolState(fsm);
        patrolState.AddTransition(Transition.SeePlayer, StateID.Chase);

        FSMState chaseState = new ChaseState(fsm);
        chaseState.AddTransition(Transition.LostPlayer, StateID.Patrol);

        fsm.AddState(patrolState);
        fsm.AddState(chaseState);
    }

    private void Update()
    {
        fsm.Update(gameObject);
    }
}
