using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingScript : FSMGlobal<EnemyShipAI_Base>
{

    private float obstacleRebootTime = 0.5f; // время проверки, нет ли препятсвий на пути преследования
    private float attackRebootTime = 0.5f; // время проверки, нельзя ли перейти в состояние атаки
    private float spendTime = 0.0f;
    private float spendTime_2 = 0.0f;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
    {
        spendTime = obstacleRebootTime;
        spendTime_2 = 0.0f;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
    {
        // Из базового класса вызывается функция, определяющая основное поведение в состоянии преследования
        m_MonoBehaviour.ChasingSpace();

        // Проверка раз в obstacleRebootTime секнуд, нет ли препятствий
        if (spendTime >= obstacleRebootTime)
        {
            spendTime = 0.0f;
            if (m_MonoBehaviour.CheckForObstacleHunt()) // Если препятствие есть - забываем цель
            {
                m_MonoBehaviour.ForgetTarget();
            }
        }

        // Проверка раз в attackRebootTime секунд, не перейти ли в состояние атаки
        if (spendTime_2 >= attackRebootTime)
        {
            spendTime_2 = 0.0f;
            m_MonoBehaviour.ScanForAttack();
        }

        spendTime += Time.deltaTime;
        spendTime_2 += Time.deltaTime;
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	
	}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
