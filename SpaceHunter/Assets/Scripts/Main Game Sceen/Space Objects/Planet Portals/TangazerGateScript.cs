using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TangazerGateScript : MonoBehaviour
{
    public Sprite portal_1;
    public Sprite portal_2;

    public SpriteRenderer portalImage;

    public GameObject part1;
    public GameObject part2;
    public GameObject part3;
    public GameObject part4;
    public GameObject part5;
    public GameObject part6;

    protected Collider myCheckCollider;

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
        myCheckCollider = gameObject.GetComponent<Collider>();
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
        portalImage.sprite = portal_1;

        // Запускаме цепочку случайных включений у планетных порталов

    }

    public void OffCharge()
    {
        isPassed = false;
        myCheckCollider.enabled = true;
        // Меняем цвет портала, например
        portalImage.sprite = portal_2;
    }
}
