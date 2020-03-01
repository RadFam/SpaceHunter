using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Второй тип искусственного интеллекта, так как в нем уже есть убегание
public class EnemyShipAI_2 : EnemyShipAI_Base
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
    private float timer = 0.0f;

    // Хэш-коды названия состояний в которые переходит вражеский кораблик
    protected readonly int m_HashWandering = Animator.StringToHash("Wandering");
    protected readonly int m_HashChasing = Animator.StringToHash("Chasing");
    protected readonly int m_HashAttacking = Animator.StringToHash("Attacking");
    protected readonly int m_HashRunaway = Animator.StringToHash("Runaway");
    protected readonly int m_HashTargetLost = Animator.StringToHash("TargetLost");

    // Use this for initialization
    void Start()
    {
        base.StartBegin();

        myHealth.enemyChHlth = ShipWasAttacked;

        FSMGlobal<EnemyShipAI_2>.Initialise(anim, this);

        wayVector = currWayPoint - gameObject.transform.position;
    }

    // Функция вызывается каждый кадр
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

    // Добавим методы управления (блуждание, маневрирование, преследование, атака, уход от встречной атаки)

    // Проверка, есть ли впереди препятствие
    /*
    bool CheckForObstacle(float sRange, LayerMask mask, out RaycastHit rch)
    {
        return Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out rch, sRange, mask);
    }
    */

    // .......Проверка, нет ли препятсвия между нами и игроком
    /*
    public bool CheckForObstacleHunt()
    {
        return Physics.Raycast(gameObject.transform.position, playerObj.ctrlObject.transform.position - gameObject.transform.position, out rch, distanceToPlayer, obstacleMask);
    }
    */

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
        if (!isUnderAttack)
        {
            isUnderAttack = true;

            // Надо выбрать точку, куда корабль будет убегать
            float dice = Random.Range(0.0f, 1.0f);
            float angleX, angleY;
            if (dice < 0.5f)
            {
                angleY = Random.Range(25.0f, 50.0f);
            }
            else
            {
                angleY = Random.Range(-50.0f, -25.0f);
            }

            dice = Random.Range(0.0f, 1.0f);
            if (dice < 0.5f)
            {
                angleX = Random.Range(25.0f, 50.0f);
            }
            else
            {
                angleX = Random.Range(-50.0f, -25.0f);
            }

            float distRand = Random.Range(50.0f, 75.0f);
            Vector3 subVect = gameObject.transform.forward.normalized;
            Quaternion q1 = Quaternion.AngleAxis(angleX, gameObject.transform.up);
            Quaternion q2 = Quaternion.AngleAxis(angleY, gameObject.transform.right);
            currRunawayPoint = q1 * q2 * subVect * distRand + gameObject.transform.position;

            // Переходим в состояние убегания
            anim.SetTrigger(m_HashRunaway);
        }
    }

    public override void RunawayState()
    {
        wayVector = currRunawayPoint - gameObject.transform.position;
        float dist = Vector3.Distance(gameObject.transform.position, currRunawayPoint);
        if (dist < takeNextWaypointDist)
        {
            if (obstacleRunawayState)
            {
                currRunawayPoint = obstacleRunawayPoint;
                obstacleRunawayState = false;
            }
            ScanForWandering();
        }
    }

    public override void ScanForWandering()
    {
        if (!isUnderAttack)
        {
            anim.SetTrigger(m_HashWandering);
        }
    }

    public override bool CheckForRunawayObstacle()
    {
        bool ans = CheckForObstacle(sightRange, obstacleMask, out rch);

        if (ans)
        {
            obstacleRunawayState = true;
            obstacleRunawayPoint = rch.transform.GetComponent<ObstacleBehaviour>().GetLeavePoint(gameObject.transform.position);
            Vector3 tmp = currRunawayPoint;
            currRunawayPoint = obstacleRunawayPoint;
            obstacleRunawayPoint = tmp;
        }

        return ans;
    }

    // Если игрок уничтожен, то "теряем цель"
    /*
    public void OnMainPlayerDefeat()
    {
        ForgetTarget();
    }
    */
}