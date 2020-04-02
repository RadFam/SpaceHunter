using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBaseFSM : StateMachineBehaviour
{
    public GameObject battlePlatform;
    public GameObject player;
    public float cannotRotateSpeed = 5.0f;

    protected GameObject leftCannon;
    protected GameObject rightCannon;
    protected Transform leftCannonRot;
    protected Transform rightCannonRot;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        battlePlatform = animator.gameObject;
        player = battlePlatform.GetComponent<PlatformAI>().GetPlayer();
        leftCannon = battlePlatform.GetComponent<PlatformAI>().GetLeftCannon();
        rightCannon = battlePlatform.GetComponent<PlatformAI>().GetRightCannon();
        leftCannonRot = battlePlatform.GetComponent<PlatformAI>().GetLeftCannonRot();
        rightCannonRot = battlePlatform.GetComponent<PlatformAI>().GetRightCannonRot();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
