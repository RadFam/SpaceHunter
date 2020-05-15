using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformAI : MonoBehaviour
{
    public GameObject player;
    public GameObject leftCannon;
    public GameObject rightCannon;
    public ParticleSystem deathExplode;
    public Transform leftCannonRot;
    public Transform rightCannonRot;

    private Vector3 fixedLeftCannonPos;
    private Vector3 fixedRightCannonPos;

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

    public Vector3 GetLeftCannonPos()
    {
        return fixedLeftCannonPos;
    }

    public Vector3 GetRightCannonPos()
    {
        return fixedRightCannonPos;
    }

    // Start is called before the first frame update
    void Start()
    {
        fixedLeftCannonPos = leftCannon.transform.localPosition;
        fixedRightCannonPos = rightCannon.transform.localPosition;

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

    private void MyDestroy()
    {
        Destroy(gameObject);
    }

    public void OnHealthChange(float val)
    {
    }
}
