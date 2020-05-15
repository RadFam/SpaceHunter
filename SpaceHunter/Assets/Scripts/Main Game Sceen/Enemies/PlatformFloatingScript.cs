using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFloatingScript : PlatformBaseFSM
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        //leftCannonRot.transform.localPosition = new Vector3(-5.14f, 2.97f, -0.1f);
        //leftCannonRot.transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
        //rightCannonRot.transform.localPosition = new Vector3(5.14f, 2.97f, -0.1f);
        //rightCannonRot.transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
        leftCannon.transform.localPosition = new Vector3(-4.9f, 0.1f, 1.8f);
        leftCannon.transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
        rightCannon.transform.localPosition = new Vector3(-4.9f, 0.1f, 1.8f);
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
