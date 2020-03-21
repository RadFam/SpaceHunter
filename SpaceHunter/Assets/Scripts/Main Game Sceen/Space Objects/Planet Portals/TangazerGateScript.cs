using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TangazerGateScript : MonoBehaviour
{
    public Collider myCheckCollider;

    public GameObject part1;
    public GameObject part2;
    public GameObject part3;
    public GameObject part4;
    public GameObject part5;
    public GameObject part6;

    protected float speedRot1 = -35.0f;
    protected float speedRot2 = 52.0f;
    protected float speedRot3 = 75.0f;
    protected float speedRot4 = -45.0f;
    protected float speedRot5 = 68.0f;
    protected float speedRot6 = -80.0f;

    protected bool isPassed;

    [SerializeField]
    protected float rechargeTime = 300.0f;
    protected float timer;

    // Start is called before the first frame update
    void Start()
    {
        isPassed = false;
    }

    // Update is called once per frame
    void Update()
    {
        part1.transform.RotateAround(gameObject.transform.position, gameObject.transform.forward, speedRot1 * Time.deltaTime);
        part2.transform.RotateAround(gameObject.transform.position, gameObject.transform.forward, speedRot2 * Time.deltaTime);
        part3.transform.RotateAround(gameObject.transform.position, gameObject.transform.forward, speedRot3 * Time.deltaTime);
        part4.transform.RotateAround(gameObject.transform.position, gameObject.transform.forward, speedRot4 * Time.deltaTime);
        part5.transform.RotateAround(gameObject.transform.position, gameObject.transform.forward, speedRot5 * Time.deltaTime);
        part6.transform.RotateAround(gameObject.transform.position, gameObject.transform.forward, speedRot6 * Time.deltaTime);

        if (isPassed)
        {
            timer += Time.deltaTime;

            if (timer >= rechargeTime)
            {
                timer = 0.0f;
                OffCharge();
            }
        }
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !isPassed)
        {
            OnCharge();
        }
    }

    public void OnCharge()
    {
        isPassed = true;
        myCheckCollider.enabled = false;
        // Меняем цвет портала

        // Запускаме цепочку случайных включений у планетных порталов

    }

    public void OffCharge()
    {
        isPassed = false;
        myCheckCollider.enabled = true;
        // Меняем цвет портала, например
    }
}
