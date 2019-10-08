using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipAI : MonoBehaviour {

    public Animator anim;
    public List<Transform> waypoints;
    public List<Vector3> waypointsCoord;

    public SpaceShipMove playerObj;
    public EnemyShipBattleAI enemyBattleAI;
    public LayerMask obstacleMask;
    public LayerMask enemyMask;
    public LayerMask playerMask;

    private RaycastHit rch;

    protected float sightRange = 100.0f;
    protected float sightAngle = 150.0f;
    protected float attackRange = 45.0f;
    protected float attackAngle = 5.0f;
    protected float cruisingSpeed = 30.0f;
    protected float rotationSpeed = 1.5f;
    protected float takeNextWaypointDist = 5.0f;

    private float timerOfAnalyse = 0.2f;
    private float timer = 0.0f;

    private float distanceToPlayer = 0.0f;
    private float angleToPlayer = 0.0f;

    private Rigidbody enemyRB;
    private Vector3 wayVector;
    private Vector3 currWayPoint;
    public int currWayIndex;
    public int addedWayIndex;
    private float nextWayPointDist;
    public int increment;

    private Damagable myHealth;

    protected readonly int m_HashWandering = Animator.StringToHash("Wandering");
    protected readonly int m_HashChasing = Animator.StringToHash("Chasing");
    protected readonly int m_HashAttacking = Animator.StringToHash("Attacking");
    protected readonly int m_HashTargetLost = Animator.StringToHash("TargetLost");

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        playerObj = FindObjectOfType<SpaceShipMove>();
        enemyRB = GetComponent<Rigidbody>();

        enemyBattleAI = GetComponent<EnemyShipBattleAI>();

        myHealth = GetComponent<Damagable>();
        myHealth.deathDel = OnDeath;

        waypointsCoord = new List<Vector3>();
        foreach (Transform tr in waypoints)
        {
            waypointsCoord.Add(tr.position);
        }

        currWayIndex = 0;
        currWayPoint = waypointsCoord[currWayIndex];
        addedWayIndex = -1;
        increment = 1;

        FSMGlobal<EnemyShipAI>.Initialise(anim, this);

        wayVector = currWayPoint - gameObject.transform.position;
    }

    void FixedUpdate()
    {
        if (timer >= timerOfAnalyse)
        { 
            timer = 0.0f;
            
            // Рассчитываем параметры для состояния противника
            distanceToPlayer = Vector3.Distance(gameObject.transform.position, playerObj.ctrlObject.transform.position);
            angleToPlayer = Vector3.Angle(gameObject.transform.forward, playerObj.ctrlObject.transform.position - gameObject.transform.position);
        }

        gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.LookRotation(wayVector), rotationSpeed * Time.deltaTime);
        enemyRB.velocity = enemyRB.transform.forward * cruisingSpeed;

        timer += Time.deltaTime;
    }

    public void OnDeath()
    { 
        // Останавливаем FSM
        anim.enabled = false;

        // Высвобождаем все задействованные снаряды (если это надо)

        // Уничтожаем объект
        Destroy(gameObject);
    }

    // Добавим методы управления (блуждание, маневрирование, преследование, атака, уход от встречной атаки)
    bool CheckForObstacle(float sRange, LayerMask mask, out RaycastHit rch)
    {
        return Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out rch, sRange, mask);
    }

    public bool CheckForObstacleHunt()
    {
        return Physics.Raycast(gameObject.transform.position, playerObj.ctrlObject.transform.position - gameObject.transform.position, out rch, distanceToPlayer, obstacleMask);
    }

    public bool CheckForWanderingObstacle()
    {
        bool ans = CheckForObstacle(sightRange, obstacleMask, out rch);

        if (ans) // назначаем курс отклонения
        {
            Vector3 nextPoint = rch.transform.GetComponent<ObstacleBehaviour>().GetLeavePoint(gameObject.transform.position);

            // Помещаем новую точку в очередь
            addedWayIndex = currWayIndex;
            waypointsCoord.Insert(addedWayIndex, nextPoint);
            currWayIndex = addedWayIndex;
            currWayPoint = waypointsCoord[currWayIndex];
        }

        return ans;
    }

    public void ForgetTarget()
    {
        anim.SetTrigger(m_HashWandering);
        enemyBattleAI.Makeshoot(false);
    }

    public void PatrollingSpace()
    {
        wayVector = currWayPoint - gameObject.transform.position;

        nextWayPointDist = Vector3.Distance(gameObject.transform.position, currWayPoint);
        if (nextWayPointDist <= takeNextWaypointDist)
        {
            if (addedWayIndex == -1)
            {
                currWayIndex += increment;
            }
            else  // то есть шли к добавленной точке
            {
                waypointsCoord.RemoveAt(addedWayIndex);
                addedWayIndex = -1;
                if (increment == -1)
                {
                    currWayIndex += increment;
                }

            }

            if (currWayIndex == waypointsCoord.Count || currWayIndex == -1)
            {
                increment = increment * (-1);
                currWayIndex += increment;
            }

            currWayPoint = waypointsCoord[currWayIndex];
        }
    }

    public void ChasingSpace()
    {
        wayVector = playerObj.ctrlObject.transform.position - gameObject.transform.position;
    }

    public void AttackingSpace()
    {
        wayVector = playerObj.ctrlObject.transform.position - gameObject.transform.position;
    }

    public void ScanForChase()
    {
        if ((distanceToPlayer <= sightRange) && (angleToPlayer <= sightAngle))
        {
            // Проверяем на наличие препятствий
            if (!CheckForObstacleHunt())
            {
                anim.SetTrigger(m_HashChasing);
            }
        }
    }

    public void ScanForAttack()
    {
        if ((distanceToPlayer <= attackRange) && (angleToPlayer <= attackAngle))
        {
            // Проверяем на наличие препятствий
            if (!CheckForObstacleHunt())
            {
                anim.SetTrigger(m_HashAttacking);
                enemyBattleAI.Makeshoot(true);
            }
        }
        if ((distanceToPlayer > sightRange) || (angleToPlayer > sightAngle))
        {
            //Debug.Log("distanceToPlayer: " + distanceToPlayer.ToString() + "  angleToPlayer: " + angleToPlayer.ToString());
            ForgetTarget();
        }
    }

    public void ScanForFurtherAttack()
    {
        if ((distanceToPlayer > attackRange) && (distanceToPlayer <= sightRange) && (angleToPlayer > attackAngle) && (angleToPlayer <= sightAngle))
        {
            enemyBattleAI.Makeshoot(false);
            anim.SetTrigger(m_HashChasing);
        }

        if ((distanceToPlayer > sightRange) || (angleToPlayer > sightAngle))
        {
            ForgetTarget();
        }
    }

    public void OnMainPlayerDefeat()
    {
        ForgetTarget();
    }
}
