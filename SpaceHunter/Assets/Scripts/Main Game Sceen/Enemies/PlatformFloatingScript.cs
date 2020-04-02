using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFloatingScript : PlatformBaseFSM
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        leftCannon.transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
        rightCannon.transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        leftCannon.transform.RotateAround(leftCannonRot.position, leftCannonRot.up, cannotRotateSpeed * Time.deltaTime);
        rightCannon.transform.RotateAround(rightCannonRot.position, rightCannonRot.up, cannotRotateSpeed * Time.deltaTime);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
