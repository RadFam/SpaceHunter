using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipAI_Base : MonoBehaviour
{
    protected Animator anim; // Ссылка на объект аниматор
    public int indexOfPath;
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
    protected int currWayIndex; // Индекс в списке координат текущей точки куда надо двигаться
    protected int addedWayIndex; // Индекс добавленной координаты движения (для огибания препятствий)
    protected float nextWayPointDist;
    protected int increment;

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
    }

    public void OnDeath()
    {
        // Останавливаем FSM
        anim.enabled = false;

        // Уничтожаем объект
        Destroy(gameObject);
    }
}
