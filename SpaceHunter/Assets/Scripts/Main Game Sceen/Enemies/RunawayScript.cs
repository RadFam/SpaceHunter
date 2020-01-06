using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunawayScript : FSMGlobal<EnemyShipAI>
{
    private float obstacleRebootTime = 0.5f; // через какой промежуток времени мы делаем проверку, нет ли перед нами препятствий
    private float runawayRebootTime = 0.05f; // через какой промежуток времени мы делаем проверку, можно ли оставаться в состоянии убегания
    private float spendTime = 0.0f;
    private float spendTime_2 = 0.0f;

    void Awake()
    {
        spendTime = 0.0f;
        spendTime_2 = 0.0f;
    }

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        spendTime = obstacleRebootTime;
        spendTime_2 = runawayRebootTime;
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (spendTime >= obstacleRebootTime)
        {
            spendTime = 0.0f;
        }

        if (spendTime_2 >= runawayRebootTime)
        {
            spendTime_2 = 0.0f;
        }

        spendTime += Time.deltaTime;
        spendTime_2 += Time.deltaTime;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
