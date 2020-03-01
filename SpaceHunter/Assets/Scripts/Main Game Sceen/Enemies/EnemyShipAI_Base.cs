using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipAI_Base : MonoBehaviour
{
    protected Animator anim; // Ссылка на объект аниматор
    public int indexOfPath;
    public ParticleSystem deathExplode;
    protected ListPathPoints LPP;

    protected List<Transform> waypoints; // Список точек по которым движется (патрулирует) вражеский корабль
    protected List<Vector3> waypointsCoord; // Список координат точек по которым движется вражеский корабль

    public SpaceShipMove playerObj; // Ссылка на корабль игрока
    public EnemyShipBattleAI enemyBattleAI; // Ссылка на скрипт который в случае стрельбы генерирует объкты сгустков плазмы

    public LayerMask obstacleMask; // Маска объектов препятствий в пространстве
    public LayerMask enemyMask; // Маска вражеских объектов
    public LayerMask playerMask; // Маска объекта игрока

    protected RaycastHit rch;

    protected Rigidbody enemyRB; // Объект "твердого физического тела" для вражеского корабля
    protected Vector3 wayVector; // Вектор направления куда надо двигаться
    protected Vector3 currWayPoint; // Координата текущей точки куда надо лететь
    protected Vector3 currRunawayPoint;
    protected Vector3 obstacleRunawayPoint;
    protected bool obstacleRunawayState;
    protected int currWayIndex; // Индекс в списке координат текущей точки куда надо двигаться
    protected int addedWayIndex; // Индекс добавленной координаты движения (для огибания препятствий)
    protected float nextWayPointDist;
    protected int increment;

    protected float distanceToPlayer = 0.0f;
    protected float angleToPlayer = 0.0f;

    protected Damagable myHealth; // Скрипт который отвечает за повреждения
    protected bool isUnderAttack;
    public bool IsUnderAttack { get { return isUnderAttack; } set { isUnderAttack = value; } }

    protected void StartBegin()
    {
        anim = GetComponent<Animator>();
        playerObj = FindObjectOfType<SpaceShipMove>();
        enemyRB = GetComponent<Rigidbody>();

        enemyBattleAI = GetComponent<EnemyShipBattleAI>();

        myHealth = GetComponent<Damagable>();
        myHealth.enemyChHlth = OnHealthChange;
        myHealth.deathDel = OnDeath;

        LPP = FindObjectOfType<ListPathPoints>();
        waypoints = LPP.GetPath(indexOfPath);

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
        obstacleRunawayState = false;
    }

    public void OnDeath()
    {
        // Останавливаем FSM
        anim.enabled = false;
        deathExplode.Play();
        // Уничтожаем объект
        Invoke("MyDestroy", 1.8f);
    }
    private void MyDestroy()
    {
        Destroy(gameObject);
    }

    public void OnHealthChange(float val)
    {
        Debug.Log("My Health is: " + val.ToString());
    }

    public bool CheckForObstacle(float sRange, LayerMask mask, out RaycastHit rch)
    {
        return Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out rch, sRange, mask);
    }

    public bool CheckForObstacleHunt()
    {
        return Physics.Raycast(gameObject.transform.position, playerObj.ctrlObject.transform.position - gameObject.transform.position, out rch, distanceToPlayer, obstacleMask);
    }

    public virtual bool CheckForWanderingObstacle()
    {
        return true;
    }

    public virtual void ForgetTarget()
    {
    }

    public virtual void PatrollingSpace()
    {
    }

    public virtual void ChasingSpace()
    {
    }

    public virtual void AttackingSpace()
    {
    }

    public virtual void ScanForChase()
    {
    }

    public virtual void ScanForAttack()
    {
    }

    public virtual void ScanForFurtherAttack()
    {
    }

    public virtual void ShipWasAttacked(float val)
    {
    }

    public virtual void ShipWasNearlyAttacked()
    {
    }

    public virtual void RunawayState()
    {
    }

    public virtual void ScanForWandering()
    {
    }

    public virtual bool CheckForRunawayObstacle()
    {
        return true;
    }

    // Если игрок уничтожен, то "теряем цель"
    public void OnMainPlayerDefeat()
    {
        ForgetTarget();
    }
}
