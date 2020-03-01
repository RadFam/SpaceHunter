using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Первый тип искусственного интеллекта, самый простой
public class EnemyShipAI : EnemyShipAI_Base
{

    /*
    public Animator anim; // Ссылка на объект аниматор
    public List<Transform> waypoints; // Список точек по которым движется (патрулирует) вражеский корабль
    public List<Vector3> waypointsCoord; // Список координат точек по которым движется вражеский корабль

    public SpaceShipMove playerObj; // Ссылка на корабль игрока
    public EnemyShipBattleAI enemyBattleAI; // Ссылка на скрипт который в случае стрельбы генерирует объкты сгустков плазмы
    public LayerMask obstacleMask; // Маска объектов препятствий в пространстве
    public LayerMask enemyMask; // Маска вражеских объектов
    public LayerMask playerMask; // Маска объекта игрока

    private RaycastHit rch;
    */
    [SerializeField]
    float sightRange = 100.0f;
    [SerializeField]
    float sightAngle = 150.0f;
    [SerializeField]
    float attackRange = 45.0f;
    [SerializeField]
    float attackAngle = 5.0f;
    [SerializeField]
    float cruisingSpeed = 30.0f;
    [SerializeField]
    float rotationSpeed = 1.5f;
    [SerializeField]
    float takeNextWaypointDist = 5.0f;

    [SerializeField]
    float timerOfAnalyse = 0.2f; // Период времени (в сек) через который вражеский корабль производит анализ своих действий
    [SerializeField]
    float timer = 0.0f;

    //private float distanceToPlayer = 0.0f;
    //private float angleToPlayer = 0.0f;

    /*
    private Rigidbody enemyRB; // Объект "твердого физического тела" для вражеского корабля
    private Vector3 wayVector; // Вектор направления куда надо двигаться
    private Vector3 currWayPoint; // Координата текущей точки куда надо лететь
    private Vector3 currRunawayPoint;
    public int currWayIndex; // Индекс в списке координат текущей точки куда надо двигаться
    public int addedWayIndex; // Индекс добавленной координаты движения (для огибания препятствий)
    private float nextWayPointDist;
    public int increment;

    private Damagable myHealth; // Скрипт который отвечает за повреждения
    private bool isUnderAttack;
    public bool IsUnderAttack { get { return isUnderAttack; } set { isUnderAttack = value; } }
    */

    // Хэш-коды названия состояний в которые переходит вражеский кораблик
    protected readonly int m_HashWandering = Animator.StringToHash("Wandering"); 
    protected readonly int m_HashChasing = Animator.StringToHash("Chasing");
    protected readonly int m_HashAttacking = Animator.StringToHash("Attacking");
    protected readonly int m_HashTargetLost = Animator.StringToHash("TargetLost");

    // Use this for initialization
    void Start()
    {
        /*
        anim = GetComponent<Animator>();
        playerObj = FindObjectOfType<SpaceShipMove>();
        enemyRB = GetComponent<Rigidbody>();

        enemyBattleAI = GetComponent<EnemyShipBattleAI>();

        myHealth = GetComponent<Damagable>();
        myHealth.enemyChHlth = ShipWasAttacked;
        myHealth.deathDel = OnDeath;
        */
        base.StartBegin();

        /*
        waypointsCoord = new List<Vector3>();
        foreach (Transform tr in waypoints)
        {
            waypointsCoord.Add(tr.position);
        }

        currWayIndex = 0;
        currWayPoint = waypointsCoord[currWayIndex];
        addedWayIndex = -1;
        increment = 1;
        isUnderAttack = false;
        */

        FSMGlobal<EnemyShipAI_Base>.Initialise(anim, this);

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
        if (addedWayIndex == -1)
        {
            currWayPoint = waypointsCoord[currWayIndex];
        }
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

    // Если игрок уничтожен, то "теряем цель"
    /*
    public void OnMainPlayerDefeat()
    {
        ForgetTarget();
    }
    */
}
