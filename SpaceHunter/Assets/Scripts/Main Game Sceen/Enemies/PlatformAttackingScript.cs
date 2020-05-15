using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformAttackingScript : PlatformBaseFSM
{
    Vector3 toPlayer_1;
    Vector3 toPlayerXZ_1;
    Vector3 toPlayerZ_1;
    Vector3 toPlayer_2;
    Vector3 toPlayerXZ_2;
    Vector3 toPlayerZ_2;

    Vector3 toPlayerY_1;
    Vector3 toPlayerYY_1;
    Vector3 toPlayerY_2;
    Vector3 toPlayerYY_2;
    float angleRight_1;
    float angleRight_2;
    float angleUp_1;
    float angleUp_11;
    float angleUp_2;
    float angleUp_22;
    float angleUpRes_1;
    float angleUpRes_2;

    float timeToLookForPlayer = 0.2f;
    float timer;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        timer = timeToLookForPlayer;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        toPlayer_1 = player.transform.position - leftCannon.transform.position;
        toPlayerXZ_1 = new Vector3(toPlayer_1.x, 0.0f, toPlayer_1.z);
        toPlayerZ_1 = new Vector3(leftCannon.transform.forward.x, 0.0f, leftCannon.transform.forward.z);
        angleRight_1 = Vector3.SignedAngle(toPlayerZ_1, toPlayerXZ_1, Vector3.up);
        //toPlayerY_1 = new Vector3(0.0f, toPlayer_1.y, 0.0f);
        //toPlayerYY_1 = new Vector3(0.0f, leftCannon.transform.forward.y, 0.0f);
        angleUp_1 = Vector3.SignedAngle(leftCannon.transform.forward, toPlayerZ_1, leftCannon.transform.right);
        angleUp_11 = Vector3.SignedAngle(toPlayerXZ_1, toPlayer_1, Vector3.Cross(toPlayerXZ_1, toPlayer_1));
        angleUpRes_1 = angleUp_11 - angleUp_1;

        Debug.Log(angleUp_1 + " " + angleUp_11);

        toPlayer_2 = player.transform.position - rightCannon.transform.position;
        toPlayerXZ_2 = new Vector3(toPlayer_2.x, 0.0f, toPlayer_2.z);
        toPlayerZ_2 = new Vector3(rightCannon.transform.forward.x, 0.0f, rightCannon.transform.forward.z);
        angleRight_2 = Vector3.SignedAngle(toPlayerZ_2, toPlayerXZ_2, Vector3.up);
        //toPlayerY_2 = new Vector3(0.0f, toPlayer_2.y, 0.0f);
        //toPlayerYY_2 = new Vector3(0.0f, rightCannon.transform.forward.y, 0.0f);
        angleUp_2 = Vector3.SignedAngle(rightCannon.transform.forward, toPlayerZ_2, rightCannon.transform.right);
        angleUp_22 = Vector3.SignedAngle(toPlayerXZ_2, toPlayer_2, Vector3.Cross(toPlayerXZ_2, toPlayer_2));
        angleUpRes_2 = angleUp_22 - angleUp_2;

        //Debug.Log(angleUpRes_1 + " " + angleUpRes_2);


        leftCannon.transform.RotateAround(leftCannonRot.position, leftCannonRot.up, angleRight_1);
        leftCannon.transform.RotateAround(leftCannonRot.position, leftCannon.transform.right, -1.0f * angleUpRes_1);
        rightCannon.transform.RotateAround(rightCannonRot.position, rightCannonRot.up, angleRight_2);
        rightCannon.transform.RotateAround(rightCannonRot.position, rightCannon.transform.right, -1.0f * angleUpRes_2);

        //Vector3 locaAngles = new Vector3(leftCannonRot.transform.eulerAngles.x, leftCannonRot.transform.eulerAngles.y + angleRight_1 + 180.0f, leftCannonRot.transform.eulerAngles.z);
        //leftCannonRot.transform.eulerAngles = locaAngles;
        //locaAngles = new Vector3(rightCannonRot.transform.eulerAngles.x, rightCannonRot.transform.eulerAngles.y + angleRight_1 + 180.0f, rightCannonRot.transform.eulerAngles.z);
        //rightCannonRot.transform.eulerAngles = locaAngles;

        /*
        if (timer >= timeToLookForPlayer)
        {
            timer = 0.0f;

            toPlayer = player.transform.position - battlePlatform.transform.position;
            toPlayerXZ = new Vector3(toPlayer.x, 0.0f, toPlayer.z);
            toPlayerY = new Vector3(0.0f, toPlayer.y, 0.0f);
            angleRight = Vector3.SignedAngle(toPlayerXZ, battlePlatform.transform.right, Vector3.up);
            //angleUp = Vector3.SignedAngle(toPlayer, toPlayerXZ, Vector3.Cross(toPlayerXZ, toPlayer));

            leftCannon.transform.RotateAround(leftCannonRot.position, leftCannonRot.up, angleRight);
            rightCannon.transform.RotateAround(rightCannonRot.position, rightCannonRot.up, angleRight);

            //leftCannon.transform.forward = toPlayerXZ.normalized;
            leftCannon.transform.localPosition = leftCannonPos;
            //leftCannon.transform.up = Vector3.Cross(toPlayer, leftCannon.transform.right).normalized;
            //rightCannon.transform.forward = toPlayerXZ.normalized;
            rightCannon.transform.localPosition = rightCannonPos;
            //rightCannon.transform.up = Vector3.Cross(toPlayer, rightCannon.transform.right).normalized;

            //leftCannon.transform.LookAt(player.transform);
            //rightCannon.transform.LookAt(player.transform);
        }
        */

        timer += Time.deltaTime;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
