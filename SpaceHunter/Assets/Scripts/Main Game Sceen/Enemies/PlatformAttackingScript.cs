using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformAttackingScript : PlatformBaseFSM
{
    Vector3 toPlayer;
    Vector3 toPlayerXZ;
    Vector3 toPlayerY;
    float angleRight;
    float angleUp;

    float timeToLookForPlayer = 0.2f;
    float timer;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        timer = timeToLookForPlayer;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer >= timeToLookForPlayer)
        {
            timer = 0.0f;

            toPlayer = player.transform.position - battlePlatform.transform.position;
            toPlayerXZ = new Vector3(toPlayer.x, 0.0f, toPlayer.z);
            toPlayerY = new Vector3(0.0f, toPlayer.y, 0.0f);
            //angleRight = Vector3.SignedAngle(toPlayerXZ, battlePlatform.transform.forward, Vector3.up);
            //angleUp = Vector3.SignedAngle(toPlayer, toPlayerXZ, Vector3.Cross(toPlayerXZ, toPlayer));

            leftCannon.transform.forward = toPlayerXZ.normalized;
            leftCannon.transform.up = Vector3.Cross(toPlayer, leftCannon.transform.right).normalized;
            rightCannon.transform.forward = toPlayerXZ.normalized;
            rightCannon.transform.up = Vector3.Cross(toPlayer, rightCannon.transform.right).normalized;
        }

        timer += Time.deltaTime;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
