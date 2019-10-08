using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaExplosionEffect : MonoBehaviour {

    public ListParticle LP;
    public ParticleSystem mySystem;
    private float timer = 0.0f;
    private float fullTime;
    private bool canPlay = false;

    public void OnEnable()
    {
        LP.listBusyExplodes.Add(gameObject);
        LP.listFreeExplodes.RemoveAt(0);
        fullTime = mySystem.main.duration;
        timer = 0.0f;
    }

    public void SetCoords(Vector3 position, Vector3 needForward)
    {
        gameObject.transform.position = position;
        gameObject.transform.forward = needForward;
        //gameObject.transform.rotation = rotation;

        // Запускаем анимацию
        canPlay = true;
        mySystem.Play();
    }

    void Update()
    {
        if (canPlay)
        {
            timer += Time.deltaTime;
            if (timer >= fullTime)
            {
                canPlay = false;
                OnEndAnimation();
            }
        }
    }

    public void OnEndAnimation()
    {
        timer = 0.0f;
        LP.listFreeExplodes.Add(gameObject);
        LP.listBusyExplodes.RemoveAt(0);
        gameObject.SetActive(false);
    }
}
