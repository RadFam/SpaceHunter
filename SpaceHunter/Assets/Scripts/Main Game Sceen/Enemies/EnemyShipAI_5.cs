using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipAI_5 : EnemyShipAI_Base
{
    [SerializeField]
    protected float sightRange = 100.0f;
    [SerializeField]
    protected float sightAngle = 150.0f;
    [SerializeField]
    protected float attackRange = 45.0f;
    [SerializeField]
    protected float attackAngle = 5.0f;
    [SerializeField]
    protected float cruisingSpeed = 30.0f;
    [SerializeField]
    protected float rotationSpeed = 1.5f;
    [SerializeField]
    protected float takeNextWaypointDist = 5.0f;

    [SerializeField]
    private float timerOfAnalyse = 0.2f; // Период времени (в сек) через который вражеский корабль производит анализ своих действий
    [SerializeField]
    private float timer = 0.2f;

    protected readonly int m_HashWandering = Animator.StringToHash("Wandering");
    protected readonly int m_HashChasing = Animator.StringToHash("Chasing");
    protected readonly int m_HashAttacking = Animator.StringToHash("Attacking");
    protected readonly int m_HashTargetLost = Animator.StringToHash("TargetLost");

    // Start is called before the first frame update
    void Start()
    {
        base.StartBegin();

        FSMGlobal<EnemyShipAI_Base>.Initialise(anim, this);
        wayVector = currWayPoint - gameObject.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timer >= timerOfAnalyse)
        {
            timer = 0.0f;

            // Рассчитываем параметры для состояния противника
            distanceToPlayer = Vector3.Distance(gameObject.transform.position, playerObj.ctrlObject.transform.position);
            angleToPlayer = Vector3.Angle(gameObject.transform.forward, playerObj.ctrlObject.transform.position - gameObject.transform.position);
        }

        // Совершаем поворот к тому направлению, куда нам надо лететь. Поворот осуществляется на rotationSpeed * Time.deltaTime градусов
        gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.LookRotation(wayVector), rotationSpeed * Time.deltaTime);
        // Вычисляем вектр скорости движения
        enemyRB.velocity = enemyRB.transform.forward * cruisingSpeed;

        timer += Time.deltaTime; // Time.deltaTime - сколько времени прошло с момента последнего вызова функции FixedUpdate()
    }

    public override bool CheckForWanderingObstacle()
    {
        bool ans = CheckForObstacle(sightRange, obstacleMask, out rch);

        if (ans) // назначаем курс отклонения
        {
            // из луча rch получаем указатель на препятствие (планету) у которой через метод GetLeavePoint получаем координату точки куда нужно лететь, чтобы отклониться от столкновения
            Vector3 nextPoint = rch.transform.GetComponent<ObstacleBehaviour>().GetLeavePoint(gameObject.transform.position);

            // Помещаем новую точку в очередь, делаем ее текущей точкой маршрута, куда надо лететь
            addedWayIndex = currWayIndex;
            waypointsCoord.Insert(addedWayIndex, nextPoint);
            currWayIndex = addedWayIndex;
            currWayPoint = waypointsCoord[currWayIndex];
        }

        return ans;
    }

    public override void ForgetTarget()
    {
        // Принудительно переводим FSM вражеского корабля в состояние Wandering
        anim.SetTrigger(m_HashWandering);
        enemyBattleAI.Makeshoot(false);
    }

    public override void PatrollingSpace()
    {
        // Вычисляет вектор направления движения к очеденой точке назначения
        wayVector = currWayPoint - gameObject.transform.position;

        // Считает расстояние до точки назначения
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

            // Условие, если достигли последнюю точку из списка и начинаем двигаться в обратон напрвлении
            if (currWayIndex == waypointsCoord.Count || currWayIndex == -1)
            {
                increment = increment * (-1);
                currWayIndex += increment;
            }

            currWayPoint = waypointsCoord[currWayIndex];
        }
    }

    public override void ChasingSpace()
    {
        wayVector = playerObj.ctrlObject.transform.position - gameObject.transform.position;
    }

    public override void ScanForChase()
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

    public override void ScanForAttack()
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
        if (distanceToPlayer > sightRange)
        {
            //Debug.Log("distanceToPlayer: " + distanceToPlayer.ToString() + "  angleToPlayer: " + angleToPlayer.ToString());
            ForgetTarget();
        }
    }

    public override void ScanForFurtherAttack()
    {
        if ((distanceToPlayer > attackRange) && (distanceToPlayer <= sightRange))
        {
            enemyBattleAI.Makeshoot(false);
            anim.SetTrigger(m_HashChasing);
        }

        if (distanceToPlayer > sightRange)
        {
            ForgetTarget();
        }
    }
}
