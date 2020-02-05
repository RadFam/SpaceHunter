using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManeuveringScript : FSMGlobal<EnemyShipAI_4> // А не Base AI
{
    private float obstacleRebootTime = 0.5f; // через какой промежуток времени мы делаем проверку, нет ли перед нами препятствий
    private float maneuverRebootTime = 0.05f; // через какой промежуток времени мы делаем проверку, можно ли оставаться в состоянии убегания
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
        spendTime_2 = maneuverRebootTime;
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        m_MonoBehaviour.RunawayState();

        if (spendTime >= obstacleRebootTime)
        {
            spendTime = 0.0f;
            m_MonoBehaviour.CheckForManeuverObstacle();
        }

        if (spendTime_2 >= maneuverRebootTime)
        {
            spendTime_2 = 0.0f;
            m_MonoBehaviour.IsUnderAttack = false;
            m_MonoBehaviour.ScanForEndManeuvering();
        }

        spendTime += Time.deltaTime;
        spendTime_2 += Time.deltaTime;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
