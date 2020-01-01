using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingScript : FSMGlobal<EnemyShipAI>
{   
    /*
    public int currWayCoordInd;
    public int addedWayCoordInd;
    public Vector3 currWayCoord;
    public Vector3 wayVector;
    public int increment;
    public bool checkObstacles;

    private float nextWayPointDist;
    
    private RaycastHit rch;
    private ObstacleBehaviour obsObject;
    */


    private float obstacleRebootTime = 0.5f; // через какой промежуток времени мы делаем проверку, есть ли игрок, чтобы начать его преследовать
    private float chasingRebootTime = 0.05f; // через какой промежуток времени мы делаем проверку, нет ли перед нами препятствий
    private float spendTime = 0.0f;
    private float spendTime_2 = 0.0f;

    void Awake()
    {        
        /*
        currWayCoordInd = 0;
        addedWayCoordInd = -1;
        increment = 1;
        checkObstacles = true;
        */
        
        spendTime = 0.0f;
    }

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
    {
        /*
        base.OnStateEnter(animator, stateInfo, layerIndex);
        enemyRB.velocity = enemyRB.transform.forward * cruisingSpeed;
        currWayCoord = waypointsCoord[currWayCoordInd];
        checkObstacles = true;
        */
        spendTime = obstacleRebootTime;
        spendTime_2 = chasingRebootTime;
	}

	//OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
    {
        // В родительсвом классе вызываем функцию PatrollingSpace, которая определяет наше движение по точкам маршрута
        m_MonoBehaviour.PatrollingSpace();

        // Раз в obstacleRebootTime проверяем в функции CheckForWanderingObstacle родительского класса, нет ли перед нами препятствий
        if (spendTime >= obstacleRebootTime)
        {
            spendTime = 0.0f;
            m_MonoBehaviour.CheckForWanderingObstacle();
        }

        // Раз в chasingRebootTime проверяем в функции ScanForChase родительского класса, можем ли мы перейти в состояние преследования корабля игрока
        if (spendTime_2 >= chasingRebootTime)
        {
            spendTime_2 = 0.0f;
            m_MonoBehaviour.ScanForChase();
        }

        spendTime += Time.deltaTime;
        spendTime_2 += Time.deltaTime;
        /*
        nextWayPointDist = Vector3.Distance(enemyShip.transform.position, currWayCoord);
        if (nextWayPointDist <= takeNextWaypointDist)
        {
            if (addedWayCoordInd == -1) // то есть шли к добавленной точке
            {
                currWayCoordInd += increment;
            }
            else
            {
                waypointsCoord.RemoveAt(addedWayCoordInd);
                addedWayCoordInd = -1;
                if (increment == -1)
                {
                    currWayCoordInd += increment;
                }

            }

            if (currWayCoordInd == waypointsCoord.Count || currWayCoordInd == -1)
            {
                increment = increment * (-1);
                currWayCoordInd += increment;
            }

            currWayCoord = waypointsCoord[currWayCoordInd];
        }      

        wayVector = currWayCoord - enemyShip.transform.position;
        enemyShip.transform.rotation = Quaternion.Slerp(enemyShip.transform.rotation, Quaternion.LookRotation(wayVector), 1.5f * Time.deltaTime);
        enemyRB.velocity = enemyRB.transform.forward * cruisingSpeed;

        // Теперь проверка объектов впереди
        if (checkObstacles)
        {
            if (Physics.Raycast(enemyShip.transform.position, enemyShip.transform.forward, out rch, collisionSight, obstacleMask))
            {
                checkObstacles = false;

                // Выбрать точку, куда можно свернуть
                obsObject = rch.transform.GetComponent<ObstacleBehaviour>();
                Vector3 nextPoint = obsObject.GetLeavePoint(enemyShip.transform.position);

                // Помещаем новую точку в очередь
                addedWayCoordInd = currWayCoordInd;
                waypointsCoord.Insert(addedWayCoordInd, nextPoint);
                currWayCoordInd = addedWayCoordInd;
                currWayCoord = waypointsCoord[currWayCoordInd];
            }
        }
        else
        {
            spendTime += Time.deltaTime;
            if (spendTime >= obstacleRebootTime)
            {
                spendTime = 0.0f;
                checkObstacles = true;
            }
        }
        */


	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	
	}

}
