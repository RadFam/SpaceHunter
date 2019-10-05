﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingScript : FSMGlobal<EnemyShipAI>
{
    /*
    private float timer;
    private float deltaTime = 0.1f;
    private PlasmaShot leftPlasma;
    private PlasmaShot rightPlasma;
    */

    private float obstacleRebootTime = 0.5f;
    private float continueAttackRebootTime = 0.2f;
    private float spendTime = 0.0f;
    private float spendTime_2 = 0.0f;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        /*
        timer = deltaTime;
        */

        spendTime = obstacleRebootTime;
        spendTime_2 = continueAttackRebootTime;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
    {

        m_MonoBehaviour.AttackingSpace();

        if (spendTime >= obstacleRebootTime)
        {
            spendTime = 0.0f;
            if (m_MonoBehaviour.CheckForObstacleHunt())
            {
                m_MonoBehaviour.ForgetTarget();
            }
        }

        if (spendTime_2 >= continueAttackRebootTime)
        {
            spendTime_2 = 0.0f;
            m_MonoBehaviour.ScanForFurtherAttack();
        }

        spendTime += Time.deltaTime;
        spendTime_2 += Time.deltaTime;
        /*
        if (timer >= deltaTime)
        {
            timer = 0.0f;

            if (enemyPlasmaShots.listFreeObjects.Count >= 2) // ЧТобы сразу два снаряда выпустить
            {
                leftPlasma = enemyPlasmaShots.listFreeObjects[0].GetComponent<PlasmaShot>();
                rightPlasma = enemyPlasmaShots.listFreeObjects[1].GetComponent<PlasmaShot>();
                leftPlasma.gameObject.SetActive(true);
                leftPlasma.SetCoords(leftGun.transform.position, leftGun.transform.rotation);
                rightPlasma.gameObject.SetActive(true);
                rightPlasma.SetCoords(rightGun.transform.position, rightGun.transform.rotation);
            }
        }

        timer += Time.deltaTime;
        */
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