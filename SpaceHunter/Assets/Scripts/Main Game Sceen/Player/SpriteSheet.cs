using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSheet : MonoBehaviour 
{

    public Renderer myRenderer;

    private Vector2 size;
    private int fps = 5;
    private float timer = 0.0f;
    private int cntr = 1;

    void Start()
    {
        size = new Vector2(1.0f, 1.0f);
        if (myRenderer == null)
            enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (timer >= 1.0f / fps)
        {
            timer = 0.0f;
            Vector2 offset = new Vector3(cntr * 1.0f / 3.0f, 0.0f);

            myRenderer.material.SetTextureOffset("_MainTex", offset);
            myRenderer.material.SetTextureScale("_MainTex", size);

            cntr++;
            cntr %= 4;
        }
        timer += Time.deltaTime;
    }
}
