using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformAI : MonoBehaviour
{
    public GameObject player;
    public GameObject leftCannon;
    public GameObject rightCannon;
    public ParticleSystem deathExplode;
    Transform leftCannonRot;
    Transform rightCannonRot;

    [SerializeField]
    private float sightRange = 200.0f;
    private float distanceToPlayer;
    private Animator anim;
    private EnemyShipBattleAI enemyBattleAI;

    [SerializeField]
    float timeToLookForPlayer = 0.5f;
    float timer;

    protected readonly int m_HashFloating = Animator.StringToHash("Floating");
    protected readonly int m_HashAttacking = Animator.StringToHash("Attacking");

    public GameObject GetPlayer()
    {
        return player;
    }
    public GameObject GetLeftCannon()
    {
        return leftCannon;
    }
    public Transform GetLeftCannonRot()
    {
        return leftCannonRot;
    }
    public Transform GetRightCannonRot()
    {
        return rightCannonRot;
    }
    public GameObject GetRightCannon()
    {
        return rightCannon;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        enemyBattleAI = gameObject.GetComponent<EnemyShipBattleAI>();

        Damagable myHealth = GetComponent<Damagable>();
        myHealth.enemyChHlth = OnHealthChange;
        myHealth.deathDel = OnMyDeath;

        timer = timeToLookForPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= timeToLookForPlayer)
        {
            distanceToPlayer = Vector3.Distance(gameObject.transform.position, player.transform.position);

            if ((distanceToPlayer <= sightRange) && (player.transform.position.y >= gameObject.transform.position.y) && anim.GetCurrentAnimatorStateInfo(0).IsName("Floating"))
            {
                anim.SetTrigger(m_HashAttacking);
                enemyBattleAI.Makeshoot(true);
            }
            if (((distanceToPlayer > sightRange) || (player.transform.position.y < gameObject.transform.position.y)) && anim.GetCurrentAnimatorStateInfo(0).IsName("Attacking"))
            {
                anim.SetTrigger(m_HashFloating);
                enemyBattleAI.Makeshoot(false);
            }
        }

        timer += Time.deltaTime;
    }

    public void OnPlayerDeath()
    {
        anim.SetTrigger(m_HashFloating);
        enemyBattleAI.Makeshoot(false);
    }

    public void OnMyDeath()
    {
        anim.enabled = false;
        deathExplode.Play();
        // Уничтожаем объект
        Invoke("MyDestroy", 1.8f);
    }

    public void OnHealthChange(float val)
    {
    }
}
