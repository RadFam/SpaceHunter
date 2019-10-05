using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class FSMGlobal<TMonoBehaviour> : SealedSMB where TMonoBehaviour : MonoBehaviour
{
    protected TMonoBehaviour m_MonoBehaviour;
    
    /*
    public bool canChase;
    public float attackDistance;

    public List<Transform> waypoints;
    public List<Vector3> waypointsCoord;
    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public GameObject leftGun;
    public GameObject rightGun;
    public ListParticle enemyPlasmaShots;
    

    protected float takeNextWaypointDist = 2.0f;
    protected float collisionSight = 100.0f;
    protected float sightRange = 100.0f;
    protected float sightAngle = 60.0f;
    protected float attackRange = 50.0f;
    protected float attackAngle = 0.5f;
    protected float cruisingSpeed = 25.0f;

    protected float obstacleRebootTime = 1.0f;

    protected GameObject enemyShip;
    protected Rigidbody enemyRB;
    */
    public static void Initialise(Animator animator, TMonoBehaviour monoBehaviour)
    {   
        FSMGlobal<TMonoBehaviour>[] fsmGlobal = animator.GetBehaviours<FSMGlobal<TMonoBehaviour>>();
        for (int i = 0; i < fsmGlobal.Length; i++)
        {
            fsmGlobal[i].InternalInitialise(animator, monoBehaviour);
        }
    }

    protected void InternalInitialise(Animator animator, TMonoBehaviour monoBehaviour)
    {
        m_MonoBehaviour = monoBehaviour;
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        /*
        canChase = false;

        enemyShip = animator.gameObject;
        enemyRB = enemyShip.GetComponent<Rigidbody>();

        waypoints = enemyShip.GetComponent<EnemyShipAI>().waypoints;

        waypointsCoord = new List<Vector3>();
        foreach (Transform tr in waypoints)
        {
            waypointsCoord.Add(tr.position);
        }
        */
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}

public abstract class SealedSMB : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }
}
