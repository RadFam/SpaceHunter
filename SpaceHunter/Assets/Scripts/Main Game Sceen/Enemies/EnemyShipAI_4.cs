using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipAI_4 : EnemyShipAI_Base
{
    protected float sightRange = 100.0f;
    protected float sightAngle = 150.0f;
    protected float attackRange = 45.0f;
    protected float attackAngle = 5.0f;
    protected float cruisingSpeed = 30.0f;
    protected float rotationSpeed = 1.5f;
    protected float takeNextWaypointDist = 5.0f;

    private float timerOfAnalyse = 0.2f; // Период времени (в сек) через который вражеский корабль производит анализ своих действий
    private float timer = 0.0f;

    protected Vector3 maneuveringPoint;
    protected bool maneuveringState;

    // Хэш-коды названия состояний в которые переходит вражеский кораблик
    protected readonly int m_HashWandering = Animator.StringToHash("Wandering");
    protected readonly int m_HashChasing = Animator.StringToHash("Chasing");
    protected readonly int m_HashAttacking = Animator.StringToHash("Attacking");
    protected readonly int m_HashManeuvering = Animator.StringToHash("Maneuvering");
    protected readonly int m_HashTargetLost = Animator.StringToHash("TargetLost");

    // Start is called before the first frame update
    void Start()
    {
        base.StartBegin();

        myHealth.enemyChHlth = ShipWasAttacked;

        FSMGlobal<EnemyShipAI_4>.Initialise(anim, this);

        wayVector = currWayPoint - gameObject.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Каджые timerOfAnalyse мы рассчитываем расстояния и угол до корабля игрока
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

    // Функция, которая проверяет нет ли на пути препятсвий, когда вражеский корабль находится в состоянии Wandering
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

    // "Забываем" цель преследования - прекращаем стрелять, переходим в состояние Wandering
    public override void ForgetTarget()
    {
        // Принудительно переводим FSM вражеского корабля в состояние Wandering
        anim.SetTrigger(m_HashWandering);
        enemyBattleAI.Makeshoot(false);
    }

    // Функия вызываемая из состояния Wandering
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

    // Расчет вектора направления движения к игроку в случае нахождения в состоянии Chasing (преследования)
    public override void ChasingSpace()
    {
        wayVector = playerObj.ctrlObject.transform.position - gameObject.transform.position;
    }

    // Расчет вектора направления движения к игроку в случае нахождения в состоянии Attacking (атаки)
    public override void AttackingSpace()
    {
        wayVector = playerObj.ctrlObject.transform.position - gameObject.transform.position;
    }

    // Проверяет можем ли мы перейти в состояние Chasing
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

    // Проверяет можем ли мы перейти в состояние Attacking
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
        if ((distanceToPlayer > sightRange) || (angleToPlayer > sightAngle))
        {
            //Debug.Log("distanceToPlayer: " + distanceToPlayer.ToString() + "  angleToPlayer: " + angleToPlayer.ToString());
            ForgetTarget();
        }
    }

    // Проверяем, можем ли мы продолжать атаковать (в противном случае, мы переходим  всостояние преследования)
    public override void ScanForFurtherAttack()
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

    public override void ShipWasAttacked(float val)
    {
        // На случай, если мы находимся в состоянии wandering
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Wandering"))
        {
            CheckForChaseSpecial();
        }
    }

    public override void ShipWasNearlyAttacked()
    {
        // Проверить, есть ли в радиусе досягаемости корабль игрока и потом перейти в погоню за ним
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Wandering"))
        {
            CheckForChaseSpecial();
        }
    }

    public void CheckForChaseSpecial()
    {
        if (distanceToPlayer <= sightRange)
        {
            anim.SetTrigger(m_HashManeuvering);
        }
    }

    public  void ManeuveringState()
    {
        wayVector = currRunawayPoint - gameObject.transform.position;
        float dist = Vector3.Distance(gameObject.transform.position, currRunawayPoint);
        if (dist < takeNextWaypointDist)
        {
            if (maneuveringState)
            {
                currRunawayPoint = maneuveringPoint;
                maneuveringState = false;
            }
            ScanForWandering();
        }
    }
}
